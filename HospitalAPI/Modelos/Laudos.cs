using System;

namespace HospitalAPI.Modelos;

public class Laudos
{
    public int IdLaudo { get; set; }
    public int CID { get; set; }
    public DateTime DataLaudo { get; set; }
    public string NomeLaudo { get; set; } = string.Empty;
    public int IdPaciente { get; }
    public string DescricaoLaudo { get; set; } = string.Empty;
    public int IdConsulta { get; }
    public int IdExame { get; }
    public int IdMedico { get; }

}
