using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConsultaController : ControllerBase
{
    public HospitalAPIContext _context;
    public ILogger<ConsultaController> _logger;
    public ConsultaController(HospitalAPIContext context, ILogger<ConsultaController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CadastroConsulta([FromBody] AgendarConsultaDto cadastrarConsultaDto)
    {
        try
        {
            _logger.LogInformation($"Cadastrando consulta.");
            var medico = await _context.Medicos.FirstOrDefaultAsync(x => x.Id == cadastrarConsultaDto.MedicoId);
            Consulta consultas = new Consulta(cadastrarConsultaDto);
            consultas.ValorConsulta = medico!.area.ValorConsulta;
            _context.Consultas.Add(consultas);
            await _context.SaveChangesAsync();
            return Ok("Consulta cadastrada com sucesso!");
        }
        catch
        {
            _logger.LogInformation($"Erro ao cadastrar consulta.");
            return BadRequest("Não foi possível cadastrar a consulta, por favor, verifique as informações e tente novamente.");
        }
    }

    [HttpGet]
    [Authorize(Policy = Policies.Superior)]
    public async Task<IActionResult> VerTodasConsultas()
    {
        try
        {
            _logger.LogInformation("Listando todos as consultas.");
            List<Consulta> verconsultas = await _context.Consultas.Include(x => x.Laudos).ToListAsync();
            return Ok(verconsultas);
        }
        catch
        {
            return BadRequest("Não foi possível listar todas as consultas.");
        }
    }

    [HttpPut("RealizarPagamento/{id}")]
    [Authorize(Policy = Policies.AdminPaciente)]
    public async Task<IActionResult> RealizaPagamento([FromRoute] int id)
    {
        try
        {
            _logger.LogInformation("Realizando pagamento da consulta.");
            Consulta? consulta = _context.Consultas.FirstOrDefault(consulta => consulta.Id == id);

            if (consulta == null)
            {
                _logger.LogInformation("Não foi possível encontrar a consulta pelo Id.");
                return BadRequest("Não foi possível encontrar a consulta, verifique o Id e tente novamente.");
            }

            consulta.RealizarPagamento();
            await _context.SaveChangesAsync();
            _logger.LogInformation("Pagamento da consulta realizado.");
            return Ok("Pagamento Realizado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPut("Realizar/{id}")]
    [Authorize(Policy = Policies.Superior)]
    public async Task<IActionResult> RealizaConsulta([FromRoute] int id, [FromBody] RealizarConsultaExameDto realizarConsultaDto)
    {
        _logger.LogInformation("Realizando consulta");
        Consulta? consulta = _context.Consultas.FirstOrDefault(x => x.Id == id);

        if (consulta == null)
        {
            _logger.LogInformation("Não foi possível encontrar a consulta pelo Id.");
            return BadRequest("Não foi possível encontrar a consulta, verifique o Id e tente novamente.");
        }
        if (consulta.Pago == false)
        {
            return BadRequest("Não é possível realizar a consulta sem antes realizar o pagamento.");
        }
        consulta.Realizar(realizarConsultaDto);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Consulta realizada.");
        return Ok("Consulta realizada e Retorno agendado com sucesso!");

    }


    [HttpPut("Cancelar/{id}")]
    [Authorize(Policy = Policies.AdminPaciente)]
    public async Task<IActionResult> CancelaConsulta([FromRoute] int id)
    {
        _logger.LogInformation("Cancelando consulta.");
        Consulta? consulta = _context.Consultas.FirstOrDefault(x => x.Id == id);
        if (consulta == null)
        {
            _logger.LogInformation("Não foi possível encontrar a consulta pelo Id.");
            return BadRequest("Não foi possível encontrar a consulta, verifique o Id e tente novamente.");
        }
        consulta.Cancelar();
        await _context.SaveChangesAsync();
        _logger.LogInformation("Consulta cancelada.");
        return Ok("Consulta cancelada");
    }
}
