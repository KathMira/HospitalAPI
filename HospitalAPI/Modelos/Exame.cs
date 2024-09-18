using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Enums;

namespace HospitalAPI.Modelos;

public class Exame
{
    public int Id { get; set; }
    public string NomeExame { get; set; } = string.Empty;
    public Guid MedicoId { get; set; }
    public Guid PacienteId { get; set; }
    public DateTime DataAgendamento { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public double ValorExame { get; set; }
    public EnumStatusAtendimento Status {  get; set; }
    public bool Pago { get; set; }
    public virtual Laudo Laudo { get; set; }
    public virtual Paciente Paciente { get; set; }
    public virtual Medico Medico { get; set; }


    public Exame() { }

    public Exame(CadastrarExameDto cadastrarExameDto)
    {
        MedicoId = cadastrarExameDto.MedicoId;
        PacienteId = cadastrarExameDto.PacienteId;
        DataAgendamento = cadastrarExameDto.DataAgendamento;
        Status = EnumStatusAtendimento.Agendada;
        if(Medico!.area.NomeArea == EnumArea.Clinico)
        {
            NomeExame = EnumTiposExames.Hemograma.ToString();
            ValorExame = 200;
        }
        if(Medico!.area.NomeArea == EnumArea.Pediatra)
        {
            NomeExame = EnumTiposExames.Audiometria.ToString();
            ValorExame = 150;
        }
        if(Medico!.area.NomeArea == EnumArea.Endocrinologista)
        {
            NomeExame = EnumTiposExames.Ultrassonografia.ToString();
            ValorExame = 100;
        }
        if(Medico!.area.NomeArea == EnumArea.Cardiologista)
        {
            NomeExame = EnumTiposExames.Ecocardiograma.ToString();
            ValorExame = 100;
        }
        if (Paciente!.TemConvenio == true)
        {
            Pago = true;
        }
    }
    public void RealizarPagamento()
    {
       Pago = true;
    }
    public void Realizar(RealizarConsultaExameDto realizarExameDto)
    {
        DataInicio = realizarExameDto.DataInicio;
        DataFim = realizarExameDto.DataFim;
        Status = EnumStatusAtendimento.Concluida;
    }

    public void Cancelar()
    {
        Status = EnumStatusAtendimento.Cancelada;
    }
}
