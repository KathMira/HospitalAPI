using HospitalAPI.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalAPI.Banco.Mapping;

public class EnfermeiroMap : IEntityTypeConfiguration<Enfermeiro>
{
    public void Configure(EntityTypeBuilder<Enfermeiro> builder)
    {
        builder.HasOne(x => x.Pessoa).WithOne().HasForeignKey<Enfermeiro>(x => x.PessoaId);
    }
}
