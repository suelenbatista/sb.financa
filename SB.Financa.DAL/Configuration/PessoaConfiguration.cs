using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.DAL.Configuration
{
    internal class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder
                .Property(p => p.Nome)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder
                .Property(p => p.Tipo)
                .HasColumnType("varchar(10)")
                .IsRequired()
                .HasConversion<string>(
                    tipo => tipo.TipoDocParaString(),
                    texto => texto.StringParaTipoDoc()
                 );

            builder
                .Property(p => p.Documento)
                .HasColumnType("nvarchar(20)");

            builder
                .Property(p => p.Ativo)
                .HasColumnType("bit")
                .IsRequired();
        }
    }
}
