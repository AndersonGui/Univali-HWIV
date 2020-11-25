using HW.Database.Context;
using HW.Database.Entities;
using HW.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Database.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(HWContext db) : base(db) { }
    }
}
