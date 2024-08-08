namespace HospitalAPI.DTOs.Entrada;

public class LaudoQueryDto
{
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFinal {  get; set; }
    public int? PacienteId { get; set; }
    public int? MedicoId { get; set; }
}
