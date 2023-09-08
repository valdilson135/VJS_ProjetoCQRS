﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vjs.Pagamentos.DataRepository;

#nullable disable

namespace Vjs.Pagamentos.DataRepository.Migrations
{
    [DbContext(typeof(PagamentoContext))]
    [Migration("20230908172720_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Vjs.Pagamentos.Business.Pagamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CvvCartao")
                        .IsRequired()
                        .HasColumnType("varchar(4)");

                    b.Property<string>("ExpiracaoCartao")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("NomeCartao")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("NumeroCartao")
                        .IsRequired()
                        .HasColumnType("varchar(16)");

                    b.Property<Guid>("PedidoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Pagamentos", (string)null);
                });

            modelBuilder.Entity("Vjs.Pagamentos.Business.Transacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PagamentoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PedidoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StatusTransacao")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PagamentoId")
                        .IsUnique();

                    b.ToTable("Transacoes", (string)null);
                });

            modelBuilder.Entity("Vjs.Pagamentos.Business.Transacao", b =>
                {
                    b.HasOne("Vjs.Pagamentos.Business.Pagamento", "Pagamento")
                        .WithOne("Transacao")
                        .HasForeignKey("Vjs.Pagamentos.Business.Transacao", "PagamentoId")
                        .IsRequired();

                    b.Navigation("Pagamento");
                });

            modelBuilder.Entity("Vjs.Pagamentos.Business.Pagamento", b =>
                {
                    b.Navigation("Transacao")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}