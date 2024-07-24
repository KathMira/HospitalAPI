﻿namespace HospitalAPI.DTOs.Entrada;

public class CadastrarEnfermeiroDto
{
    public string NomeCompleto { get; set; }
    public string Area { get; set; }
    public uint CPF { get; set; }
    public uint Telefone { get; set; }
    public string Endereco { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public int ImagemDocumentoId { get; set; }
    public IFormFile ImagemDocumento { get; set; }

}
