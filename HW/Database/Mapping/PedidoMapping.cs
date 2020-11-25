using HW.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Database.Mapping
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Status).HasColumnType("varchar(100)").IsRequired();

            builder.Property(c => c.Identificador).HasColumnType("varchar(100)").IsRequired();
            
            builder.Property(c => c.ValorFinal).HasColumnType("decimal(5,2)");

            builder.Property(c => c.Observacoes).HasColumnType("varchar(200)");

            builder.HasMany(c => c.Produtos).WithOne(p => p.Pedido).HasForeignKey(c => c.IdPedido);

            builder.ToTable("Pedido");
        }
    }
}
