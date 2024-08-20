using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Modelos;
using HospitalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HospitalAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{

    private readonly UserManager<Pessoa> _userManager;
    private readonly JwtTokenService _jwtTokenService;

    public LoginController( UserManager<Pessoa> userManager, JwtTokenService jwtTokenService)
    {

        _userManager= userManager;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("Administrador")]
    public async Task<IActionResult> LoginAdmin([FromBody]LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Cpf);
        if (user == null)
        {
            return BadRequest("Verifique o usuário e tente novamente.");
        }
        var senhaValida = await _userManager.CheckPasswordAsync(user, loginDto.Senha);
        if (senhaValida == false)
        {
            return BadRequest("Verifique a senha e tente novamente.");
        }
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginDto.Cpf),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, Roles.Administrador)
        };
        var token = _jwtTokenService.GetToken(authClaims);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(tokenString);


    }

    [HttpPost("Paciente")]
    public async Task<IActionResult> LoginPaciente(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Cpf);
        if (user == null)
        {
            return BadRequest("Verifique o usuário e tente novamente.");
        }
        var senhaValida = await _userManager.CheckPasswordAsync(user, loginDto.Senha);
        if (senhaValida == false)
        {
            return BadRequest("Verifique a senha e tente novamente.");
        }
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginDto.Cpf),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, Roles.Paciente)
        };
        var token = _jwtTokenService.GetToken(authClaims);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(tokenString);
    }

    [HttpPost("Medico")]
    public async Task<IActionResult> LoginMedico(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Cpf);
        if (user == null)
        {
            return BadRequest("Verifique o usuário e tente novamente.");
        }
        var senhaValida = await _userManager.CheckPasswordAsync(user, loginDto.Senha);
        if (senhaValida == false)
        {
            return BadRequest("Verifique a senha e tente novamente.");
        }
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginDto.Cpf),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, Roles.Medico)
        };
        var token = _jwtTokenService.GetToken(authClaims);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(tokenString);
    }
    [HttpGet]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Pega()
    {
        return Ok("isso isso isso");
    }
}
