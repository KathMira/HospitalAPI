namespace HospitalAPI.Modelos;

public class Exame
{
    public int IdExame { get; set; }
    public int IdMedico { get; }
    public int IdPaciente { get; }
    public string NomeExame {  get; set; }= string.Empty;
}
