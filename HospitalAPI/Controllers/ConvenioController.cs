﻿using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ConvenioController : ControllerBase
{
    private readonly HospitalAPIContext _context;
    private readonly ILogger<ConvenioController> _logger;

    public ConvenioController(HospitalAPIContext context, ILogger<ConvenioController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    [Authorize(Policy = Policies.Administrador)]
    public async Task<IActionResult> CadastroConvenio(CadastrarConvenioDto cadastrarConvenioDto)
    {
        try
        {
            //acessando banco de dados via variavel - context onde é buscado um convenio a partir do seu nome, o nome vem da requisição http.
            var possivelConvenio = await _context.Convenios.Where(convenio => convenio.Nome == cadastrarConvenioDto.Nome).FirstOrDefaultAsync();

            //caso já exista um convenio é retornado para o usuário BadResquest.
            if(possivelConvenio != null)
            {
                return BadRequest("Não foi possível cadastrar o convênio pois já existe um com o mesmo nome.");
            }
            _logger.LogInformation("Cadastrando convenio.");
            Convenio convenio = new Convenio(cadastrarConvenioDto);

            //Adiciona novo convenio a tabela do banco.
            _context.Convenios.Add(convenio);
            //Salva as mudanças no banco.
            await _context.SaveChangesAsync();
            //retorna uma mensagem para o usuário.
            return Created(string.Empty, null);
        }
        catch (ApplicationException error)
        {
            return BadRequest(error.Message);
        }
        catch (Exception ex)
        {
            return BadRequest("Não foi possível cadastrar o convênio.");
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> VerTodosConvenios()
    {
        _logger.LogInformation("Listando todos os convenios cadastrados.");
        List<Convenio> convenios = await _context.Convenios.ToListAsync();
        return Ok(convenios);
    }

    [HttpPut("{Id}")]
    [Authorize(Policy = Policies.Administrador)]
    public async Task<IActionResult> AtualizaConvenio([FromRoute]string Id, [FromBody]CadastrarConvenioDto cadastrarConvenioDto)
    {
        _logger.LogInformation($"Atualizando dados do Convenio{cadastrarConvenioDto.Nome}");
        Convenio? convenio = _context.Convenios.FirstOrDefault(x => x.Id.ToString() == Id);
        if (convenio == null)
        {
            _logger.LogInformation("Não foi possível encontrar o Id do Convênio.");
            return BadRequest("O Id informado não coincide com nenhum em nossa base de dados. Verifique e tente novamente!");
        }
        convenio.Atualizar(cadastrarConvenioDto);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Salvando informações atualizadas do convênio.");
        return Ok("Convênio atualizado com sucesso!");
    }


    [HttpDelete("{Id}")]
    [Authorize(Policy = Policies.Administrador)]
    public async Task<IActionResult> ApagaConvenio([FromRoute]string Id)
    {
        try
        {
            _logger.LogInformation("Apagando convenio da base de dados.");
            Convenio? convenio = _context.Convenios.FirstOrDefault(x => x.Id.ToString() == Id);
            if (convenio == null)
            {
                _logger.LogInformation("Não foi possível encontrar o Id do Convênio.");
                return BadRequest("O Id informado não coincide com nenhum em nossa base de dados. Verifique e tente novamente!");
            }
            _context.Convenios.Remove(convenio);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Convênio apagado com sucesso!");
            return Ok("Convênio removido com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest("Não foi possível apagar o convênio do banco de dados.");
        }
    }
}
