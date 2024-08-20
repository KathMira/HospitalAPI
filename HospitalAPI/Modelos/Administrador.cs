using HospitalAPI.DTOs.Entrada;

namespace HospitalAPI.Modelos;

public class Administrador 
{

    public Guid PessoaId { get; set; }
    public Guid Id { get; set; }
    public Pessoa Pessoa { get; set; }

    private Administrador() { }

    public Administrador(CadastrarPessoaDto cadastrarPessoaDto)
    {

        Pessoa = new Pessoa
            (cadastrarPessoaDto.NomeCompleto,
            cadastrarPessoaDto.CPF,
            DateOnly.FromDateTime(cadastrarPessoaDto.DataNascimento),
            cadastrarPessoaDto.Telefone,
            cadastrarPessoaDto.Endereco);
      
    }

}
