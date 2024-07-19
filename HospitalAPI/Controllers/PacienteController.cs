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
public class PacienteController : ControllerBase
{
    public HospitalAPIContext context { get; set; }

    public IImagesServices imagesServices { get; set; }
    public PacienteController(HospitalAPIContext context, IImagesServices imagesServices)
    {
        this.context = context;
        this.imagesServices = imagesServices;
    }

    [HttpPost]
    public async Task<IActionResult> CadastroPaciente([FromForm] CadastrarPacienteDto cadastrarPacienteDto)
    {
        try
        {
            string nomeImagem = imagesServices.Salvar(cadastrarPacienteDto.ImagemDocumento.OpenReadStream(), 
                Enums.EnumTiposDocumentos.DocumentoIdentificacao);
            Imagem imagem = new Imagem(Guid.Parse(nomeImagem), Enums.EnumTiposDocumentos.DocumentoIdentificacao);
            context.Imagens.Add(imagem);
            
            Paciente paciente = new Paciente(cadastrarPacienteDto);
            
            paciente.Pessoa.ImagemDocumento = imagem;
            context.Pacientes.Add(paciente);
            await context.SaveChangesAsync();
            return Ok("Paciente cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest("Erro.");
        }
    }

    [HttpGet("{id}/documento")]
    public async Task<IActionResult> PegarImagem([FromRoute] int id)
    {
        Paciente paciente = await context.Pacientes.Include(x => x.Pessoa)
            .Include(x => x.Pessoa.ImagemDocumento)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (paciente == null)
        {
            return BadRequest("Não achei fio");
        }
        Stream imagem = imagesServices.PegarImagem(paciente.Pessoa.ImagemDocumento.NomeImagem.ToString(), 
            Enums.EnumTiposDocumentos.DocumentoIdentificacao); 
        return File(imagem, "image/png");
    }


    [HttpGet]
    public async Task<IActionResult> ListarTodosPacientes()
    {
        List<Paciente> pegaPaciente = await context.Pacientes.Include(x => x.Pessoa).ToListAsync();
        return Ok(pegaPaciente);
    }

    [HttpDelete("{Id}")]
    public IActionResult ApagarPaciente([FromRoute] int Id)
    {
        Paciente paciente = context.Pacientes.FirstOrDefault(x => x.Id == Id);
        if (paciente == null)
        {
            return NotFound("Não encontrei, burro");
        }
        context.Pacientes.Remove(paciente);
        context.SaveChanges();
        return Ok("Paciente removido com sucesso!");
    }

    [HttpPut("{Id}")]
    public IActionResult AtualizarPaciente([FromRoute] int Id, [FromBody] CadastrarPacienteDto cadastrarPacienteDto)
    {
        Paciente paciente = context.Pacientes.Include(x => x.Pessoa).FirstOrDefault(x => x.Id == Id);
        if (paciente == null)
        {
            return BadRequest("Id não existe");
        }

        paciente.Atualizar(cadastrarPacienteDto);
        context.SaveChanges();
        return Ok("Paciente atualizado com sucesso!");
    }
}
