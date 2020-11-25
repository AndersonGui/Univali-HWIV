using AutoMapper;
using HW.Interfaces.Repositories;
using HW.Services;
using HW.ViewModels;
using HW.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace HW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioRepository usuarioRepository;
        protected readonly IMapper mapper;

        public AutenticacaoController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            this.usuarioRepository = usuarioRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioViewModel>> Autenticar(AutenticarRequest autenticarRequest)
        {
            try
            {
                var usuario = mapper.Map<UsuarioViewModel>(await usuarioRepository.Find(u => u.Login == autenticarRequest.Login));

                if (usuario == null || !BC.Verify(autenticarRequest.Senha, usuario.Senha))
                {
                    return Unauthorized();
                }

                usuario.Senha = string.Empty;

                return Ok(new { User = usuario, Token = TokenService.GenerateToken(usuario) });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro Inesperado");
            }
        }

        [HttpPost("Verificartoken")]
        [Authorize]
        public async Task<ActionResult<bool>> Verificartoken(AutenticarRequest autenticarRequest)
        {
            try
            {
                return Ok(true);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro Inesperado");
            }
        }
    }
}
