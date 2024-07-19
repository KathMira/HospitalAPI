using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PacienteController : ControllerBase
{
    public HospitalAPIContext context { get; set; }
    public PacienteController(HospitalAPIContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CadastroPaciente([FromBody] CadastrarPacienteDto cadastrarPacienteDto)
    {
        try
        {
            Paciente paciente = new Paciente(cadastrarPacienteDto);
            context.Pacientes.Add(paciente);
            await context.SaveChangesAsync();
            return Ok("Paciente cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest("Erro.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarTodosPacientes()
    {
        List<Paciente> pegaPaciente = await context.Pacientes.Include(x => x.Pessoa).ToListAsync();
        return Ok(pegaPaciente);
    }

    [HttpDelete("{Id}")]
    public IActionResult ApagarPaciente([FromRoute] int Id)
    {
        Paciente paciente = context.Pacientes.FirstOrDefault(x => x.Id == Id);
        if (paciente == null)
        {
            return NotFound("Não encontrei, burro");
        }
        context.Pacientes.Remove(paciente);
        context.SaveChanges();
        return Ok("Paciente removido com sucesso!");
    }

    [HttpPut("{Id}")]
    public IActionResult AtualizarPaciente([FromRoute] int Id, [FromBody] CadastrarPacienteDto cadastrarPacienteDto)
    {
        Paciente paciente = context.Pacientes.Include(x => x.Pessoa).FirstOrDefault(x => x.Id == Id);
        if (paciente == null)
        {
            return BadRequest("Id não existe");
        }

        paciente.Atualizar(cadastrarPacienteDto);
        context.SaveChanges();
        return Ok("Paciente atualizado com sucesso!");
    }
}
