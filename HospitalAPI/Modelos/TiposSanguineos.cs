using HospitalAPI.Enums;
using System.Runtime.CompilerServices;

namespace HospitalAPI.Modelos;

public class TiposSanguineos
{
    public static List<string> ListaTipos = ["O+","O-","A+","A-","B+","B-","AB+","AB-"];
    
    public string this[EnumTiposSanguineos enumTiposSanguineos]
    {
        get => ListaTipos[(int)enumTiposSanguineos];
    }
}
