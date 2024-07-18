using HospitalAPI.DTOs;
using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Modelos;

public class Enfermeiro
{
    public int PessoaId { get; set; }
    [Key]
    public int IdEnfermeiro { get; set; }
    public int IdSetor { get; }
    public virtual Pessoas Pessoa { get; set; }

    public Enfermeiro() { }
    public Enfermeiro(CadastrarPessoaDto cadastrarPessoaDto)
    {
        Pessoa = new Pessoas(cadastrarPessoaDto.NomeCompleto, cadastrarPessoaDto.CPF, DateOnly.FromDateTime(cadastrarPessoaDto.DataNascimento), cadastrarPessoaDto.Telefone,
            cadastrarPessoaDto.Endereco);

    }
}
