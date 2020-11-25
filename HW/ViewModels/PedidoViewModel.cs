using HW.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.ViewModels
{
    public class PedidoViewModel
    {
        public int Id { get; set; }
        public StatusPedido Status { get; set; }
        public string StatusFormatada { get {
                switch (this.Status)
                {
                    case StatusPedido.Aberto:
                        return "Aberto";
                    case StatusPedido.Fechado:
                        return "Fechado";
                }

                return string.Empty;
            } 
        }
        public string Identificador { get; set; }

        public string Observacoes { get; set; }

        public decimal? ValorFinal { get; set; }
        public virtual List<ListaProdutoViewModel> Produtos { get; set; }
    }
}
