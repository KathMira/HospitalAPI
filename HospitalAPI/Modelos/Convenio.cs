using HospitalAPI.DTOs.Entrada;

namespace HospitalAPI.Modelos;

public class Convenio
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public float Desconto { get; set; }

    public Convenio() { }
    public Convenio(CadastrarConvenioDto cadastrarConvenioDto)
    {
        Nome = cadastrarConvenioDto.Nome;
        Desconto = cadastrarConvenioDto.Desconto;
    }
    public void Atualizar(CadastrarConvenioDto cadastrarConvenioDto)
    {
        Nome = cadastrarConvenioDto.Nome;
        Desconto = cadastrarConvenioDto.Desconto;
    }

}
