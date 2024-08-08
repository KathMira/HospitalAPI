using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.DTOs.Saida;
using HospitalAPI.Enums;
using HospitalAPI.Modelos;
using HospitalAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HospitalAPI.DTOs.Saida;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PacienteController : ControllerBase
{
    public HospitalAPIContext _context;

    public IImagesServices _imagesServices;
    public PacienteController(HospitalAPIContext context, IImagesServices imagesServices)
    {
        _context = context;
        _imagesServices = imagesServices;
    }

    [HttpPost]
    public async Task<IActionResult> CadastroPaciente([FromForm] CadastrarPacienteDto cadastrarPacienteDto)
    {
        try
        {

            string nomeImagem = _imagesServices.Salvar(cadastrarPacienteDto.ImagemDocumento.OpenReadStream(),
                Enums.EnumTiposDocumentos.DocumentoIdentificacao);
            Imagem imagem = new Imagem(Guid.Parse(nomeImagem), Enums.EnumTiposDocumentos.DocumentoIdentificacao);
            _context.Imagens.Add(imagem);

            Paciente paciente = new Paciente(cadastrarPacienteDto);

            paciente.Pessoa.ImagemDocumento = imagem;
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return Ok("Paciente cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest("Erro.");
        }
    }

    [HttpGet("{id}/documento")]
    public async Task<IActionResult> PegarImagem([FromRoute] int id)
    {
        Paciente? paciente = await _context.Pacientes.Include(x => x.Pessoa)
            .Include(x => x.Pessoa.ImagemDocumento)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (paciente == null)
        {
            return BadRequest("Não achei fio");
        }

        Stream imagem = _imagesServices.PegarImagem(paciente.Pessoa.ImagemDocumento.NomeImagem.ToString(),
            Enums.EnumTiposDocumentos.DocumentoIdentificacao);
        return File(imagem, "image/png");
    }


    [HttpGet]
    public async Task<IActionResult> ListarTodosPacientes()
    {
        var pegaPaciente = await _context.Pacientes.Include(x => x.Pessoa).ToListAsync();
        return Ok(pegaPaciente);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> ApagarPaciente([FromRoute] int Id)
    {
        Paciente? paciente = _context.Pacientes.FirstOrDefault(x => x.Id == Id);
        if (paciente == null)
        {
            return BadRequest("O Id informado não coincide com nenhum em nossa base de dados. Verifique e tente novamente!");
        }
        _context.Pacientes.Remove(paciente);
        await _context.SaveChangesAsync();
        return Ok("Paciente removido com sucesso!");
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> AtualizarPaciente([FromRoute] int Id, [FromBody] CadastrarPacienteDto cadastrarPacienteDto)
    {
        Paciente? paciente = _context.Pacientes.Include(x => x.Pessoa).FirstOrDefault(x => x.Id == Id);
        if (paciente == null)
        {
            return BadRequest("Id não existe");
        }

        paciente.Atualizar(cadastrarPacienteDto);
        await _context.SaveChangesAsync();
        return Ok("Paciente atualizado com sucesso!");
    }

    [HttpGet("ListaConsultasPorPaciente")]
    public async Task<IActionResult> PegaConsultaPacientePorId([FromQuery]PacienteQueryDto pacienteQueryDto)
    {
        //var pegaPaciente = _context.Pacientes.AsQueryable();
        var pegaConsulta = _context.Consultas.AsQueryable();

        if (pacienteQueryDto.PacienteId != null)
        {
            pegaConsulta = pegaConsulta.Where(x => x.PacienteId == pacienteQueryDto.PacienteId);
        }
       
        //var verPaciente = await pegaPaciente.Include(x => x.Pessoa).ToListAsync();
        List<Consulta> verConsulta = await pegaConsulta.ToListAsync();
        //var verExame = pegaExame.ToListAsync();
        return Ok(verConsulta);

      /*Paciente paciente = _context.Pacientes.Include(x => x.Pessoa).FirstOrDefault(x => x.Id == Id);
        if (paciente == null)
        {
            return NotFound();
        }*/
        
        /*
        Exame exame = _context.Exames.FirstOrDefault(x => x.PacienteId == Id);
        if (exame == null)
        {
            return NotFound();
        }
        return Ok($"{paciente.ToString} {consulta.ToString} {exame.ToString}");*/
       
        //return Ok();
    }
    [HttpGet("ListaExamesPorPaciente")]
    public async Task<IActionResult> PegaExamesPacientePorId([FromQuery]PacienteQueryDto pacienteQueryDto)
    {
        var pegaExame = _context.Exames.AsQueryable();
        if (pacienteQueryDto.PacienteId != null)
        {
            pegaExame = pegaExame.Where(x => x.PacienteId == pacienteQueryDto.PacienteId);
        }
       
        List<Exame> verExame = await pegaExame.ToListAsync();
        return Ok(verExame);
    }

}
