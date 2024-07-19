using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class RetornoController : ControllerBase
{
    public HospitalAPIContext context { get; set; }
    public RetornoController(HospitalAPIContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public IActionResult AgendaRetorno([FromBody] AgendarRetornoDto agendarRetornoDto)
    {
        Consulta consulta = context.Consultas.FirstOrDefault(x => x.Id == agendarRetornoDto.ConsultaId);
        if (consulta == null)
        {
            return BadRequest("Não achei fio");
        }
        Consulta retorno = consulta.AgendarRetorno(agendarRetornoDto);
        context.Consultas.Add(retorno);
        context.SaveChanges();
        return Ok("Retorno Agendado com sucesso!");
    }

}
