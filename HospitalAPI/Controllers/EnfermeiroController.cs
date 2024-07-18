using HospitalAPI.Banco;
using HospitalAPI.DTOs;
using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnfermeiroController : ControllerBase
{
    private readonly HospitalAPIContext context;

    public EnfermeiroController(HospitalAPIContext context)
    {
        this.context = context;
    }
    [HttpPost]
    public IActionResult CadastrarEnfermeiro([FromBody] CadastrarPessoaDto cadastrarPessoaDto)
    {
        Enfermeiro enfermeiro = new Enfermeiro(cadastrarPessoaDto);
        context.Enfermeiros.Add(enfermeiro);
        context.SaveChanges();
        return Ok("Enfermeiro cadastrado com sucesso!");
    }
}


