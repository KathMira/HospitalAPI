using HospitalAPI.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalAPI.Banco.Mapping;

public class MedicamentoMap : IEntityTypeConfiguration<Medicamentos>
{
    public void Configure(EntityTypeBuilder<Medicamentos> builder)
    {
        //builder.HasOne(x => x.Paciente).WithMany().HasForeignKey(x => x.PacienteId).OnDelete(DeleteBehavior.Cascade);
    }
}
