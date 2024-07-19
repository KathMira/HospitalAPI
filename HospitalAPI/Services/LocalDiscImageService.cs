using HospitalAPI.Enums;

namespace HospitalAPI.Services;

public class LocalDiscImageService : IImagesServices
{
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
