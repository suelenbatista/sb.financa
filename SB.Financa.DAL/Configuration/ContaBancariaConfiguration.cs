using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.DAL.Configuration
{
    internal class ContaBancariaConfiguration : IEntityTypeConfiguration<ContaBancaria>
    {
        public void Configure(EntityTypeBuilder<ContaBancaria> builder)
        {
            builder
                .Property(p => p.Nome)
                .HasColumnType("nvarchar(150)")
                .IsRequired();

            builder
                .Property(p => p.Agencia)
                .HasColumnType("nvarchar(5)");

            builder
                .Property(p => p.Conta)
                .HasColumnType("nvarchar(5)");

            builder
                .Property(p => p.Digito)
                .HasColumnType("nvarchar(5)");

            builder
                .Property(p => p.Ativa)
                .HasColumnType("bit")
                .IsRequired();
        }
    }
}
