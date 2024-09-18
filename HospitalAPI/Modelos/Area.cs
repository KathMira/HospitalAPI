using HospitalAPI.Enums;

namespace HospitalAPI.Modelos;

public class Area
{
    public int Id { get; set; }
    public EnumArea NomeArea { get; set; }
    public double ValorConsulta { get; set; }

    public Area() { }

    public Area(int id, EnumArea nomeArea, double valorConsulta)
    {
        Id = id;
        NomeArea = nomeArea;
        ValorConsulta = valorConsulta;
        if (nomeArea == EnumArea.Clinico)
        {
            valorConsulta = 100;
        }
        if(nomeArea == EnumArea.Pediatra)
        {
            valorConsulta = 150;
        }
        if(nomeArea == EnumArea.Endocrinologista)
        {
            valorConsulta = 250;
        }
        if(nomeArea == EnumArea.Cardiologista)
        {
            valorConsulta = 200;
        }
    }
}
