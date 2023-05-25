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
    public class EstadoCivilController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IEstadoCivilService _estadoCivilService;

        public EstadoCivilController(IMapper mapper,
            IEstadoCivilService estadoCivilService,
            ILoggerFactory loggerFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = loggerFactory?.CreateLogger<EstadoCivilController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _estadoCivilService = estadoCivilService ?? throw new ArgumentNullException(nameof(estadoCivilService));
        }
        [HttpGet("get-lista-estadoCivil")]
        [ProducesResponseType(typeof(EstadoCivilDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEstadoCivilAsync()
        {
            try
            {
                var estadoCivil = await _estadoCivilService.ObterEstadoCivilAsync();

                var response = _mapper.Map<IEnumerable<EstadoCivilDto>>(estadoCivil);

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
