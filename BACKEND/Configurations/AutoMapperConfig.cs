using AutoMapper;
using HW.Database.Entities;
using HW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();

            CreateMap<Pedido, PedidoViewModel>().ReverseMap();

            CreateMap<ListaProduto, ListaProdutoViewModel>().ReverseMap();
        }
    }
}
