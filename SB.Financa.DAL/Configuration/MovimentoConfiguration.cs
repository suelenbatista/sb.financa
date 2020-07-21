using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.DAL.Configuration
{
    internal class MovimentoConfiguration : IEntityTypeConfiguration <Movimento>
    {
        public void Configure(EntityTypeBuilder<Movimento> builder)
        {
              builder
                .Property(p => p.Tipo)
                .HasColumnType("nvarchar(15)")
                .IsRequired()
                .HasConversion<string>(
                    tipo => tipo.TipoMovimentoParaString(),
                    texto => texto.StringParaTipoMovimento()
                 );

            builder
                .Property(p => p.Status)
                .HasColumnType("nvarchar(15)")
                .IsRequired()
                .HasConversion<string>(
                    tipo => tipo.StatusMovimentoParaString(),
                    texto => texto.StringParaStatusMovimento()
                );
        }
    }
}
