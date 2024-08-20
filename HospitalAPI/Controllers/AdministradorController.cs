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

    private readonly HospitalAPIContext _context;
    private readonly IImagesServices _imagesServices;

    public AdministradorController(UserManager<Pessoa> userManager, HospitalAPIContext context, IImagesServices imagesServices)
    {
        _userManager = userManager;
        _context = context;
        _imagesServices = imagesServices;
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> CadastraAdministrador([FromForm] CadastrarPessoaDto cadastrarPessoaDto)
    {

        
        var usuarioExiste = await _userManager.FindByNameAsync(cadastrarPessoaDto.CPF);
        if (usuarioExiste != null)
        {
            return BadRequest("Usuário já existente!");
        }

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
        return Ok("Administrador cadastrado com sucesso!");
    }
}
