using HospitalAPI.DTOs.Entrada;

namespace HospitalAPI.Modelos;

public class Enfermeiro
{

    public Guid Id { get; set; }
    public Guid PessoaId { get; set; }
    public int SetorId { get; }
    public virtual Pessoa Pessoa { get; set; }

    private Enfermeiro() { }
    public Enfermeiro(CadastrarEnfermeiroDto cadastrarEnfermeiroDto)
    {
        Pessoa = new Pessoa
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
