using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.DAL.Configuration
{
    internal class MovimentoBaixaConfiguration : IEntityTypeConfiguration<MovimentoBaixa>
    {
        public void Configure(EntityTypeBuilder<MovimentoBaixa> builder)
        {
            builder
                .Property(mb => mb.Direcao)
                .HasColumnType("nvarchar(20)")
                .IsRequired()
                .HasConversion<string> (
                    tipo => tipo.DirecaoBaixaParaString(),
                    texto => texto.StringParaDirecaoBaixa()
                );
        }
    }
}
