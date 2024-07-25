using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ConvenioController : ControllerBase
{
    private readonly HospitalAPIContext _context;

    public ConvenioController(HospitalAPIContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CadastroConvenio(CadastrarConvenioDto cadastrarConvenioDto)
    {
        Convenio convenio = new Convenio(cadastrarConvenioDto);
        _context.Convenios.Add(convenio);
        await _context.SaveChangesAsync();
        return Created(string.Empty, null);
    }

    [HttpGet]
    public async Task<IActionResult> VerTodosConvenios()
    {
        List<Convenio> convenios = await _context.Convenios.ToListAsync();
        return Ok(convenios);
    }

    //adicionar put e delete
}
