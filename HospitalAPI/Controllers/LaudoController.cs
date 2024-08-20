﻿using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "Superior")]
    public async Task<IActionResult> Cadastrarlaudo([FromBody] CadastrarLaudoDto cadastrarLaudoDto)
    {
        Laudo laudo = new Laudo(cadastrarLaudoDto);
        _context.Laudos.Add(laudo);
        await _context.SaveChangesAsync();
        return Ok("Laudo cadastrado com sucesso!");
    }

    [HttpGet]
    [Authorize(Policy = "Superior")]
    public async Task<IActionResult> VerTodosLaudos([FromQuery] LaudoQueryDto laudoQueryDto)
    {
        var laudoQuery = _context.Laudos.AsQueryable();
        if (laudoQueryDto.PacienteId != null)
        {
            laudoQuery = laudoQuery.Where
                (x => x.PacienteId == laudoQueryDto.PacienteId);
        }
        if (laudoQueryDto.MedicoId != null)
        {
            laudoQuery = laudoQuery.Where
                (x => x.MedicoId == laudoQueryDto.MedicoId);
        }
        if (laudoQueryDto.DataInicio != null)
        {
            laudoQuery = laudoQuery.Where(x => x.DataLaudo >= laudoQueryDto.DataInicio);
        }
        if (laudoQueryDto.DataFinal != null)
        {
            laudoQuery = laudoQuery.Where(x => x.DataLaudo <= laudoQueryDto.DataFinal);
        }
       

        List<Laudo> vertodoslaudos = await laudoQuery.ToListAsync();
        return Ok(vertodoslaudos);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "Superior")]
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
