using HW.Database.Entities;
using HW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> AtualizarProduto(ProdutoViewModel produto);
        Task<bool> DesativarProduto(int idProduto);
    }
}
