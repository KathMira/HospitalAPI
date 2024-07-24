using HospitalAPI.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalAPI.Banco.Mapping;

public class ConsultaMap : IEntityTypeConfiguration<Consulta>
{

    public void Configure(EntityTypeBuilder<Consulta> builder)
    {

        builder.HasOne(x => x.Paciente).WithMany().OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Medico).WithMany().OnDelete(DeleteBehavior.NoAction);


    }
}
