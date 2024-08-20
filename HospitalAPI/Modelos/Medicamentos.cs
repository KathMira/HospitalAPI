using HospitalAPI.DTOs.Entrada;

namespace HospitalAPI.Modelos;

public class Medicamentos
{
    
    public int Id { get; set; }
    public string NomeMedicamento { get; set; } = string.Empty;
    
    public Guid PacienteId { get; set; }
    public Guid MedicoId { get; set; }


    public Medicamentos() { }

    public Medicamentos(CadastrarMedicamentoDto cadastrarMedicamentoDto)
    {
        NomeMedicamento = cadastrarMedicamentoDto.NomeMedicamento;
        PacienteId = cadastrarMedicamentoDto.PacienteId;
        MedicoId = cadastrarMedicamentoDto.MedicoId;
    }

    public void Atualizar(CadastrarMedicamentoDto cadastrarMedicamentoDto)
    {
        NomeMedicamento = cadastrarMedicamentoDto.NomeMedicamento;
        PacienteId = cadastrarMedicamentoDto.PacienteId;
        MedicoId = cadastrarMedicamentoDto.MedicoId;
    }
}
