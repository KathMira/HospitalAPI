namespace HospitalAPI.DTOs.Entrada;

public class CadastrarLaudoDto
{
    public int ConsultaId { get; set; }
    public Guid MedicoId { get; set; }
    public Guid PacienteId { get; set; }
    public int ExameId { get; set; }
    public string? CID { get; set; }
    public DateTime DataLaudo { get; set; }
    public string NomeLaudo { get; set; } = string.Empty;
    public string DescricaoLaudo { get; set; } = string.Empty;
    
}
