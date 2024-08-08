using HospitalAPI.DTOs.Entrada;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace HospitalAPI.Modelos;

public class Medicamentos
{
    
    public int Id { get; set; }
    public string NomeMedicamento { get; set; } = string.Empty;
    
    public int PacienteId { get; set; }
    public int MedicoId { get; set; }


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
