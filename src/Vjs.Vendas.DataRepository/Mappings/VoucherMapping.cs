﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vjs.Vendas.Domain;

namespace Vjs.Vendas.DataRepository.Mappings
{
    public class VoucherMapping : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(c => c.Id);


            builder.Property(c => c.Codigo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            // 1 : N => Voucher : Pedidos
            builder.HasMany(c => c.Pedidos)
                .WithOne(c => c.Voucher)
                .HasForeignKey(c => c.VoucherId).IsRequired(false);

            builder.ToTable("Vouchers");
        }
    }
}