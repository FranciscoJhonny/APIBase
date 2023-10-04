using AutoMapper;
using DevFM.Domain.Services;
using DevFM.WebApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFM.WebApi.Controllers
{
    /// <summary>
	/// Controller Estado
	/// </summary>
	[ApiVersion("1.0")]
    public class EstadoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IEstadoService _estadoService;

        public EstadoController(IMapper mapper,
            IEstadoService estadoService,
            ILoggerFactory loggerFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = loggerFactory?.CreateLogger<EstadoController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _estadoService = estadoService ?? throw new ArgumentNullException(nameof(estadoService));
        }
        [HttpGet("get-lista-estado")]        
        [ProducesResponseType(typeof(EstadoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEstadoAsync()
        {
            try
            {
                var estado = await _estadoService.ObterEstadoAsync();

                var response = _mapper.Map<IEnumerable<EstadoDto>>(estado);

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
