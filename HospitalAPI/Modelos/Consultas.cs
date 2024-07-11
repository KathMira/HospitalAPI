namespace HospitalAPI.Modelos;

public class Consultas
{
    public int IdConsulta{ get; set; }
    public DateTime DataConsulta { get; set; }
    public int IdMedico { get; }
    public int IdPaciente { get; }
    
}
