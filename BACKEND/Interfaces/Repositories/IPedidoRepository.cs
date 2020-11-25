using HW.Database.Entities;
using HW.ViewModels;
using HW.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Interfaces.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<Pedido> RealizarPedido(Pedido realizarPedido);

        Task<Pedido> FinalizarPedido(int idPedido);
    }
}
