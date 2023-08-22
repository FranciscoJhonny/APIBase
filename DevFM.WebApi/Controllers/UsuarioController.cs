using AutoMapper;
using DevFM.Application.Services;
using DevFM.Domain.Models;
using DevFM.Domain.Services;
using DevFM.WebApi.Dtos;
using DevFM.WebApi.Util;
using Microsoft.AspNetCore.Mvc;

namespace DevFM.WebApi.Controllers
{
    /// <summary>
	/// Controller ms alfabetiza do Avaliacao
	/// </summary>
	[ApiVersion("1.0")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IMapper mapper,
            IUsuarioService UsuarioService,
            ILoggerFactory loggerFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = loggerFactory?.CreateLogger<UsuarioController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _usuarioService = UsuarioService ?? throw new ArgumentNullException(nameof(UsuarioService));
        }
        [HttpGet("usuario/get-lista-usuario")]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsuarioesAsync()
        {
            try
            {
                var usuario = await _usuarioService.ObterUsuarioAsync();

                var response = _mapper.Map<IEnumerable<UsuarioDto>>(usuario);

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet("usuario/get-usuario/usuarioId")]
        [ActionName(nameof(GetUsuarioPorIdAsync))]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsuarioPorIdAsync(int usuarioId)
        {
            try
            {
                var usuario = await _usuarioService.ObterUsuarioPorIdAsync(usuarioId);

                var response = _mapper.Map<UsuarioDto>(usuario);

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("usuario/post-Usuario")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioPostDto usuarioDto)
        {
            if (usuarioDto is null)
                throw new ArgumentNullException(nameof(usuarioDto));

            var usuario = _mapper.Map<Usuario>(usuarioDto);

            var usuarioId = await _usuarioService.NewUsuarioAsync(usuario);

            return CreatedAtAction(nameof(GetUsuarioPorIdAsync), new { usuarioId }, usuarioId);

        }

        /// <summary>
        /// Efetua o login do usuário no MSAlfabetiza
        /// </summary>
        /// <param name="loginUsuario">Parametro do aluno</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost("usuario/login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostLogin([FromBody] LoginUsuarioDto loginUsuario)
        {
            if (loginUsuario is null)
                throw new ArgumentNullException(nameof(loginUsuario));

            var msUsuario = await _usuarioService.LoginUsuario(loginUsuario.Usuario, loginUsuario.Senha);

            if (msUsuario == null)
                return NotFound(new ApiResponse(401, $"Usuario ou senha invalido"));

            //return NotFound(new ApiResponse(401, $"Produto não encontrado.( id ={id}) ")); exemplo

            return Ok(msUsuario);

        }
        [HttpGet("usuario/get-verifica-usuario")]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVerificaUsuarioAsync(string email)
        {
            try
            {
                var exite_usuario = await _usuarioService.VerificaUsuarioAsync(email);



                if (exite_usuario > 0)
                    return NotFound(new ApiResponse(100, null));

                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
