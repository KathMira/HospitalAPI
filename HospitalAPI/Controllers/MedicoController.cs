using HospitalAPI.Banco;
using HospitalAPI.DTOs;
using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicoController : ControllerBase
{
    private readonly HospitalAPIContext context;

    public MedicoController(HospitalAPIContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public IActionResult CadastrarMedico([FromBody] CadastrarMedicoDto cadastrarMedicoDto)
    {
        Medico medico = new Medico(cadastrarMedicoDto);
        context.Medicos.Add(medico);
        context.SaveChanges();
        return Ok("Médico cadastrado com sucesso!");
    }

    [HttpGet]
    public IActionResult VerTodosMedicos()
    {
        List<Medico> medicos = context.Medicos.ToList();
        return Ok(medicos);
    }

}
