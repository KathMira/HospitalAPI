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
    public HospitalAPIContext context { get; set; }
    public ConsultaController(HospitalAPIContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public IActionResult CadastroConsulta([FromBody] AgendarConsultaDto cadastrarConsultaDto)
    {
        Consulta consultas = new Consulta(cadastrarConsultaDto);
        context.Consultas.Add(consultas);
        context.SaveChanges();
        return Ok("Consulta cadastrada com sucesso!");
    }

    [HttpGet]
    public IActionResult VerTodasConsultas()
    {
        List<Consulta> verconsultas = context.Consultas.ToList();
        return Ok(verconsultas);
    }

    [HttpPut("Realizar/{id}")]
    public IActionResult RealizaConsulta([FromRoute] int id, [FromBody] RealizarConsultaDto realizarConsultaDto)
    {
        Consulta consulta = context.Consultas.FirstOrDefault(x => x.Id == id);
        if (consulta == null)
        {
            return BadRequest("Não encontrei fio");
        }
        consulta.Realizar(realizarConsultaDto);
        context.SaveChanges();
        return Ok("Consulta realizada e Retorno agendado com sucesso!");

    }


    [HttpPut("Cancelar/{id}")]
    public IActionResult CancelaConsulta([FromRoute] int id)
    {
        Consulta consulta = context.Consultas.FirstOrDefault(x => x.Id == id);
        if (consulta == null)
        {
            return BadRequest("Não encontrei fio");
        }
        consulta.Cancelar();
        context.SaveChanges();
        return Ok("Consulta cancelada");
    }
}
