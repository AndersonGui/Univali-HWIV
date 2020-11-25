using HW.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public Perfil Perfil { get; set; }
    }
}
