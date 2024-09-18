using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using HospitalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LaudoController : ControllerBase
{
    public HospitalAPIContext _context;
    public ILogger<LaudoController> _logger;
    public IImagesServices _imagesServices;

    public LaudoController(HospitalAPIContext context, ILogger<LaudoController> logger, IImagesServices imagesServices)
    {
        _context = context;
        _logger = logger;
        _imagesServices = imagesServices;
    }

    [HttpPost]
    [Authorize(Policy = Policies.Superior)]
    public async Task<IActionResult> Cadastrarlaudo([FromForm] CadastrarLaudoDto cadastrarLaudoDto)
    {
        _logger.LogInformation("Cadastrando laudo.");
        var temExame = _context.Laudos.Any(x => x.ExameId == cadastrarLaudoDto.ExameId);
        if (temExame)
        {
            _logger.LogInformation("Verificando se já existe laudo cadastrado para o exame.");
            return BadRequest("Já existe um laudo cadastrado para esse exame.");
        }
        Laudo laudo = new Laudo(cadastrarLaudoDto);

        string nomeImagem = _imagesServices.Salvar(cadastrarLaudoDto.ImagemDocumento.OpenReadStream(),
            Enums.EnumTiposDocumentos.DocumentoLaudo);

        Imagem imagemLaudo = new Imagem(Guid.Parse(nomeImagem),
               Enums.EnumTiposDocumentos.DocumentoLaudo);
        _logger.LogInformation("Adicionando imagem ao laudo.");
        laudo.ImagemDocumento = imagemLaudo;
        _context.Imagens.Add(imagemLaudo);


        _context.Laudos.Add(laudo);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Laudo cadastrado.");
        return Ok("Laudo cadastrado com sucesso!");
    }

    [HttpGet]
    [Authorize(Policy = Policies.Superior)]
    public async Task<IActionResult> VerTodosLaudos([FromQuery] LaudoQueryDto laudoQueryDto)
    {
        _logger.LogInformation("Listando todos os laudos.");
        var laudoQuery = _context.Laudos.AsQueryable();
        if (laudoQueryDto?.PacienteId != null)
        {
            laudoQuery = laudoQuery.Where
                (x => x.PacienteId == laudoQueryDto.PacienteId);
        }
        if (laudoQueryDto?.MedicoId != null)
        {
            laudoQuery = laudoQuery.Where
                (x => x.MedicoId == laudoQueryDto.MedicoId);
        }
        /*
         if (laudoQueryDto.DataInicio != null)
        {
            laudoQuery = laudoQuery.Where(x => x.DataLaudo >= laudoQueryDto.DataInicio);
        }
        if (laudoQueryDto.DataFinal != null)
        {
            laudoQuery = laudoQuery.Where(x => x.DataLaudo <= laudoQueryDto.DataFinal);
        }
        */


        List<Laudo> vertodoslaudos = await laudoQuery.ToListAsync();
        return Ok(vertodoslaudos);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = Policies.Superior)]
    public async Task<IActionResult> VerLaudoPorId([FromRoute] int id)
    {
        _logger.LogInformation("Mostrando Laudo por Id.");
        Laudo? laudo = await _context.Laudos.FirstOrDefaultAsync(x => x.Id == id);
        if (laudo == null)
        {
            _logger.LogInformation("Não foi possível encontrar o laudo pelo Id.");
            return BadRequest("Não foi possível encontrar o laudo, verifique o Id e tente novamente.");
        }
        return Ok(laudo);

    }

    [HttpGet("{id}/imagemLaudo")]
    [Authorize]
    public async Task<IActionResult> PegaImagemLaudo([FromRoute] string id)
    {

        _logger.LogInformation("Mostrando imagem do laudo.");
        Laudo? laudo = await _context.Laudos.Include(x => x.ImagemDocumento).FirstOrDefaultAsync(x => x.Id.ToString() == id);

        if (laudo == null)
        {
            _logger.LogInformation("Não foi possível encontrar o Id do Laudo.");
            return BadRequest("Não achei fio.");
        }
        Stream imagem = _imagesServices.PegarImagem(laudo.ImagemDocumento.NomeImagem.ToString(),
            Enums.EnumTiposDocumentos.DocumentoLaudo);
        return File(imagem, "image/png");
    }
}
