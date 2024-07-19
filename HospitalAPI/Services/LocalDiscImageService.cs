using HospitalAPI.Enums;
using HospitalAPI.Modelos;

namespace HospitalAPI.Services;

public class LocalDiscImageService : IImagesServices
{
    public Stream PegarImagem(string nome, EnumTiposDocumentos tipo)
    {
        string basepath = @"C:\Users\kmira\Pictures\ImagensAPIHospital";
        string caminhoImagem = Path.Combine(basepath, nome);
        FileStream fileStream = new FileStream(caminhoImagem, FileMode.Open);
        return fileStream;
    }

    public string Salvar(Stream imageStream, EnumTiposDocumentos tipo)
    {
        string basepath = "C:\\Users\\kmira\\Pictures\\ImagensAPIHospital";
        string imageName = Guid.NewGuid().ToString();
        string imagePath = Path.Combine(basepath, imageName);
        using FileStream fileStream = new FileStream(imagePath, FileMode.Create);
        imageStream.CopyTo(fileStream);
        fileStream.Close();
        return imageName;
    }
}
