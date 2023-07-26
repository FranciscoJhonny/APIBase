using AutoMapper;
using DevFM.Domain.Services;
using DevFM.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DevFM.WebApi.Controllers
{
    /// <summary>
	/// Controller ms alfabetiza do Avaliacao
	/// </summary>
	[ApiVersion("1.0")]
    public class TurnoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ITurnoService _turnoService;

        public TurnoController(IMapper mapper,
            ITurnoService turnoService,
            ILoggerFactory loggerFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = loggerFactory?.CreateLogger<TurnoController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _turnoService = turnoService ?? throw new ArgumentNullException(nameof(turnoService));
        }
        [HttpGet("turno/get-lista-turno")]
        [ProducesResponseType(typeof(TurnoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTurnoAsync()
        {
            try
            {
                var turno = await _turnoService.ObterTurnoAsync();

                var response = _mapper.Map<IEnumerable<TurnoDto>>(turno);

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
