using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Database.Entities
{
    public class ListaProduto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdPedido { get; set; }

        public int IdProduto { get; set; }

        public int Quantidade { get; set; }

        public decimal Valor { get; set; }

        public virtual Produto Produto { get; set; }

        public virtual Pedido Pedido { get; set; }
    }
}
