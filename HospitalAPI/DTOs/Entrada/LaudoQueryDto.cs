namespace HospitalAPI.DTOs.Entrada;

public class LaudoQueryDto
{
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFinal {  get; set; }
    public Guid PacienteId { get; set; }
    public Guid MedicoId { get; set; }
}
