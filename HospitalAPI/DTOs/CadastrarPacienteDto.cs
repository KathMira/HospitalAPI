using HospitalAPI.Modelos;

namespace HospitalAPI.DTOs;

public class CadastrarPacienteDto
{
   
    public float Peso { get; set; }

    public float Altura { get; set; }

    public char Sexo;

    public int TipoSanguineo { get; set; }

    public string Alergias { get; set; } = string.Empty;

    public string HistoricoFamiliar { get; set; } = string.Empty;

    public bool TemConvenio { get; set; }

    public int IdConvenio { get; }

    public string NomeCompleto { get; set; } = string.Empty;

    public int CPF { get; set; }

    public int Telefone { get; set; }

    public string Endereco { get; set; } = string.Empty;

    public DateTime DataNascimento { get; set; }

    public int IdImagemDocumento { get; set; }

}
