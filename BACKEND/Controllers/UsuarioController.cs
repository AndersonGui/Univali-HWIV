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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository usuarioRepository;
        protected readonly IMapper mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            this.usuarioRepository = usuarioRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<UsuarioViewModel>> Criar(UsuarioViewModel usuario)
        {
            try
            {
                var userEntity = mapper.Map<Usuario>(usuario);

                userEntity.Login = userEntity.Login;
                userEntity.Senha = BCrypt.Net.BCrypt.HashPassword(userEntity.Senha);

                userEntity = await usuarioRepository.Add(userEntity);

                userEntity.Senha = string.Empty;

                return Ok(mapper.Map<UsuarioViewModel>(userEntity));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro Inesperado");
            }
        }
    }
}