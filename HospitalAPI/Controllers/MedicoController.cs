using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using HospitalAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicoController : ControllerBase
{
    private readonly HospitalAPIContext _context;
    public IImagesServices _imagesServices;
    public MedicoController(HospitalAPIContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarMedico([FromBody] CadastrarMedicoDto cadastrarMedicoDto)
    {
        try
        {
            string nomeImagem = _imagesServices.Salvar(cadastrarMedicoDto.ImagemDocumento.OpenReadStream(),
                Enums.EnumTiposDocumentos.DocumentoMedico);

            Imagem imagem = new Imagem(Guid.Parse(nomeImagem),
                Enums.EnumTiposDocumentos.DocumentoMedico);

            _context.Imagens.Add(imagem);
            Medico medico = new Medico(cadastrarMedicoDto);
            _context.Medicos.Add(medico);
            await _context.SaveChangesAsync();
            return Ok("Médico cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest("Erro.");
        }

    }
    [HttpGet("{id}/crm")]
    public async Task<IActionResult> VerDcoumentoCRM([FromRoute] int id)
    {
        Medico medico = await _context.Medicos.Include(x => x.Pessoa)
           .Include(x => x.Pessoa.ImagemDocumento)
           .FirstOrDefaultAsync(x => x.Id == id);
        if (medico == null)
        {
            return BadRequest("Não achei fio");
        }
        Stream imagem = _imagesServices.
            PegarImagem(medico.Pessoa.ImagemDocumento.NomeImagem.ToString(),
           Enums.EnumTiposDocumentos.DocumentoMedico);
        return File(imagem, "image/png");
    }

    [HttpGet]
    public async Task<IActionResult> VerTodosMedicos()
    {

        List<Medico> medicos = await _context.Medicos.ToListAsync();
        return Ok(medicos);
    }
    // add put e delete
}
