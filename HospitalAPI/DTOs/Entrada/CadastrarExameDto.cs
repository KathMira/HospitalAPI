namespace HospitalAPI.DTOs.Entrada;

public class CadastrarExameDto
{
    public string NomeExame { get; set; } = string.Empty;
    public int MedicoId { get; set; }
    public int PacienteId { get; set; }
    public DateTime DataAgendamento { get; set; }

}
