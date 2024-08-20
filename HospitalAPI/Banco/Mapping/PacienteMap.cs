using HospitalAPI.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalAPI.Banco.Mapping;

public class PacienteMap : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.HasOne(x => x.Convenio).WithMany().OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.ConvenioId);
       builder.HasMany(x => x.Medicamentos).WithOne().HasForeignKey(x => x.PacienteId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Pessoa).WithOne().HasForeignKey<Paciente>(x => x.PessoaId);
    }
}

