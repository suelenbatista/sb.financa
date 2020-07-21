using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.DAL.Configuration
{
    internal class PlanejamentoConfiguration : IEntityTypeConfiguration<Planejamento>
    {
        public void Configure(EntityTypeBuilder<Planejamento> builder)
        {
            builder
                .Property(p => p.Meta)
                .HasColumnType("nvarchar(150)")
                .IsRequired();

            builder
                .Property(p => p.DataInicial)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(p => p.DataFinal)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(p => p.Tipo)
                .HasColumnType("nvarchar(30)")
                .IsRequired()
                .HasConversion<string>(
                    tipo => tipo.TipoPlanejamentoParaString(),
                    texto => texto.StringParaTipoPlanejamento()
                 );


            builder
                .Property(p => p.Valor)
                .HasColumnType("decimal(15,2)")
                .IsRequired();
        }
    }
}
