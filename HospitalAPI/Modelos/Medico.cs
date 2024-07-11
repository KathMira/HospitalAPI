namespace HospitalAPI.Modelos;

public class Medico
{

    public int IdPessoas { get; }
    public int IdMedico { get; set; }
    public int CRM { get; set; }
    public string Area { get; set; } = string.Empty;

}