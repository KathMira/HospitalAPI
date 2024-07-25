using HospitalAPI.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalAPI.Banco.Mapping;

public class ExameMap : IEntityTypeConfiguration<Exame>
{
    public void Configure(EntityTypeBuilder<Exame> builder)
    {
        builder.HasOne(x => x.Paciente).WithMany().OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Medico).WithMany().OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Laudo).WithOne().HasForeignKey<Laudo>(x => x.ExameId).OnDelete(DeleteBehavior.NoAction);
    }
}
