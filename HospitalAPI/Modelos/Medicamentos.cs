namespace HospitalAPI.Modelos;

public class Medicamentos
{
    public int Id { get; set; }
    public string NomeMedicamento { get; set; } = string.Empty;
    public int PacienteId { get; set; }
    public int MedicoId { get; set; }


}
