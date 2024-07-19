using HospitalAPI.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalAPI.Banco.Mapping;

public class PacienteMap : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.HasOne(x => x.Convenio).WithMany().OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.ConvenioId);
    }
}

