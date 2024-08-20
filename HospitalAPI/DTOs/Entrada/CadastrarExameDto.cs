namespace HospitalAPI.DTOs.Entrada;

public class CadastrarExameDto
{
    public string NomeExame { get; set; } = string.Empty;
    public Guid MedicoId { get; set; }
    public Guid PacienteId { get; set; }
    public DateTime DataAgendamento { get; set; }

}
