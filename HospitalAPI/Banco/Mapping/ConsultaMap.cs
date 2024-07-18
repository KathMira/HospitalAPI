using HospitalAPI.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalAPI.Banco.Mapping;

public class ConsultaMap : IEntityTypeConfiguration<Consultas>
{
    public void Configure(EntityTypeBuilder<Consultas> builder)
    {
        builder.HasOne(x => x.Paciente).WithMany().OnDelete(DeleteBehavior.NoAction)
            ;
        builder.HasOne(x => x.Medico).WithMany().OnDelete(DeleteBehavior.NoAction);


    }
}
