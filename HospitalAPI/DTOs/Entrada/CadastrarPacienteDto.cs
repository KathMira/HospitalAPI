using HospitalAPI.Modelos;

namespace HospitalAPI.DTOs.Entrada;

public class CadastrarPacienteDto
{

    public float Peso { get; set; }

    public float Altura { get; set; }

    public char Sexo;

    public int TipoSanguineo { get; set; }

    public string Alergias { get; set; } = string.Empty;

    public string HistoricoFamiliar { get; set; } = string.Empty;

    public int? ConvenioId { get; }

    public string NomeCompleto { get; set; } = string.Empty;

    public string CPF { get; set; }

    public string Telefone { get; set; }

    public string Endereco { get; set; } = string.Empty;

    public DateTime DataNascimento { get; set; }

    public IFormFile ImagemDocumento { get; set; }

    public string Senha {  get; set; }

}
