using HospitalAPI.Enums;

namespace HospitalAPI.Modelos;

public class Imagem
{
    public int ImagemId { get; set; }
    public Guid NomeImagem { get; set; }
    public EnumTiposDocumentos TipoImagem { get; set; }

    public Imagem( Guid nomeImagem, EnumTiposDocumentos tipoImagem)
    {
        NomeImagem = nomeImagem;
        TipoImagem = tipoImagem;
    }
}
