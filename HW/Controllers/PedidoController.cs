using AutoMapper;
using HW.Database.Entities;
using HW.Interfaces.Repositories;
using HW.ViewModels;
using HW.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository repository;
        protected readonly IMapper mapper;

        public PedidoController(IPedidoRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> ListarAbertos()
        {
            try
            {
                var pedidos = mapper.Map<List<PedidoViewModel>>(await repository.FindAll(p => p.Status == Enumerators.StatusPedido.Aberto));

                foreach (var pedido in pedidos)
                {
                    pedido.ValorFinal = pedido.Produtos.Sum(p => p.Valor);
                }

                return Ok(pedidos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro Inesperado");
            }
        }

        [HttpPost("RealizarPedido")]
        [Authorize]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> RealizarPedido(PedidoViewModel pedidoViewModel)
        {
            try
            {
                var pedido = mapper.Map<Pedido>(pedidoViewModel);
                var pedidoEntity = await repository.RealizarPedido(pedido);

                return Ok(mapper.Map<PedidoViewModel>(await repository.Find(p => p.Id == pedidoEntity.Id)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro Inesperado");
            }
        }

        [HttpPost("FinalizarPedido")]
        [Authorize]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> FinalizarPedido(PedidoViewModel pedido)
        {
            try
            {
                var pedidoEntity = await repository.FinalizarPedido(pedido.Id);

                return Ok(mapper.Map<PedidoViewModel>(pedidoEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro Inesperado");
            }
        }
    }
}
