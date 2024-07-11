using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PacienteController : ControllerBase
{
    [HttpPost]
    public IActionResult CadastroPaciente([FromBody]Paciente paciente)
    {
        return Ok(paciente);
    }

}
