namespace HospitalAPI.DTOs.Entrada;

public class CadastrarPessoaDto
{
    public string NomeCompleto { get; set; } = string.Empty;

    public int CPF { get; set; }

    public int Telefone { get; set; }

    public string Endereco { get; set; } = string.Empty;

    public DateTime DataNascimento { get; set; }

    public int IdImagemDocumento { get; set; }
}
