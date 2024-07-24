namespace HospitalAPI.Modelos;

public class Pessoas
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public uint CPF { get; set; }
    public uint Telefone { get; set; }
    public string Endereco { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public int ImagemDocumentoId { get; set; }

    public virtual Imagem ImagemDocumento { get; set; }

    public Pessoas() { }
    public Pessoas(string nomeCompleto, uint cpf, DateOnly dataNascimento, uint telefone, string endereco)
    {
        NomeCompleto = nomeCompleto;
        CPF = cpf;
        Telefone = telefone;
        Endereco = endereco;
        DataNascimento = dataNascimento;
    }
    public void Atualizar(string nomeCompleto, uint cpf, DateOnly dataNascimento, uint telefone, string endereco)
    {
        NomeCompleto = nomeCompleto;
        CPF = cpf;
        Telefone = telefone;
        Endereco = endereco;
        DataNascimento = dataNascimento;
    }
}
