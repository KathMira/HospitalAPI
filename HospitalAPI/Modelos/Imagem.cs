using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Enums;
using HospitalAPI.Services;

namespace HospitalAPI.Modelos;

public class Imagem
{
    public IImagesServices _imagesServices;

    public int ImagemId { get; set; }
    public Guid NomeImagem { get; set; }
    public EnumTiposDocumentos TipoImagem { get; set; }

    public Imagem(Guid nomeImagem, EnumTiposDocumentos tipoImagem)
    {
        NomeImagem = nomeImagem;
        TipoImagem = tipoImagem;
    }
    public Imagem(CadastrarPacienteDto cadastrarPacienteDto)
    {

        string nomeImagem = _imagesServices.Salvar(cadastrarPacienteDto.ImagemDocumento.OpenReadStream(),
                   Enums.EnumTiposDocumentos.DocumentoIdentificacao);


    }
}
