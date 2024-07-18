using HospitalAPI.DTOs;

namespace HospitalAPI.Modelos;

public class Exame
{
    public int Id { get; set; }
    public string NomeExame { get; set; } = string.Empty;
    public int MedicoId { get; set; }
    public int PacienteId { get; set; }
    public DateTime DataAgendamento { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }

    public virtual Paciente Paciente { get; set; }
    public virtual Medico Medico { get; set; }


    public Exame() { }

    public Exame(CadastrarExameDto cadastrarExameDto)
    {
        MedicoId = cadastrarExameDto.MedicoId;
        PacienteId = cadastrarExameDto.PacienteId;
        DataAgendamento = cadastrarExameDto.DataAgendamento;
    

    }
}
