namespace HospitalAPI.DTOs.Entrada;

public class CadastrarMedicoDto
{
    public int CRM { get; set; }
    public string Area { get; set; } = string.Empty;
    public string NomeCompleto { get; set; } = string.Empty;
    public int CPF { get; set; }
    public int Telefone { get; set; }
    public string Endereco { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
   
    public IFormFile ImagemDocumento { get; set; }

}
