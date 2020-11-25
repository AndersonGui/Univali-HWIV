using HW.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Database.Context
{
    public class HWContext : DbContext
    {
        public HWContext(DbContextOptions<HWContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        //public DbSet<PedidoProduto> PedidoProduto { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<ListaProduto> ListaProduto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HWContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
