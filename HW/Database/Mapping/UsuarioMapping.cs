using HW.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Database.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Login).HasColumnType("varchar(100)").IsRequired();

            builder.Property(c => c.Senha).HasColumnType("varchar(400)").IsRequired();

            builder.Property(c => c.Perfil).HasColumnType("varchar(100)").IsRequired();

            builder.ToTable("Usuario");

            builder.HasData(new Usuario
            {
                Id = 1,
                Login = "admin",
                Perfil = Enumerators.Perfil.Administrador,
                Senha = BCrypt.Net.BCrypt.HashPassword("admin")
            });
        }
    }
}
