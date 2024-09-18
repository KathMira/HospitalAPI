using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers;


[Route("api/[controller]")]
[ApiController]

public class MedicamentoController : ControllerBase
{
    public HospitalAPIContext _context;

    public MedicamentoController(HospitalAPIContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize(Policy = Policies.Superior)]
    public async Task<IActionResult> CadastrarMedicamento([FromBody] CadastrarMedicamentoDto cadastrarMedicamentoDto)
    {
        try
        {
            Medicamentos medicamentos = new Medicamentos(cadastrarMedicamentoDto);
            _context.Medicamentos.Add(medicamentos);
            await _context.SaveChangesAsync();
            return Ok("Medicamento cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            return Ok("Medicamento cadastrado com sucesso!");
        }
    }

    [HttpGet]
    [Authorize(Policy = Policies.Superior)]
    public async Task<IActionResult> VerTodosMedicamentos()
    {

        List<Medicamentos> verMedicamentos = await _context.Medicamentos.ToListAsync();
        return Ok(verMedicamentos);

    }

    [HttpGet("{id}")]
    [Authorize(Policy = Policies.Superior)]
    public async Task<IActionResult> VerMedicamentoPorId([FromRoute]int id)
    {
        
        Medicamentos? medicamentoPorId =  _context.Medicamentos.FirstOrDefault(x => x.Id == id);
        if (medicamentoPorId == null)
        {
            return BadRequest("Não achei fio");
        }
        return Ok(medicamentoPorId);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = Policies.Superior)]
    public async Task<IActionResult> AtualizarMedicamento([FromRoute]int id, [FromBody] CadastrarMedicamentoDto cadastrarMedicamentoDto)
    {
        Medicamentos? medicamentos = _context.Medicamentos.FirstOrDefault(x => x.Id == id);
        if (medicamentos == null)
        {
            return BadRequest("Não achei fio");
        }
        medicamentos.Atualizar(cadastrarMedicamentoDto);
        await _context.SaveChangesAsync();
        return Ok("Medicamento atualizado com sucesso!");
    }
}
