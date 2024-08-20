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
public class MedicoController : ControllerBase
{
    private readonly HospitalAPIContext _context;
    public UserManager<Pessoa> _userManager;
    public IImagesServices _imagesServices;
    public MedicoController(HospitalAPIContext context,UserManager<Pessoa> userManager, IImagesServices imagesServices)
    {
        _context = context;
        _userManager = userManager;
        _imagesServices = imagesServices;
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> CadastrarMedico([FromForm] CadastrarMedicoDto cadastrarMedicoDto)
    {

        try
        {
            Medico medico = new Medico(cadastrarMedicoDto);

            string nomeImagem = _imagesServices.Salvar(cadastrarMedicoDto.ImagemDocumento.OpenReadStream(),
    Enums.EnumTiposDocumentos.DocumentoMedico);
                Imagem imagem = new Imagem(Guid.Parse(nomeImagem),
                Enums.EnumTiposDocumentos.DocumentoMedico);

            _context.Imagens.Add(imagem);
            medico.Pessoa.ImagemDocumento = imagem;

            using var transaction = _context.Database.BeginTransaction();
            var resultado = await _userManager.CreateAsync(medico.Pessoa, cadastrarMedicoDto.Senha);
            if (!resultado.Succeeded)
            {
                var erros = resultado.Errors;
                transaction.Rollback();
                return BadRequest(erros);
            }

            _context.Medicos.Add(medico);

            await _context.SaveChangesAsync();

            var resultadoRole = await _userManager.AddToRoleAsync(medico.Pessoa, Roles.Medico);
            if (!resultadoRole.Succeeded)
            {
                var erros = resultadoRole.Errors;
                transaction.Rollback();
                return BadRequest(erros);
            }
            transaction.Commit();
            return Ok("Médico cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest("Erro.");
        }

    }


    [HttpGet("{id}/crm")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> VerDcoumentoCRM([FromRoute] string id)
    {
        Medico medico = await _context.Medicos.Include(x => x.Pessoa)
           .Include(x => x.Pessoa.ImagemDocumento)
           .FirstOrDefaultAsync(x => x.Id.ToString() == id);
        if (medico == null)
        {
            return BadRequest("Não achei fio");
        }
        Stream imagem = _imagesServices.
            PegarImagem(medico.Pessoa.ImagemDocumento.NomeImagem.ToString(),
           Enums.EnumTiposDocumentos.DocumentoMedico);
        return File(imagem, "image/png");
    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> VerTodosMedicos()
    {

        List<Medico> medicos = await _context.Medicos.ToListAsync();
        return Ok(medicos);
    }

    [HttpGet("ListaConsultasPorMedico")]
    [Authorize(Policy = "Superior")]
    public async Task<IActionResult> PegaConsultaPacientePorId([FromQuery] MedicoQueryDto medicoQueryDto)
    {
        var pegaConsulta = _context.Consultas.AsQueryable();

        if (medicoQueryDto.MedicoId != null)
        {
            pegaConsulta = pegaConsulta.Where(x => x.MedicoId == medicoQueryDto.MedicoId);
        }
        if (medicoQueryDto.DataInicio != null)
        {
            pegaConsulta = pegaConsulta.Where(x => x.DataInicio >= medicoQueryDto.DataInicio);
        }
        List<Consulta> verConsulta = await pegaConsulta.ToListAsync();
        return Ok(verConsulta);
    }


    [HttpGet("ListaExamesPorMedico")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> PegaExamesPacientePorId([FromQuery] MedicoQueryDto medicoQueryDto)
    {
        var pegaExame = _context.Exames.AsQueryable();
        if (medicoQueryDto.MedicoId != null)
        {
            pegaExame = pegaExame.Where(x => x.PacienteId == medicoQueryDto.MedicoId);
        }

        List<Exame> verExame = await pegaExame.ToListAsync();
        return Ok(verExame);
    }

    [HttpDelete("{Id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> ApagarPaciente([FromRoute] string Id)
    {
      Medico? medico = _context.Medicos.FirstOrDefault(x => x.Id.ToString() == Id);
        if (medico == null)
        {
            return BadRequest("O Id informado não coincide com nenhum em nossa base de dados. Verifique e tente novamente!");
        }
        _context.Medicos.Remove(medico);
        await _context.SaveChangesAsync();
        return Ok("Médico removido com sucesso!");
    }

    [HttpPut("{Id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> AtualizarMedico([FromRoute] string Id, [FromBody] CadastrarMedicoDto cadastrarMedicoDto)
    {
        Medico? medico = _context.Medicos.FirstOrDefault(x => x.Id.ToString() == Id);
        if (medico == null)
        {
            return BadRequest("Id não existe");
        }

        medico.Atualizar(cadastrarMedicoDto);
        await _context.SaveChangesAsync();
        return Ok("Médico atualizado com sucesso!");
    }
}