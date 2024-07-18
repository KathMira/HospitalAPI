using HospitalAPI.DTOs;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace HospitalAPI.Modelos;

public class Paciente
{
    public int PessoaId { get; set; }

    public int Id { get; set; }

    public float Peso { get; set; }

    public float Altura { get; set; }

    public char Sexo;

    public int TipoSanguineo { get; set; }

    public string Alergias { get; set; } = string.Empty;

    public string HistoricoFamiliar { get; set; } = string.Empty;

    public bool TemConvenio { get; set; }

    public int IdConvenio { get; }

    public virtual Pessoas Pessoa { get; set; }

    public Paciente() { }
    public Paciente(CadastrarPacienteDto cadastrarPacienteDto)
    {
        Pessoa = new Pessoas(cadastrarPacienteDto.NomeCompleto, cadastrarPacienteDto.CPF, DateOnly.FromDateTime(cadastrarPacienteDto.DataNascimento),
            cadastrarPacienteDto.Telefone, cadastrarPacienteDto.Endereco);
        Peso = cadastrarPacienteDto.Peso;
        Altura = cadastrarPacienteDto.Altura;
        Sexo = cadastrarPacienteDto.Sexo;
        TipoSanguineo = cadastrarPacienteDto.TipoSanguineo;
        Alergias = cadastrarPacienteDto.Alergias;
        HistoricoFamiliar = cadastrarPacienteDto.HistoricoFamiliar;
        TemConvenio = cadastrarPacienteDto.TemConvenio;

    }
    public void Atualizar(CadastrarPacienteDto cadastrarPacienteDto)
    {
        Pessoa.Atualizar(cadastrarPacienteDto.NomeCompleto, cadastrarPacienteDto.CPF, DateOnly.FromDateTime(cadastrarPacienteDto.DataNascimento), cadastrarPacienteDto.Telefone, cadastrarPacienteDto.Endereco);
        Peso = cadastrarPacienteDto.Peso;
        Altura = cadastrarPacienteDto.Altura;
        Sexo = cadastrarPacienteDto.Sexo;
        TipoSanguineo = cadastrarPacienteDto.TipoSanguineo;
        Alergias = cadastrarPacienteDto.Alergias;
        HistoricoFamiliar = cadastrarPacienteDto.HistoricoFamiliar;
        TemConvenio = cadastrarPacienteDto.TemConvenio;
    }
}
