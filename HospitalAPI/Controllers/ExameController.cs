using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExameController : ControllerBase
{
    public HospitalAPIContext _context;

    public ExameController(HospitalAPIContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CadastroExame([FromBody] CadastrarExameDto cadastrarExameDto)
    {
        Exame exame = new Exame(cadastrarExameDto);
        _context.Exames.Add(exame);
        await _context.SaveChangesAsync();
        return Ok("Exame cadastrado com sucesso!");
    }

    [HttpGet]
    public async Task<IActionResult> VerTodosExames()
    {
        List<Exame> vertodosexames = await _context.Exames.ToListAsync();
        return Ok(vertodosexames);
    }
    //add put e delete
}
