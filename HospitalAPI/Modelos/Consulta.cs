using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Enums;

namespace HospitalAPI.Modelos;

public class Consulta
{
    public int Id { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public DateTime DataAgendamento { get; set; }
    public int MedicoId { get; set; }
    public int PacienteId { get; set; }
    public virtual Medico Medico { get; set; }
    public virtual Paciente Paciente { get; set; }
    public EnumStatusConsulta Status { get; set; }
    public bool Retorno { get; set; }


    public Consulta() { }
    public Consulta(AgendarConsultaDto cadastrarConsultaDto)
    {
        DataAgendamento = cadastrarConsultaDto.DataAgendamento;
        MedicoId = cadastrarConsultaDto.MedicoId;
        PacienteId = cadastrarConsultaDto.PacienteId;
        Retorno = false;
        Status = EnumStatusConsulta.Agendada;

    }
    public Consulta(RealizarConsultaDto realizarConsultaDto, int PacienteId, int MedicoId)
    {
        DataAgendamento = realizarConsultaDto.DataFim.AddDays(7);
        Status = EnumStatusConsulta.Agendada;
        Retorno = true;
        this.MedicoId = MedicoId;
        this.PacienteId = PacienteId;
    }

    public void Realizar(RealizarConsultaDto realizarConsultaDto)
    {
        DataInicio = realizarConsultaDto.DataInicio;
        DataFim = realizarConsultaDto.DataFim;
        Status = EnumStatusConsulta.Concluida;
    }

    public void Cancelar()
    {
        Status = EnumStatusConsulta.Cancelada;
    }

    public Consulta AgendarRetorno(AgendarRetornoDto agendarRetornoDto)
    {
        Consulta retorno = new Consulta();
        retorno.DataAgendamento = agendarRetornoDto.DataAgendamento;
        retorno.Status = EnumStatusConsulta.Agendada;
        retorno.Retorno = true;
        retorno.MedicoId = MedicoId;
        retorno.PacienteId = PacienteId;
        return retorno;
    }
}
