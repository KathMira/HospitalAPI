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
public class MedicoController : ControllerBase
{
    private readonly HospitalAPIContext _context;
    public IImagesServices _imagesServices;
    public MedicoController(HospitalAPIContext context, IImagesServices imagesServices)
    {
        _context = context;
        _imagesServices = imagesServices;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarMedico([FromForm] CadastrarMedicoDto cadastrarMedicoDto)
    {

        try
        {
            string nomeImagem = _imagesServices.Salvar(cadastrarMedicoDto.ImagemDocumento.OpenReadStream(),
    Enums.EnumTiposDocumentos.DocumentoMedico);

            Imagem imagem = new Imagem(Guid.Parse(nomeImagem),
                Enums.EnumTiposDocumentos.DocumentoMedico);

            _context.Imagens.Add(imagem);

            Medico medico = new Medico(cadastrarMedicoDto);
            medico.Pessoa.ImagemDocumento = imagem;
            _context.Medicos.Add(medico);

            await _context.SaveChangesAsync();
            return Ok("Médico cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest("Erro.");
        }

    }


    [HttpGet("{id}/crm")]
    public async Task<IActionResult> VerDcoumentoCRM([FromRoute] int id)
    {
        Medico medico = await _context.Medicos.Include(x => x.Pessoa)
           .Include(x => x.Pessoa.ImagemDocumento)
           .FirstOrDefaultAsync(x => x.Id == id);
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
    public async Task<IActionResult> VerTodosMedicos()
    {

        List<Medico> medicos = await _context.Medicos.ToListAsync();
        return Ok(medicos);
    }

    [HttpGet("ListaConsultasPorMedico")]
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
    public async Task<IActionResult> ApagarPaciente([FromRoute] int Id)
    {
      Medico? medico = _context.Medicos.FirstOrDefault(x => x.Id == Id);
        if (medico == null)
        {
            return BadRequest("O Id informado não coincide com nenhum em nossa base de dados. Verifique e tente novamente!");
        }
        _context.Medicos.Remove(medico);
        await _context.SaveChangesAsync();
        return Ok("Médico removido com sucesso!");
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> AtualizarMedico([FromRoute] int Id, [FromBody] CadastrarMedicoDto cadastrarMedicoDto)
    {
        Medico? medico = _context.Medicos.FirstOrDefault(x => x.Id == Id);
        if (medico == null)
        {
            return BadRequest("Id não existe");
        }

        medico.Atualizar(cadastrarMedicoDto);
        await _context.SaveChangesAsync();
        return Ok("Médico atualizado com sucesso!");
    }
}