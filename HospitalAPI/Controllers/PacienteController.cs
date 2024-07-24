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
    public HospitalAPIContext _context;

    public IImagesServices _imagesServices;
    public PacienteController(HospitalAPIContext context, IImagesServices imagesServices)
    {
        _context = context;
        _imagesServices = imagesServices;
    }

    [HttpPost]
    public async Task<IActionResult> CadastroPaciente([FromForm] CadastrarPacienteDto cadastrarPacienteDto)
    {
        try
        {
            string nomeImagem = _imagesServices.Salvar(cadastrarPacienteDto.ImagemDocumento.OpenReadStream(),
                Enums.EnumTiposDocumentos.DocumentoIdentificacao);
            Imagem imagem = new Imagem(Guid.Parse(nomeImagem), Enums.EnumTiposDocumentos.DocumentoIdentificacao);
            _context.Imagens.Add(imagem);

            Paciente paciente = new Paciente(cadastrarPacienteDto);

            paciente.Pessoa.ImagemDocumento = imagem;
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
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
        Paciente paciente = await _context.Pacientes.Include(x => x.Pessoa)
            .Include(x => x.Pessoa.ImagemDocumento)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (paciente == null)
        {
            return BadRequest("Não achei fio");
        }

        Stream imagem = _imagesServices.PegarImagem(paciente.Pessoa.ImagemDocumento.NomeImagem.ToString(),
            Enums.EnumTiposDocumentos.DocumentoIdentificacao);
        return File(imagem, "image/png");
    }


    [HttpGet]
    public async Task<IActionResult> ListarTodosPacientes()
    {
        List<Paciente> pegaPaciente = await _context.Pacientes.Include(x => x.Pessoa).ToListAsync();
        return Ok(pegaPaciente);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> ApagarPaciente([FromRoute] int Id)
    {
        Paciente paciente = _context.Pacientes.FirstOrDefault(x => x.Id == Id);
        if (paciente == null)
        {
            return BadRequest("O Id informado não coincide com nenhum em nossa base de dados. Verifique e tente novamente!");
        }
        _context.Pacientes.Remove(paciente);
        await _context.SaveChangesAsync();
        return Ok("Paciente removido com sucesso!");
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> AtualizarPaciente([FromRoute] int Id, [FromBody] CadastrarPacienteDto cadastrarPacienteDto)
    {
        Paciente paciente = _context.Pacientes.Include(x => x.Pessoa).FirstOrDefault(x => x.Id == Id);
        if (paciente == null)
        {
            return BadRequest("Id não existe");
        }

        paciente.Atualizar(cadastrarPacienteDto);
        await _context.SaveChangesAsync();
        return Ok("Paciente atualizado com sucesso!");
    }
}
