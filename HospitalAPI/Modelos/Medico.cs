using HospitalAPI.DTOs.Entrada;

namespace HospitalAPI.Modelos;

public class Medico
{

    public int PessoaId { get; set; }
    public int Id { get; set; }
    public int CRM { get; set; }
    public string Area { get; set; } = string.Empty;
    public virtual Pessoas Pessoa { get; set; }

    public Medico() { }

    public Medico(CadastrarMedicoDto cadastrarMedicoDto)
    {
        Pessoa = new Pessoas(cadastrarMedicoDto.NomeCompleto, cadastrarMedicoDto.CPF, DateOnly.FromDateTime(cadastrarMedicoDto.DataNascimento),
        cadastrarMedicoDto.Telefone, cadastrarMedicoDto.Endereco);
        CRM = cadastrarMedicoDto.CRM;
        Area = cadastrarMedicoDto.Area;
    }
}