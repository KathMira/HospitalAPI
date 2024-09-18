using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using HospitalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnfermeiroController : ControllerBase
{
    public IImagesServices _imagesServices;
    public UserManager<Pessoa> _userManager;
    private readonly HospitalAPIContext _context;

    public EnfermeiroController(HospitalAPIContext context,UserManager<Pessoa> userManager, IImagesServices imagesServices)
    {
        _context = context;
        _userManager = userManager;
        _imagesServices = imagesServices;
    }

    [HttpPost]
    [Authorize(Policy = Policies.Administrador)]
    public async Task<IActionResult> CadastrarEnfermeiro([FromForm] CadastrarEnfermeiroDto cadastrarEnfermeiroDto)
    {
        try
        {
            Enfermeiro enfermeiro = new Enfermeiro(cadastrarEnfermeiroDto);
            string nomeImagem = _imagesServices.Salvar(cadastrarEnfermeiroDto.ImagemDocumento.OpenReadStream(),
                Enums.EnumTiposDocumentos.DocumentoIdentificacao);
            Imagem imagem = new Imagem(Guid.Parse(nomeImagem),
                Enums.EnumTiposDocumentos.DocumentoIdentificacao);
            _context.Imagens.Add(imagem);
            enfermeiro.Pessoa.ImagemDocumento = imagem;
            using var transaction = _context.Database.BeginTransaction();
            var resultado = await _userManager.CreateAsync(enfermeiro.Pessoa, cadastrarEnfermeiroDto.Senha);
            if (!resultado.Succeeded)
            {
                var erros = resultado.Errors;
                transaction.Rollback();
                return BadRequest(erros);
            }

            _context.Enfermeiros.Add(enfermeiro);
            await _context.SaveChangesAsync();
            var resultadoRole = await _userManager.AddToRoleAsync(enfermeiro.Pessoa, Roles.Enfermeiro);
            if (!resultadoRole.Succeeded)
            {
                var erros = resultadoRole.Errors;
                transaction.Rollback();
                return BadRequest(erros);
            }
            transaction.Commit();
            return Ok("Enfermeiro cadastrado com sucesso!");

        }
        catch (Exception ex)
        {
            return BadRequest("Erro.");
        }
    }

    [HttpGet("{id}/documento")]
    [Authorize(Policy = Policies.Administrador)]
    public async Task<IActionResult> verDocumento([FromRoute] Guid id)
    {
        Enfermeiro? enfermeiro = await _context.Enfermeiros.Include(x => x.Pessoa)
            .Include(x => x.Pessoa.ImagemDocumento)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (enfermeiro == null)
        {
            return BadRequest("Não achei fio");
        }
        Stream imagem = _imagesServices.PegarImagem(enfermeiro.Pessoa.ImagemDocumento.NomeImagem.ToString(),
            Enums.EnumTiposDocumentos.DocumentoIdentificacao);
        return File(imagem, "image/png");
    }

    [HttpGet]
    [Authorize(Policy = Policies.Superior)]
    public async Task<IActionResult> ListarTodosEnfermeiros()
    {
        List<Enfermeiro> verEnfermeiros = await _context.Enfermeiros.ToListAsync();
        return Ok(verEnfermeiros);
    }

    [HttpDelete("{Id}")]
    [Authorize(Policy = Policies.Administrador)]
    public async Task<IActionResult> ApagarEnfermeiro([FromRoute] Guid Id)
    {
        Enfermeiro? enfermeiro = _context.Enfermeiros.FirstOrDefault(x => x.Id == Id);
        if (enfermeiro == null)
        {
            return BadRequest("O Id informado não coincide com nenhum em nossa base de dados. Verifique e tente novamente!");
        }
        _context.Enfermeiros.Remove(enfermeiro);
        await _context.SaveChangesAsync();
        return Ok("Enfermeiro removido com sucesso!");
    }

    [HttpPut("{Id}")]
    [Authorize(Policy = Policies.Administrador)]
    public async Task<IActionResult> AtualizarEnfermeiro([FromRoute] Guid Id, [FromBody] CadastrarEnfermeiroDto cadastrarEnfermeiroDto)
    {
        Enfermeiro? enfermeiro = _context.Enfermeiros.FirstOrDefault(x => x.Id == Id);
        if (enfermeiro == null)
        {
            return BadRequest("O Id informado não coincide com nenhum em nossa base de dados. Verifique e tente novamente!");
        }
        enfermeiro.Atualizar(cadastrarEnfermeiroDto);
        await _context.SaveChangesAsync();
        return Ok("Enfermeiro atualizado com sucesso!");
    }
}


