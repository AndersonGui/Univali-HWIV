using HW.Database.Context;
using HW.Database.Entities;
using HW.Interfaces.Repositories;
using HW.ViewModels;
using HW.ViewModels.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HW.Database.Repositories
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(HWContext db) : base(db) { }

        public async Task<Pedido> RealizarPedido(Pedido pedido)
        {
            var state = Db.Pedido.Add(pedido);
            Db.SaveChanges();

            pedido.Id = state.Entity.Id;

            return pedido;
        }

        public async Task<Pedido> FinalizarPedido(int idPedido)
        {
            var pedido = await Db.Pedido.AsNoTracking().Include(p => p.Produtos).ThenInclude(pp => pp.Produto).Where(p => p.Id == idPedido).FirstOrDefaultAsync();

            if(pedido == null)
                throw new Exception("Pedido não localizado");

            pedido.ValorFinal = pedido.Produtos.Sum(c => c.Valor);
            pedido.Status = Enumerators.StatusPedido.Fechado;

            Db.Update(pedido);
            await Db.SaveChangesAsync();

            return pedido;
        }

        public virtual async Task<ICollection<Pedido>> FindAll(Expression<Func<Pedido, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Include(p => p.Produtos).ThenInclude(pp => pp.Produto).Where(predicate).ToListAsync();
        }

        public virtual async Task<ICollection<Pedido>> FindAll()
        {
            return await DbSet.Include(p => p.Produtos).ThenInclude(pp => pp.Produto).ToListAsync();
        }

        public virtual async Task<Pedido> Find(Expression<Func<Pedido, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Include(p => p.Produtos).ThenInclude(pp => pp.Produto).Where(predicate).FirstOrDefaultAsync();
        }

    }
}
