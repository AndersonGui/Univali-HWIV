using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.ViewModels
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }

        public bool Ativo { get; set; }
    }
}
