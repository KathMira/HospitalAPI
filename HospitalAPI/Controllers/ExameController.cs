using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExameController : ControllerBase
{
    public HospitalAPIContext context { get; set; }

    public ExameController(HospitalAPIContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public IActionResult CadastroExame([FromBody] CadastrarExameDto cadastrarExameDto)
    {
        Exame exame = new Exame(cadastrarExameDto);
        context.Exames.Add(exame);
        context.SaveChanges();
        return Ok("Exame cadastrado com sucesso!");
    }

    [HttpGet]
    public IActionResult VerTodosExames()
    {
        List<Exame> vertodosexames = context.Exames.ToList();
        return Ok(vertodosexames);
    }
}
