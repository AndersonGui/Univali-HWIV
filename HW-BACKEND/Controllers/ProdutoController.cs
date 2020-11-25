using AutoMapper;
using HW.Database.Entities;
using HW.Interfaces.Repositories;
using HW.ViewModels;
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
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository repository;
        protected readonly IMapper mapper;

        public ProdutoController(IProdutoRepository repository, IMapper mapper)
        {
           this.repository = repository;
           this.mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<List<ProdutoViewModel>>> Listar()
        {
            try
            {
                var produtos = mapper.Map<ICollection<ProdutoViewModel>>(await repository.FindAll(p => p.Ativo));

                return Ok(produtos.OrderBy(p => p.Nome).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro Inesperado");
            }
        }

        [HttpGet("{idProduto}")]
        [Authorize]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<List<ProdutoViewModel>>> BuscarPorId(int idProduto)
        {
            try
            {
                var produto = mapper.Map<ProdutoViewModel>(await repository.FindById(idProduto));

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro Inesperado");
            }
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<ProdutoViewModel>> Criar(ProdutoViewModel produto)
        {
            try
            {
                produto.Ativo = true;
                var produtoInserido = mapper.Map<ProdutoViewModel>(await repository.Add(mapper.Map<Produto>(produto)));
                return Ok(produtoInserido);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro Inesperado");
            }
        }

        [HttpPut]
        [Authorize]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Atualizar(ProdutoViewModel produto)
        {
            try
            {
                var produtoEntity = await repository.AtualizarProduto(produto);

                return Ok(mapper.Map<ProdutoViewModel>(produtoEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro Inesperado");
            }
        }

        [HttpDelete]
        [Authorize]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Deletar([FromQuery]int idProduto)
        {
            try
            {
                await repository.DesativarProduto(idProduto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro Inesperado");
            }
        }
    }
}
