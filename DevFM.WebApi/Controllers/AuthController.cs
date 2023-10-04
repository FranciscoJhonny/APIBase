using AutoMapper;
using DevFM.Domain.Models;
using DevFM.Domain.Services;
using DevFM.WebApi.Dtos;
using DevFM.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevFM.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]

    public class AuthController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(
           IUsuarioService UsuarioService)
        {
            _usuarioService = UsuarioService ?? throw new ArgumentNullException(nameof(UsuarioService));
        }

        [HttpPost("usuario/auth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Auth([FromBody] LoginUsuarioDto loginUsuarioDto)
        {
            if (loginUsuarioDto.Usuario == null)
                throw new ArgumentNullException(nameof(loginUsuarioDto.Usuario));
            if (loginUsuarioDto.Senha== null)
                throw new ArgumentNullException(nameof(loginUsuarioDto.Senha));

            var dbusuario = await _usuarioService.LoginUsuario(loginUsuarioDto.Usuario, loginUsuarioDto.Senha);

            if (dbusuario != null)
            {
                var access_token = TokenService.GenerateToken(dbusuario);
                return Ok(access_token);
            }

            return BadRequest("usename ou passorwd invalido");
        }
    }
}
