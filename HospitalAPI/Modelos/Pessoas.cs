namespace HospitalAPI.Modelos;

public class Pessoas
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public int CPF { get; set; }
    public int Telefone { get; set; }
    public string Endereco { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public int ImagemDocumentoId { get; set; }

    public virtual Imagem ImagemDocumento { get; set; }

    public Pessoas() { }
    public Pessoas(string nomeCompleto, int cpf, DateOnly dataNascimento, int telefone, string endereco)
    {
        NomeCompleto = nomeCompleto;
        CPF = cpf;
        Telefone = telefone;
        Endereco = endereco;
        DataNascimento = dataNascimento;
    }
    public void Atualizar(string nomeCompleto, int cpf, DateOnly dataNascimento, int telefone, string endereco)
    {
        NomeCompleto = nomeCompleto;
        CPF = cpf;
        Telefone = telefone;
        Endereco = endereco;
        DataNascimento = dataNascimento;
    }
}
