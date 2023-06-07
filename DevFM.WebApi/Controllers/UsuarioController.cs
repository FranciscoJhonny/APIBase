using AutoMapper;
using DevFM.Domain.Models;
using DevFM.Domain.Services;
using DevFM.WebApi.Dtos;
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
        private readonly IUsuarioService _UsuarioService;

        public UsuarioController(IMapper mapper,
            IUsuarioService UsuarioService,
            ILoggerFactory loggerFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = loggerFactory?.CreateLogger<UsuarioController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _UsuarioService = UsuarioService ?? throw new ArgumentNullException(nameof(UsuarioService));
        }
        [HttpGet("get-lista-usuario")]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsuarioesAsync()
        {
            try
            {
                var usuario = await _UsuarioService.ObterUsuarioAsync();

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
        [HttpGet("get-usuario/usuarioId")]
        [ActionName(nameof(GetUsuarioPorIdAsync))]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsuarioPorIdAsync(int usuarioId)
        {
            try
            {
                var usuario = await _UsuarioService.ObterUsuarioPorIdAsync(usuarioId);

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

        [HttpPost("post-Usuario")]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType( StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioPostDto usuarioDto)
        {
            if (usuarioDto is null)
                throw new ArgumentNullException(nameof(usuarioDto));

            var usuario = _mapper.Map<Usuario>(usuarioDto);

            var usuarioId = await _UsuarioService.NewUsuarioAsync(usuario);

            return CreatedAtAction(nameof(GetUsuarioPorIdAsync), new { usuarioId }, usuarioId);

        }
    }
}
