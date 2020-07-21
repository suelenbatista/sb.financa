using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.DAL.Configuration
{
    internal class ContaCartaoConfiguration : IEntityTypeConfiguration<ContaCartao>
    {
        public void Configure(EntityTypeBuilder<ContaCartao> builder)
        {
            builder
                .Property(c => c.Apelido)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder
                .Property(c => c.Numero)
                .HasColumnType("nvarchar(16)");

            builder
                .Property(c => c.Bandeira)
                .HasColumnType("nvarchar(20)")
                .IsRequired()
                .HasConversion<string>(
                    bandeira => bandeira.BandeiraCartaoParaString(),
                    texto => texto.StringParaBandeiraCartao()
                 );

            builder
                .Property(l => l.Tipo)
                .HasColumnType("nvarchar(15)")
                .IsRequired()
                .HasConversion<string>(
                    tipo => tipo.TipoCartaoParaString(),
                    texto => texto.StringParaTipoCartao()
                );
        }
    }
}
