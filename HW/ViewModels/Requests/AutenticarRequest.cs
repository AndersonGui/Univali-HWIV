using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.ViewModels.Requests
{
    public class AutenticarRequest
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }
    }
}
