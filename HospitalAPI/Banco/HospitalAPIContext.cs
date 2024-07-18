using HospitalAPI.Banco.Mapping;
using HospitalAPI.Modelos;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Banco;

public class HospitalAPIContext : DbContext
{
    public HospitalAPIContext(DbContextOptions<HospitalAPIContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ConsultaMap());
        builder.ApplyConfiguration(new ExameMap());
    }


    public DbSet<Pessoas> Pessoas { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Medico> Medicos { get; set; }
    public DbSet<Enfermeiro> Enfermeiros { get; set; }
    public DbSet<Consultas> Consultas { get; set; }
    public DbSet<Exame> Exames { get; set; }

}
