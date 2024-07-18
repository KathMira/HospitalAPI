namespace HospitalAPI.DTOs;

public class AgendarConsultaDto
{
    public DateTime DataAgendamento { get; set; }
    public int MedicoId { get; set; }
    public int PacienteId { get; set; }

}
