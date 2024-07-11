namespace HospitalAPI.Modelos;

public class Pessoas
{
    public int IdPessoas { get; set; }

    public string NomeCompleto { get; set; } = string.Empty;

    public int CPF { get; set; }

    public int Telefone { get; set; }

    public string Endereco { get; set; } = string.Empty;

    public DateOnly DataNascimento {  get; set; }

    public int IdImagemDocumento { get; set; }
}
