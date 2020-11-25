using HW.Database.Context;
using HW.Database.Entities;
using HW.Interfaces.Repositories;
using HW.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Database.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(HWContext db) : base(db) { }

        public async Task<Produto> AtualizarProduto(ProdutoViewModel produto) {
            try
            {
                var entity = await Db.Produto.Where(c => c.Id == produto.Id).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new Exception("Produto não encontrado");
                }

                entity.Nome = produto.Nome;
                entity.Valor = produto.Valor;

                Db.Update(entity);
                await Db.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DesativarProduto(int idProduto) {
            try
            {
                var entity = await Db.Produto.Where(c => c.Id == idProduto).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new Exception("Produto não encontrado");
                }

                entity.Ativo = false;

                Db.Update(entity);
                await Db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
