using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.DAL.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder
                .Property(u => u.Login)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder
               .Property(u => u.Senha)
               .HasColumnType("nvarchar(max)")
               .IsRequired();

            builder
                .Property(u => u.Perfil)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            //builder
            //    .Property(u => u.Perfil)
            //    .HasColumnType("nvarchar(max)")
            //    .IsRequired()
            //    .HasConversion<string>(
            //        tipo => tipo.TipoPerfilParaString(),
            //        texto => texto.StringParaTipoPerfil()
            //    );
        }
    }
}
