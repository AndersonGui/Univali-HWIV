using HW.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Database.Mapping
{
    public class ListaProdutoMapping : IEntityTypeConfiguration<ListaProduto>
    {
        public void Configure(EntityTypeBuilder<ListaProduto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Quantidade).IsRequired(true);

            builder.Property(c => c.Valor).HasColumnType("decimal(5,2)");

            builder.HasOne(c => c.Produto).WithMany(c => c.Pedidos).HasForeignKey(c => c.IdProduto);

            builder.ToTable("ListaProduto");
        }
    }
}
