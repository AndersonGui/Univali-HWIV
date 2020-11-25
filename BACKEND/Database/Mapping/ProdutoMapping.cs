using HW.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Database.Mapping
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Nome).HasColumnType("varchar(100)").IsRequired();

            builder.Property(c => c.Valor).HasColumnType("decimal(5,2)").IsRequired();

            builder.Property(c => c.Ativo).IsRequired();

            builder.ToTable("Produto");

            builder.HasData(new Produto
            {
                Id = 1,
                Nome = "Produto 01",
                Valor = 2.99M,
                Ativo = true
            },
            new Produto
            {
                Id = 2,
                Nome = "Produto 02",
                Valor = 5.99M,
                Ativo = true
            },
            new Produto
            {
                Id = 3,
                Nome = "Produto 03",
                Valor = 19.24M,
                Ativo = true
            });
        }
    }
}
