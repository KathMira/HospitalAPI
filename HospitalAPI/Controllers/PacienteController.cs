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
    public ILogger<PacienteController> _logger;
    public HospitalAPIContext _context;
    private readonly UserManager<Pessoa> _userManager;
    public IImagesServices _imagesServices;
    
    public PacienteController(ILogger<PacienteController> logger, HospitalAPIContext context,UserManager<Pessoa> userManager, IImagesServices imagesServices)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _imagesServices = imagesServices;
        
    }

    [HttpPost]
    [Authorize(Policy = Policies.Administrador)]
    public async Task<IActionResult> CadastroPaciente([FromForm] CadastrarPacienteDto cadastrarPacienteDto)
    {
        try
        {
            _logger.LogInformation($"Cadastrando paciente com CPF{cadastrarPacienteDto.CPF}");
            Paciente paciente = new Paciente(cadastrarPacienteDto);
           
            var CPF = paciente.Pessoa.CPF;
            if (Regex.IsMatch(CPF, "^\\d{11}$") )
            {
                _logger.LogInformation($"Não foi possível cadastrar paciente.");
                return BadRequest("O número do Cpf não pode ser menor ou igual a 0.");
            }

            _logger.LogInformation("Adicionando imagem ao documento do paciente.");
            string nomeImagem = _imagesServices.Salvar(cadastrarPacienteDto.ImagemDocumento.OpenReadStream(),
               Enums.EnumTiposDocumentos.DocumentoIdentificacao);

            Imagem imagem = new Imagem(Guid.Parse(nomeImagem),
               Enums.EnumTiposDocumentos.DocumentoIdentificacao);
            
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
            _logger.LogInformation("Salvando informações do paciente no banco.");
            await _context.SaveChangesAsync();

            var resultadoRole = await _userManager.AddToRoleAsync(paciente.Pessoa, Roles.Paciente);
            if (!resultadoRole.Succeeded)
            {
                var erros = resultadoRole.Errors;
                transaction.Rollback();
                return BadRequest(erros);
            }
            transaction.Commit();
            _logger.LogInformation("Concluindo cadastro do paciente.");
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
        try {
            _logger.LogInformation($"Mostrando imagem do documento do paciente.");
            Paciente? paciente = await _context.Pacientes.Include(x => x.Pessoa)
                .Include(x => x.Pessoa.ImagemDocumento)
                .FirstOrDefaultAsync(x => x.Id.ToString() == id);

            if (paciente == null)
            {
                _logger.LogInformation($"Não foi possível encontrar o Id do Paciente.");
                return BadRequest("Não achei fio");
            }

            Stream imagem = _imagesServices.PegarImagem(paciente.Pessoa.ImagemDocumento.NomeImagem.ToString(),
                Enums.EnumTiposDocumentos.DocumentoIdentificacao);
            return File(imagem, "image/png");
        }
        catch
        {
            _logger.LogInformation("Não foi possível encontrar a imagem do documento do paciente.");
            return BadRequest("Não foi possível encontrar a imagem, verifique o Id do paciente e tente novamente.");
        }
    }



    [HttpGet]
    [Authorize(Policy = Policies.Superior)]
    public async Task<IActionResult> ListarTodosPacientes()
    {
        try
        {
            _logger.LogInformation($"Mostrando lista de pacientes.");
            var pegaPaciente = await _context.Pacientes.Include(x => x.Pessoa).ToListAsync();
            return Ok(pegaPaciente);
        }
        catch
        {
            _logger.LogInformation($"Não foi possível mostrar lista de pacientes.");
            return BadRequest("Não há nenhum paciente cadastrado.");
        }
    }

    [HttpDelete("{Id}")]
    [Authorize(Policy = Policies.Administrador)]
    public async Task<IActionResult> ApagarPaciente([FromRoute] string Id)
    {
        try
        {
            _logger.LogInformation($"APagando paciente da base de dados.");
            Paciente? paciente = _context.Pacientes.FirstOrDefault(x => x.Id.ToString() == Id);
            if (paciente == null)
            {
                _logger.LogInformation($"Não foi possível encontrar o Id do Paciente");
                return BadRequest("O Id informado não coincide com nenhum em nossa base de dados. Verifique e tente novamente!");
            }
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return Ok("Paciente removido com sucesso!");
        }
        catch
        {
            return BadRequest("Não foi possível apagar o paciente do banco de dados.");
        }
    }

    [HttpPut("{Id}")]
    [Authorize(Policy = Policies.Administrador)]
    public async Task<IActionResult> AtualizarPaciente([FromRoute] string Id, [FromBody] CadastrarPacienteDto cadastrarPacienteDto)
    {
        try
        {
            _logger.LogInformation($"Atualizando dados do paciente.");
            Paciente? paciente = _context.Pacientes.Include(x => x.Pessoa).FirstOrDefault(x => x.Id.ToString() == Id);
            if (paciente == null)
            {
                _logger.LogInformation($"Não foi possível encontrar o Id do Paciente");
                return BadRequest("O Id informado não coincide com nenhum em nossa base de dados. Verifique e tente novamente!");
            }

            paciente.Atualizar(cadastrarPacienteDto);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Salvando informações atualizadas do paciente.");
            return Ok("Paciente atualizado com sucesso!");
        }
        catch
        {
            _logger.LogInformation("Não foi possível atualizar os dados do paciente");
            return BadRequest("Não foi possível atualizar os dados do paciente, verifique o Id e tente novamente.");
        }
    }

    [HttpGet("ListaConsultasPorPaciente")]
    [Authorize(Policy = Policies.Superior)]
    public async Task<IActionResult> PegaConsultaPacientePorId([FromQuery] PacienteQueryDto pacienteQueryDto)
    {
        try
        {
            _logger.LogInformation($"Listando consultas por paciente.");
            var pegaConsulta = _context.Consultas.AsQueryable();
            if (pacienteQueryDto.PacienteId != null)
            {
                _logger.LogInformation("Verificando Id paciente.");
                pegaConsulta = pegaConsulta.Where(x => x.PacienteId == pacienteQueryDto.PacienteId);
            }
            List<Consulta> verConsulta = await pegaConsulta.ToListAsync();
            return Ok(verConsulta);
        }
        catch
        {
            _logger.LogInformation($"Erro ao tentar listar consultas por paciente.");
            return BadRequest("Não foi possível listar as consultas por paciente.");
        }
    }

    [HttpGet("ListaExamesPorPaciente")]
    [Authorize(Policy = Policies.Superior)]
    public async Task<IActionResult> PegaExamesPacientePorId([FromQuery] PacienteQueryDto pacienteQueryDto)
    {
        try
        {
            _logger.LogInformation($"Listando Exames por paciente.");
            var pegaExame = _context.Exames.AsQueryable();
            if (pacienteQueryDto.PacienteId != null)
            {
                pegaExame = pegaExame.Where(x => x.PacienteId == pacienteQueryDto.PacienteId);
            }
            List<Exame> verExame = await pegaExame.ToListAsync();
            return Ok(verExame);
        }
        catch
        {
            _logger.LogInformation($"Erro ao tentar listar exames por paciente.");
            return BadRequest("Não foi possível listar as exames por paciente.");
        }
    }

}
