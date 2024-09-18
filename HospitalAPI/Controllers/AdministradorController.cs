using HospitalAPI.Banco;
using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using HospitalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;



[Route("api/[controller]")]
[ApiController]

public class AdministradorController : ControllerBase
{
    private readonly UserManager<Pessoa> _userManager;
    public ILogger<AdministradorController> _logger;
    private readonly HospitalAPIContext _context;
    private readonly IImagesServices _imagesServices;

    public AdministradorController(UserManager<Pessoa> userManager, ILogger<AdministradorController> logger, HospitalAPIContext context, IImagesServices imagesServices)
    {
        _userManager = userManager;
        _logger = logger;
        _context = context;
        _imagesServices = imagesServices;
    }

    [HttpPost]
    //[Authorize(Policy = Policies.Administrador)]
    public async Task<IActionResult> CadastraAdministrador([FromForm] CadastrarPessoaDto cadastrarPessoaDto)
    {
        try {
            _logger.LogInformation($"Cadastrando administrador.");
            var usuarioExiste = await _userManager.FindByNameAsync(cadastrarPessoaDto.CPF);
            if (usuarioExiste != null)
            {
                return BadRequest("Usuário já existente!");
            }
            _logger.LogInformation("Adicionando imagem do documento do administrador.");
            string nomeImagem = _imagesServices.Salvar(cadastrarPessoaDto.ImagemDocumento.OpenReadStream(),
                Enums.EnumTiposDocumentos.DocumentoIdentificacao);

            Imagem imagem = new Imagem(Guid.Parse(nomeImagem), Enums.EnumTiposDocumentos.DocumentoIdentificacao);
            _context.Imagens.Add(imagem);

            var admin = new Administrador(cadastrarPessoaDto);
            using var transaction = _context.Database.BeginTransaction();
            admin.Pessoa.ImagemDocumento = imagem;

            var resultado = await _userManager.CreateAsync(admin.Pessoa, cadastrarPessoaDto.Senha);
            if (!resultado.Succeeded)
            {
                var erros = resultado.Errors;
                transaction.Rollback();
                return BadRequest(erros);
            }
            _logger.LogInformation("Salvando informações do administrador no banco.");
            _context.Add(admin);
            _context.SaveChanges();
            var resultadoRole = await _userManager.AddToRoleAsync(admin.Pessoa, Roles.Administrador);
            if (!resultadoRole.Succeeded)
            {
                var erros = resultadoRole.Errors;
                transaction.Rollback();
                return BadRequest(erros);
            }
            transaction.Commit();
            _logger.LogInformation("Concluindo cadastro do administrador.");
            return Ok("Administrador cadastrado com sucesso!");
        }
        catch
        {
            _logger.LogInformation($"Não foi possível concluir o cadastro de administrador.");
            return BadRequest("Não foi possível fazer o cadastro, verifique as informações e tente novamente.");
        }

    }
}
