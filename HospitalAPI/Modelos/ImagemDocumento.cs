namespace HospitalAPI.Modelos;

public class ImagemDocumento
{
    public int IdImagemDocumento { get; set; }

    public string NomeImagem { get; set; }= string.Empty;

    public string CaminhoImagem { get; set; }= string.Empty;

    public int TamanhoImagem { get; set; }
    public string TipoImagem { get; set; }= string.Empty;


}
