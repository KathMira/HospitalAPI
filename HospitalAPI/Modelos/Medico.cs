using HospitalAPI.DTOs.Entrada;
using Microsoft.AspNetCore.Identity;

namespace HospitalAPI.Modelos;

public class Medico
{
    public Guid Id { get; set; }
    public Guid PessoaId { get; set; }
   
    public string CRM { get; set; }
    public string Area { get; set; } = string.Empty;
    public virtual Pessoa Pessoa { get; set; }

    public Medico() { }

    public Medico(CadastrarMedicoDto cadastrarMedicoDto)
    {
        Pessoa = new Pessoa(cadastrarMedicoDto.NomeCompleto, cadastrarMedicoDto.CPF, DateOnly.FromDateTime(cadastrarMedicoDto.DataNascimento),
        cadastrarMedicoDto.Telefone, cadastrarMedicoDto.Endereco);
        CRM = cadastrarMedicoDto.CRM;
        Area = cadastrarMedicoDto.Area;
       
    }
    public void Atualizar(CadastrarMedicoDto cadastrarMedicoDto)
    {
        Pessoa.Atualizar
           (cadastrarMedicoDto.NomeCompleto,
           cadastrarMedicoDto.CPF,
           DateOnly.FromDateTime(cadastrarMedicoDto.DataNascimento),
           cadastrarMedicoDto.Telefone,
           cadastrarMedicoDto.Endereco);

        CRM = cadastrarMedicoDto.CRM;
        Area = cadastrarMedicoDto.Area;
    }
}