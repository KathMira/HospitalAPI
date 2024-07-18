using HospitalAPI.DTOs;

namespace HospitalAPI.Modelos;

public class Consultas
{
    public int Id { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public DateTime DataAgendamento { get; set; }
    public int MedicoId { get; set; }
    public int PacienteId { get; set; }

    public virtual Medico Medico { get; set; }

    public  virtual Paciente Paciente { get; set; }

    public Consultas() { }
    public Consultas(AgendarConsultaDto cadastrarConsultaDto)
    {
        DataAgendamento = cadastrarConsultaDto.DataAgendamento;
        MedicoId = cadastrarConsultaDto.MedicoId;
        PacienteId = cadastrarConsultaDto.PacienteId;
    }

}
