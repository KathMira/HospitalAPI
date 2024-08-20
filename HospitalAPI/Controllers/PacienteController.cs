using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using HospitalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PacienteController : ControllerBase
{
    public HospitalAPIContext _context;
    private readonly UserManager<Pessoa> _userManager;
    public IImagesServices _imagesServices;
    
    public PacienteController(HospitalAPIContext context,UserManager<Pessoa> userManager, IImagesServices imagesServices)
    {
        _context = context;
        _userManager = userManager;
        _imagesServices = imagesServices;
        
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> CadastroPaciente([FromForm] CadastrarPacienteDto cadastrarPacienteDto)
    {
        try
        {
            Paciente paciente = new Paciente(cadastrarPacienteDto);
           
            var CPF = paciente.Pessoa.CPF;
            if (Regex.IsMatch(CPF, "^\\d{11}$") )
            {
                return BadRequest("O número do Cpf não pode ser menor ou igual a 0.");
            }


            string nomeImagem = _imagesServices.Salvar(cadastrarPacienteDto.ImagemDocumento.OpenReadStream(),
               Enums.EnumTiposDocumentos.DocumentoIdentificacao);

            Imagem imagem = new Imagem(Guid.Parse(nomeImagem), Enums.EnumTiposDocumentos.DocumentoIdentificacao);

            _context.Imagens.Add(imagem);
            paciente.Pessoa.ImagemDocumento = imagem;

            using var transaction = _context.Database.BeginTransaction();
            var resultado = await _userManager.CreateAsync(paciente.Pessoa, cadastrarPacienteDto.Senha);
            if (!resultado.Succeeded)
            {
                var erros = resultado.Errors;
                transaction.Rollback();
                return BadRequest(erros);
            }
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            var resultadoRole = await _userManager.AddToRoleAsync(paciente.Pessoa, Roles.Paciente);
            if (!resultadoRole.Succeeded)
            {
                var erros = resultadoRole.Errors;
                transaction.Rollback();
                return BadRequest(erros);
            }
            transaction.Commit();

            return Ok("Paciente cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest("Erro.");
        }
    }

    [HttpGet("{id}/documento")]
    [Authorize]
    public async Task<IActionResult> PegarImagem([FromRoute] string id)
    {
        Paciente? paciente = await _context.Pacientes.Include(x => x.Pessoa)
            .Include(x => x.Pessoa.ImagemDocumento)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id);

        if (paciente == null)
        {
            return BadRequest("Não achei fio");
        }

        Stream imagem = _imagesServices.PegarImagem(paciente.Pessoa.ImagemDocumento.NomeImagem.ToString(),
            Enums.EnumTiposDocumentos.DocumentoIdentificacao);
        return File(imagem, "image/png");
    }


    [HttpGet]
    [Authorize(Policy = "Superior")]
    public async Task<IActionResult> ListarTodosPacientes()
    {
        var pegaPaciente = await _context.Pacientes.Include(x => x.Pessoa).ToListAsync();
        return Ok(pegaPaciente);
    }

    [HttpDelete("{Id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> ApagarPaciente([FromRoute] string Id)
    {
        Paciente? paciente = _context.Pacientes.FirstOrDefault(x => x.Id.ToString() == Id);
        if (paciente == null)
        {
            return BadRequest("O Id informado não coincide com nenhum em nossa base de dados. Verifique e tente novamente!");
        }
        _context.Pacientes.Remove(paciente);
        await _context.SaveChangesAsync();
        return Ok("Paciente removido com sucesso!");
    }

    [HttpPut("{Id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> AtualizarPaciente([FromRoute] string Id, [FromBody] CadastrarPacienteDto cadastrarPacienteDto)
    {
        Paciente? paciente = _context.Pacientes.Include(x => x.Pessoa).FirstOrDefault(x => x.Id.ToString() == Id);
        if (paciente == null)
        {
            return BadRequest("Id não existe");
        }

        paciente.Atualizar(cadastrarPacienteDto);
        await _context.SaveChangesAsync();
        return Ok("Paciente atualizado com sucesso!");
    }

    [HttpGet("ListaConsultasPorPaciente")]
    [Authorize(Policy = "Superior")]
    public async Task<IActionResult> PegaConsultaPacientePorId([FromQuery] PacienteQueryDto pacienteQueryDto)
    {
        var pegaConsulta = _context.Consultas.AsQueryable();
        if (pacienteQueryDto.PacienteId != null)
        {
            pegaConsulta = pegaConsulta.Where(x => x.PacienteId == pacienteQueryDto.PacienteId);
        }
        List<Consulta> verConsulta = await pegaConsulta.ToListAsync();
        return Ok(verConsulta);
    }

    [HttpGet("ListaExamesPorPaciente")]
    [Authorize(Policy = "Superior")]
    public async Task<IActionResult> PegaExamesPacientePorId([FromQuery] PacienteQueryDto pacienteQueryDto)
    {
        var pegaExame = _context.Exames.AsQueryable();
        if (pacienteQueryDto.PacienteId != null)
        {
            pegaExame = pegaExame.Where(x => x.PacienteId == pacienteQueryDto.PacienteId);
        }
        List<Exame> verExame = await pegaExame.ToListAsync();
        return Ok(verExame);
    }

}
