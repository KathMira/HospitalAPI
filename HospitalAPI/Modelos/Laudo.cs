using HospitalAPI.DTOs.Entrada;

namespace HospitalAPI.Modelos;

public class Laudo
{
    public int Id { get; set; }
    public string? CID { get; set; }
    public DateTime DataLaudo { get; set; }
    public string NomeLaudo { get; set; } = string.Empty;
    public Guid PacienteId { get; set; }
    public string DescricaoLaudo { get; set; } = string.Empty;
    public int ExameId { get; set; }
    public Guid MedicoId { get; set; }
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
