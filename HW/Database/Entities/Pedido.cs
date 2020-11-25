using HW.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Database.Entities
{
    public class Pedido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public StatusPedido Status { get; set; }
        public string Identificador { get; set; }

        public decimal? ValorFinal { get; set; }

        public string Observacoes { get; set; }

        public virtual List<ListaProduto> Produtos { get; set; }
    }
}
