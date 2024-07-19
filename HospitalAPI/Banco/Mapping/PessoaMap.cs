using HospitalAPI.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalAPI.Banco.Mapping;

public class PessoaMap : IEntityTypeConfiguration<Pessoas>
{
    public void Configure(EntityTypeBuilder<Pessoas> builder)
    {
        builder.HasOne(x => x.ImagemDocumento).WithOne().OnDelete(DeleteBehavior.Cascade);
    }
}
