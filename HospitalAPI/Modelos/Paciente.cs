using System.Runtime.ConstrainedExecution;

namespace HospitalAPI.Modelos;

public class Paciente
{
    public int IdPessoas { get; set; }
    public int IdPaciente { get; set; }
    public float Peso { get; set; }

    public float Altura { get; set; }

    public char Sexo;

    public int TipoSanguineo { get; set; }

    public string Alergias { get; set; } = string.Empty;

    public string HistoricoFamiliar { get; set; } = string.Empty;

    public bool TemConvenio { get; set; }

    public int IdConvenio { get; }

}
