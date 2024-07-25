﻿// <auto-generated />
using System;
using HospitalAPI.Banco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HospitalAPI.Migrations
{
    [DbContext(typeof(HospitalAPIContext))]
    [Migration("20240725153616_Pagamento")]
    partial class Pagamento
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HospitalAPI.Modelos.Consulta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataAgendamento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("MedicoId")
                        .HasColumnType("int");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.Property<bool>("Pago")
                        .HasColumnType("bit");

                    b.Property<bool>("Retorno")
                        .HasColumnType("bit");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicoId");

                    b.HasIndex("PacienteId");

                    b.ToTable("Consultas");
                });

            modelBuilder.Entity("HospitalAPI.Modelos.Convenio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Desconto")
                        .HasColumnType("real");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Convenios");
                });

            modelBuilder.Entity("HospitalAPI.Modelos.Enfermeiro", b =>
                {
                    b.Property<int>("IdEnfermeiro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEnfermeiro"));

                    b.Property<int>("PessoaId")
                        .HasColumnType("int");

                    b.HasKey("IdEnfermeiro");

                    b.HasIndex("PessoaId");

                    b.ToTable("Enfermeiros");
                });

            modelBuilder.Entity("HospitalAPI.Modelos.Exame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataAgendamento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("MedicoId")
                        .HasColumnType("int");

                    b.Property<string>("NomeExame")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicoId");

                    b.HasIndex("PacienteId");

                    b.ToTable("Exames");
                });

            modelBuilder.Entity("HospitalAPI.Modelos.Imagem", b =>
                {
                    b.Property<int>("ImagemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImagemId"));

                    b.Property<Guid>("NomeImagem")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TipoImagem")
                        .HasColumnType("int");

                    b.HasKey("ImagemId");

                    b.ToTable("Imagens");
                });

            modelBuilder.Entity("HospitalAPI.Modelos.Medico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CRM")
                        .HasColumnType("int");

                    b.Property<int>("PessoaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.ToTable("Medicos");
                });

            modelBuilder.Entity("HospitalAPI.Modelos.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Alergias")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Altura")
                        .HasColumnType("real");

                    b.Property<int?>("ConvenioId")
                        .HasColumnType("int");

                    b.Property<string>("HistoricoFamiliar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Peso")
                        .HasColumnType("real");

                    b.Property<int>("PessoaId")
                        .HasColumnType("int");

                    b.Property<bool>("TemConvenio")
                        .HasColumnType("bit");

                    b.Property<int>("TipoSanguineo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConvenioId");

                    b.HasIndex("PessoaId");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("HospitalAPI.Modelos.Pessoas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CPF")
                        .HasColumnType("int");

                    b.Property<DateOnly>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ImagemDocumentoId")
                        .HasColumnType("int");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefone")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ImagemDocumentoId");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("HospitalAPI.Modelos.Consulta", b =>
                {
                    b.HasOne("HospitalAPI.Modelos.Medico", "Medico")
                        .WithMany()
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HospitalAPI.Modelos.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Medico");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("HospitalAPI.Modelos.Enfermeiro", b =>
                {
                    b.HasOne("HospitalAPI.Modelos.Pessoas", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("HospitalAPI.Modelos.Exame", b =>
                {
                    b.HasOne("HospitalAPI.Modelos.Medico", "Medico")
                        .WithMany()
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HospitalAPI.Modelos.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Medico");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("HospitalAPI.Modelos.Medico", b =>
                {
                    b.HasOne("HospitalAPI.Modelos.Pessoas", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("HospitalAPI.Modelos.Paciente", b =>
                {
                    b.HasOne("HospitalAPI.Modelos.Convenio", "Convenio")
                        .WithMany()
                        .HasForeignKey("ConvenioId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("HospitalAPI.Modelos.Pessoas", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Convenio");

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("HospitalAPI.Modelos.Pessoas", b =>
                {
                    b.HasOne("HospitalAPI.Modelos.Imagem", "ImagemDocumento")
                        .WithMany()
                        .HasForeignKey("ImagemDocumentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImagemDocumento");
                });
#pragma warning restore 612, 618
        }
    }
}