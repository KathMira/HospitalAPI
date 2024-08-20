namespace HospitalAPI.DTOs.Entrada;

public class AgendarConsultaDto
{
    public DateTime DataAgendamento { get; set; }
    public Guid MedicoId { get; set; }
    public Guid PacienteId { get; set; }

}
