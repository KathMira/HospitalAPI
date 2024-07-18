namespace HospitalAPI.DTOs;

public class CadastrarExameDto
{
    public string NomeExame { get; set; } = string.Empty;
    public int MedicoId { get; }
    public int PacienteId { get; }
    public DateTime DataAgendamento { get; set; }
 
}
