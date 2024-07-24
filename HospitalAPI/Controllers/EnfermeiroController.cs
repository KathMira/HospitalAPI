using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using HospitalAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnfermeiroController : ControllerBase
{
    public IImagesServices _imagesServices;
    private readonly HospitalAPIContext _context;

    public EnfermeiroController(HospitalAPIContext context, IImagesServices imagesServices)
    {
        _context = context;
        _imagesServices = imagesServices;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarEnfermeiro([FromForm] CadastrarEnfermeiroDto cadastrarEnfermeiroDto)
    {
        try
        {
            string nomeImagem = _imagesServices.Salvar(cadastrarEnfermeiroDto.ImagemDocumento.OpenReadStream(),
                Enums.EnumTiposDocumentos.DocumentoIdentificacao);
            Imagem imagem = new Imagem(Guid.Parse(nomeImagem),
                Enums.EnumTiposDocumentos.DocumentoIdentificacao);
            _context.Imagens.Add(imagem);


            Enfermeiro enfermeiro = new Enfermeiro(cadastrarEnfermeiroDto);
            enfermeiro.Pessoa.ImagemDocumento = imagem;
            _context.Enfermeiros.Add(enfermeiro);
            await _context.SaveChangesAsync();
            return Ok("Enfermeiro cadastrado com sucesso!");

        }
        catch (Exception ex)
        {
            return BadRequest("Erro.");
        }
    }

    [HttpGet("{id}/documento")]
    public async Task<IActionResult> verDocumento([FromRoute] int id)
    {
        Enfermeiro enfermeiro = await _context.Enfermeiros.Include(x => x.Pessoa)
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
    public async Task<IActionResult> ListarTodosEnfermeiros()
    {
        List<Enfermeiro> verEnfermeiros = await _context.Enfermeiros.ToListAsync();
        return Ok(verEnfermeiros);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> ApagarEnfermeiro([FromRoute] int Id)
    {
        Enfermeiro enfermeiro = _context.Enfermeiros.FirstOrDefault(x => x.Id == Id);
        if (enfermeiro == null)
        {
            return BadRequest("O Id informado não coincide com nenhum em nossa base de dados. Verifique e tente novamente!");
        }
        _context.Enfermeiros.Remove(enfermeiro);
        await _context.SaveChangesAsync();
        return Ok("Enfermeiro removido com sucesso!");
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> AtualizarEnfermeiro([FromRoute] int Id, [FromBody] CadastrarEnfermeiroDto cadastrarEnfermeiroDto)
    {
        Enfermeiro enfermeiro = _context.Enfermeiros.FirstOrDefault(x => x.Id == Id);
        if (enfermeiro == null)
        {
            return BadRequest("O Id informado não coincide com nenhum em nossa base de dados. Verifique e tente novamente!");
        }
        enfermeiro.Atualizar(cadastrarEnfermeiroDto);
        await _context.SaveChangesAsync();
        return Ok("Enfermeiro atualizado com sucesso!");
    }
}


