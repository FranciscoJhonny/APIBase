using AutoMapper;
using DevFM.Domain.Models;
using DevFM.Domain.Services;
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
        public async Task<IActionResult> Auth(string usuario, string senha)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));
            if (senha == null)
                throw new ArgumentNullException(nameof(senha));

            var dbusuario = await _usuarioService.LoginUsuario(usuario, senha);

            if (dbusuario != null)
            {
                var token = TokenService.GenerateToken(dbusuario);
                return Ok(token);
            }

            return BadRequest("usename ou passorwd invalido");
        }
    }
}
