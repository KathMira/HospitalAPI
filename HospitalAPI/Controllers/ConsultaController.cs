using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConsultaController : ControllerBase
{
    public HospitalAPIContext _context;
    public ConsultaController(HospitalAPIContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CadastroConsulta([FromBody] AgendarConsultaDto cadastrarConsultaDto)
    {
        Consulta consultas = new Consulta(cadastrarConsultaDto);
        _context.Consultas.Add(consultas);
        await _context.SaveChangesAsync();
        return Ok("Consulta cadastrada com sucesso!");
    }

    [HttpGet]
    public async Task<IActionResult> VerTodasConsultas()
    {
        List<Consulta> verconsultas = await _context.Consultas.ToListAsync();
        return Ok(verconsultas);
    }

    [HttpPut("RealizarPagamento/{id}")]
    public async Task<IActionResult> RealizaPagamento([FromRoute] int id)
    {
        Consulta? consulta = _context.Consultas.FirstOrDefault(consulta => consulta.Id == id);
        if (consulta == null)
        {
            return BadRequest("Não achei fio");
        }
        consulta.RealizarPagamento();
        await _context.SaveChangesAsync();
        return Ok("Pagamento Realizado");
    }

    [HttpPut("Realizar/{id}")]
    public async Task<IActionResult> RealizaConsulta([FromRoute] int id, [FromBody] RealizarConsultaDto realizarConsultaDto)
    {
        
        Consulta? consulta = _context.Consultas.FirstOrDefault(x => x.Id == id);
        
        if (consulta == null)
        {
            return BadRequest("Não encontrei fio");
        }
        if (consulta.Pago == false)
        {
            return BadRequest("Vai pagar fio");
        }
        consulta.Realizar(realizarConsultaDto);
        await _context.SaveChangesAsync();
        return Ok("Consulta realizada e Retorno agendado com sucesso!");

    }


    [HttpPut("Cancelar/{id}")]
    public async Task<IActionResult> CancelaConsulta([FromRoute] int id)
    {
        Consulta? consulta = _context.Consultas.FirstOrDefault(x => x.Id == id);
        if (consulta == null)
        {
            return BadRequest("Não encontrei fio");
        }
        consulta.Cancelar();
        await _context.SaveChangesAsync();
        return Ok("Consulta cancelada");
    }
}
