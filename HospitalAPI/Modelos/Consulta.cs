﻿using Azure.Core;
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
    public virtual List<Laudo> Laudos { get; set; }

    public EnumStatusAtendimento Status { get; set; }
    public bool Retorno { get; set; }

    public bool Pago { get; set; }


    public Consulta() { }
    public Consulta(AgendarConsultaDto cadastrarConsultaDto)
    {
        DataAgendamento = cadastrarConsultaDto.DataAgendamento;
        MedicoId = cadastrarConsultaDto.MedicoId;
        PacienteId = cadastrarConsultaDto.PacienteId;
        Retorno = false;
        Pago = false;
        Status = EnumStatusAtendimento.Agendada;

    }

    public Consulta(RealizarConsultaExameDto realizarConsultaDto, int PacienteId, int MedicoId)
    {
        DataAgendamento = realizarConsultaDto.DataFim.AddDays(7);
        Status = EnumStatusAtendimento.Agendada;
        Retorno = true;
        this.MedicoId = MedicoId;
        this.PacienteId = PacienteId;
    }

    public void RealizarPagamento()
    {
        Pago = true;
    }
    public void Realizar(RealizarConsultaExameDto realizarConsultaDto)
    {
        DataInicio = realizarConsultaDto.DataInicio;
        DataFim = realizarConsultaDto.DataFim;
        Status = EnumStatusAtendimento.Concluida;
    }

    public void Cancelar()
    {
        Status = EnumStatusAtendimento.Cancelada;
    }

    public Consulta AgendarRetorno(AgendarRetornoDto agendarRetornoDto)
    {
        Consulta retorno = new Consulta();
        retorno.DataAgendamento = agendarRetornoDto.DataAgendamento;
        retorno.Status = EnumStatusAtendimento.Agendada;
        retorno.Retorno = true;
        retorno.MedicoId = MedicoId;
        retorno.PacienteId = PacienteId;
        return retorno;
    }
}
