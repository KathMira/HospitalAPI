using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class RetornoController : ControllerBase
{
    public HospitalAPIContext _context;
    public RetornoController(HospitalAPIContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AgendaRetorno([FromBody] AgendarRetornoDto agendarRetornoDto)
    {
        Consulta? consulta = _context.Consultas.FirstOrDefault(x => x.Id == agendarRetornoDto.ConsultaId);
        if (consulta == null)
        {
            return BadRequest("Não achei fio");
        }
        Consulta retorno = consulta.AgendarRetorno(agendarRetornoDto);
        _context.Consultas.Add(retorno);
        await _context.SaveChangesAsync();
        return Ok("Retorno Agendado com sucesso!");
    }

    //add put, get, delete
}
