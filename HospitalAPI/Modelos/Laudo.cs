using HospitalAPI.DTOs.Entrada;
using System;

namespace HospitalAPI.Modelos;

public class Laudo
{
    public int Id { get; set; }
    public string? CID { get; set; }
    public DateTime DataLaudo { get; set; }
    public string NomeLaudo { get; set; } = string.Empty;
    public int PacienteId { get; set; }
    public string DescricaoLaudo { get; set; } = string.Empty;
    public int ExameId { get; set; }
    public int MedicoId { get; set; }
    public int ConsultaId { get; set; }


    public Laudo() { }

    public Laudo(CadastrarLaudoDto cadastrarLaudoDto)
    {
        ConsultaId = cadastrarLaudoDto.ConsultaId;
        MedicoId = cadastrarLaudoDto.MedicoId;
        PacienteId = cadastrarLaudoDto.PacienteId;
        ExameId = cadastrarLaudoDto.ExameId;
        CID = cadastrarLaudoDto.CID;
        DataLaudo = cadastrarLaudoDto.DataLaudo;
        NomeLaudo = cadastrarLaudoDto.NomeLaudo;
        DescricaoLaudo = cadastrarLaudoDto.DescricaoLaudo;

    }
}
