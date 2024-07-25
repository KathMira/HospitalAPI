using System;

namespace HospitalAPI.Modelos;

public class Laudos
{
    public int Id { get; set; }
    public int CID { get; set; }
    public DateTime DataLaudo { get; set; }
    public string NomeLaudo { get; set; } = string.Empty;
    public int PacienteId { get; }
    public string DescricaoLaudo { get; set; } = string.Empty;
    public int ExameId { get; }
    public int MedicoId { get; }

}
