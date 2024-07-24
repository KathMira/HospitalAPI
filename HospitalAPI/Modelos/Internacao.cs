using System;

namespace HospitalAPI.Modelos;

public class Internacao
{
    public int Id { get; set; }
    public int IdPaciente { get; }
    public int IdMedico { get; }
    public int IdExame { get; }
    public int IdLaudo { get; }
    public DateTime DataInternacao { get; set; }
    public int NumeroQuarto { get; set; }
    public string TipoInternacao { get; set; } = string.Empty;
    public int IdEnfermeiro { get; }
}
