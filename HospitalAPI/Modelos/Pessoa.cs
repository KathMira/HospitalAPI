using Microsoft.AspNetCore.Identity;

namespace HospitalAPI.Modelos;

public class Pessoa : IdentityUser<Guid>
{
    public string NomeCompleto { get; set; } = string.Empty;
    public string CPF { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public int ImagemDocumentoId { get; set; }

    public virtual Imagem ImagemDocumento { get; set; }

    public Pessoa() { }
    public Pessoa(string nomeCompleto, string cpf, DateOnly dataNascimento, string telefone, string endereco)
    {
        NomeCompleto = nomeCompleto;
        CPF = cpf;
        Telefone = telefone;
        Endereco = endereco;
        DataNascimento = dataNascimento;
        UserName = CPF;
    }
    public void Atualizar(string nomeCompleto, string cpf, DateOnly dataNascimento, string telefone, string endereco)
    {
        NomeCompleto = nomeCompleto;
        CPF = cpf;
        Telefone = telefone;
        Endereco = endereco;
        DataNascimento = dataNascimento;
    }
}
