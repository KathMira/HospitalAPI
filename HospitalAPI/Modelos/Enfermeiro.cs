using HospitalAPI.DTOs.Entrada;
using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Modelos;

public class Enfermeiro
{
    public int Id { get; set; }
    public int PessoaId { get; set; }
    public int SetorId { get; }
    public virtual Pessoas Pessoa { get; set; }

    public Enfermeiro() { }
    public Enfermeiro(CadastrarEnfermeiroDto cadastrarEnfermeiroDto)
    {
        Pessoa = new Pessoas
            (cadastrarEnfermeiroDto.NomeCompleto,
            cadastrarEnfermeiroDto.CPF,
            DateOnly.FromDateTime(cadastrarEnfermeiroDto.DataNascimento),
            cadastrarEnfermeiroDto.Telefone,
            cadastrarEnfermeiroDto.Endereco);
    }

    public void Atualizar(CadastrarEnfermeiroDto cadastrarEnfermeiroDto)
    {
        Pessoa.Atualizar
            (cadastrarEnfermeiroDto.NomeCompleto,
            cadastrarEnfermeiroDto.CPF,
            DateOnly.FromDateTime(cadastrarEnfermeiroDto.DataNascimento),
            cadastrarEnfermeiroDto.Telefone,
            cadastrarEnfermeiroDto.Endereco);
    }
}
