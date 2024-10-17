using HospitalAPI.DTOs.Entrada;

namespace HospitalAPI.Modelos;

public class Convenio
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public float Desconto { get; set; }

    private Convenio() { }
    public Convenio(CadastrarConvenioDto cadastrarConvenioDto)
    {
        Nome = cadastrarConvenioDto.Nome;
        Desconto = cadastrarConvenioDto.Desconto;
        //Utilizando o método Split, dividimos a string pelos caracteres de espaço retornando um array, depois é verificado ele tem algum tamanho, se não tiver, significa que ele só possui caracteres em branco. 
        var t = Nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (t.Length == 0)
        {
            throw new ApplicationException("O nome do convênio não pode ser em branco.");
        }
        if (Nome.Length < 3)
        {
            throw new ApplicationException("O nome do convênio não pode ter menos de 3 letras.");
        }
        //verifica se Desconto é maior que um ou menor que 0, caso for, joga excessão
        if (Desconto > 1 || Desconto <= 0)
        {
            throw new ApplicationException("O desconto não pode ser menor ou igual a 0 ou maior do que 1.");
        }
    }
    public void Atualizar(CadastrarConvenioDto cadastrarConvenioDto)
    {
        Nome = cadastrarConvenioDto.Nome;
        Desconto = cadastrarConvenioDto.Desconto;
    }

}
