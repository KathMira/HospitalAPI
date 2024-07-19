using HospitalAPI.Enums;

namespace HospitalAPI.Services;

public interface IImagesServices
{
    string Salvar(Stream imageStream, EnumTiposDocumentos tipo);
    Stream PegarImagem(string nome, EnumTiposDocumentos tipo);
}

