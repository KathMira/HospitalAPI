namespace HospitalAPI.DTOs.Entrada;

public class CadastrarMedicoDto
{
    public string CRM { get; set; }
    public string Area { get; set; } = string.Empty;
    public string NomeCompleto { get; set; } = string.Empty;
    public string CPF { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }

    public IFormFile ImagemDocumento { get; set; }
    public string Senha { get; set; }
}
