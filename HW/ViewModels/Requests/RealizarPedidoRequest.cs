using HW.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.ViewModels.Requests
{
    public class RealizarPedidoRequest
    {
        public int Id { get; set; }
        public StatusPedido Status { get; set; }
        public string Identificador { get; set; }
        public virtual List<int> Produtos { get; set; }
    }
}
