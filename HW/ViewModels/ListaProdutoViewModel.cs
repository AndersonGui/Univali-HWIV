using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.ViewModels
{
    public class ListaProdutoViewModel
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }

        public int IdProduto { get; set; }

        public int Quantidade { get; set; }

        public string Observacoes { get; set; }

        public decimal Valor { get; set; }

        public virtual ProdutoViewModel Produto { get; set; }

        //public virtual Pedido Pedido { get; set; }
    }
}