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
public class EnfermeiroController : ControllerBase
{
    public IImagesServices imagesServices { get; set; }
    private readonly HospitalAPIContext context;

    public EnfermeiroController(HospitalAPIContext context, IImagesServices imagesServices)
    {
        this.context = context;
        this.imagesServices = imagesServices;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarEnfermeiro([FromForm] CadastrarEnfermeiroDto cadastrarEnfermeiroDto)
    {
        try
        {
            string nomeImagem = imagesServices.Salvar(cadastrarEnfermeiroDto.ImagemDocumento.OpenReadStream(),
                Enums.EnumTiposDocumentos.DocumentoIdentificacao);
            Imagem imagem = new Imagem(Guid.Parse(nomeImagem),
                Enums.EnumTiposDocumentos.DocumentoIdentificacao);
            context.Imagens.Add(imagem);


            Enfermeiro enfermeiro = new Enfermeiro(cadastrarEnfermeiroDto);
            enfermeiro.Pessoa.ImagemDocumento = imagem;
            context.Enfermeiros.Add(enfermeiro);
            await context.SaveChangesAsync();
            return Ok("Enfermeiro cadastrado com sucesso!");

        }
        catch (Exception ex)
        {
            return BadRequest("Erro.");
        }
    }

    [HttpGet("{id}/documento")]
    public async Task<IActionResult> verDocumento([FromRoute] int id)
    {
        Enfermeiro enfermeiro = await context.Enfermeiros.Include(x => x.Pessoa)
            .Include(x => x.Pessoa.ImagemDocumento)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (enfermeiro == null)
        {
            return BadRequest("Não achei fio");
        }
        Stream imagem = imagesServices.PegarImagem(enfermeiro.Pessoa.ImagemDocumento.NomeImagem.ToString(),
            Enums.EnumTiposDocumentos.DocumentoIdentificacao);
        return File(imagem, "image/png");
    }

    [HttpGet]
    public async Task<IActionResult> ListarTodosEnfermeiros()
    {
        List<Enfermeiro> verEnfermeiros = await context.Enfermeiros.ToListAsync();
        return Ok(verEnfermeiros);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> ApagarEnfermeiro([FromRoute] int Id)
    {
        Enfermeiro enfermeiro = context.Enfermeiros.FirstOrDefault(x => x.Id == Id);
        if (enfermeiro == null)
        {
            return BadRequest("O Id informado não coincide com nenhum em nossa base de dados. Verifique e tente novamente!");
        }
        context.Enfermeiros.Remove(enfermeiro);
        await context.SaveChangesAsync();
        return Ok("Enfermeiro removido com sucesso!");
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> AtualizarEnfermeiro([FromRoute]int Id, [FromBody]CadastrarEnfermeiroDto cadastrarEnfermeiroDto)
    {
        Enfermeiro enfermeiro = context.Enfermeiros.FirstOrDefault(x => x.Id == Id);
        if (enfermeiro == null)
        {
            return BadRequest("O Id informado não coincide com nenhum em nossa base de dados. Verifique e tente novamente!");
        }
        enfermeiro.Atualizar(cadastrarEnfermeiroDto);
        await context.SaveChangesAsync();
        return Ok("Enfermeiro atualizado com sucesso!");
    }
}


