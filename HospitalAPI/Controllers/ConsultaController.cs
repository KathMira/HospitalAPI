using HospitalAPI.Banco;
using HospitalAPI.DTOs;
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
        Consultas consultas = new Consultas(cadastrarConsultaDto);
        context.Consultas.Add(consultas);
        context.SaveChanges();
        return Ok("Consulta cadastrada com sucesso!");
    }

    [HttpGet]
    public IActionResult VerTodasConsultas()
    {
        List<Consultas> verconsultas = context.Consultas.ToList();
        return Ok(verconsultas);
    }
}
