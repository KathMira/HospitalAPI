namespace HospitalAPI.DTOs.Entrada;

    public class CadastrarMedicamentoDto
{
    public string NomeMedicamento { get; set; } = string.Empty;
    public int PacienteId { get; set; }
    public int MedicoId { get; set; }

}

