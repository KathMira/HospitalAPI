namespace HospitalAPI.DTOs.Entrada;

    public class CadastrarMedicamentoDto
{
    public string NomeMedicamento { get; set; } = string.Empty;
    public Guid PacienteId { get; set; }
    public Guid MedicoId { get; set; }

}

