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
    private readonly HospitalAPIContext context;
    public IImagesServices imagesServices { get; set; }
    public MedicoController(HospitalAPIContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarMedico([FromBody] CadastrarMedicoDto cadastrarMedicoDto)
    {
        try
        {
            string nomeImagem = imagesServices.Salvar(cadastrarMedicoDto.ImagemDocumento.OpenReadStream(),
                Enums.EnumTiposDocumentos.DocumentoMedico);

            Imagem imagem = new Imagem(Guid.Parse(nomeImagem),
                Enums.EnumTiposDocumentos.DocumentoMedico);

            context.Imagens.Add(imagem);
            Medico medico = new Medico(cadastrarMedicoDto);
            context.Medicos.Add(medico);
            await context.SaveChangesAsync();
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
        Medico medico = await context.Medicos.Include(x => x.Pessoa)
           .Include(x => x.Pessoa.ImagemDocumento)
           .FirstOrDefaultAsync(x => x.Id == id);
        if (medico == null)
        {
            return BadRequest("Não achei fio");
        }
        Stream imagem = imagesServices.
            PegarImagem(medico.Pessoa.ImagemDocumento.NomeImagem.ToString(),
           Enums.EnumTiposDocumentos.DocumentoMedico);
        return File(imagem, "image/png");
    }

    [HttpGet]
    public async Task<IActionResult> VerTodosMedicos()
    {

        List<Medico> medicos = await context.Medicos.ToListAsync();
        return Ok(medicos);
    }

}
