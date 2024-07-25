using HospitalAPI.DTOs.Entrada;
using HospitalAPI.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Services;

public interface IImagesServices
{
    string Salvar(Stream imageStream, EnumTiposDocumentos tipo);
    Stream PegarImagem(string nome, EnumTiposDocumentos tipo);
   
}

