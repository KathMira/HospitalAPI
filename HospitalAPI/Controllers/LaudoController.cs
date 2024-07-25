using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LaudoController : ControllerBase
{
    public HospitalAPIContext _context;

    public LaudoController(HospitalAPIContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrarlaudo([FromBody] CadastrarLaudoDto cadastrarLaudoDto)
    {
        Laudo laudo = new Laudo(cadastrarLaudoDto);
        _context.Laudos.Add(laudo);
        await _context.SaveChangesAsync();
        return Ok("Laudo cadastrado com sucesso!");
    }
    [HttpGet]
    public async Task<IActionResult> VerTodosLaudos()
    {
        List<Laudo> vertodoslaudos = await _context.Laudos.ToListAsync();
        return Ok(vertodoslaudos);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> VerLaudoPorId([FromRoute] int id)
    {
            Laudo? laudo = await _context.Laudos.FirstOrDefaultAsync(x => x.Id == id);
            if (laudo == null)
            {
                return BadRequest("Não achei fio");
            }
            return Ok(laudo);
       
    }
}
