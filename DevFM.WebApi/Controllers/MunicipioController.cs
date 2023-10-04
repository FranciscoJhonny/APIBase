using AutoMapper;
using DevFM.Domain.Services;
using DevFM.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DevFM.WebApi.Controllers
{
    /// <summary>
	/// Controller Municipio
	/// </summary>
	[ApiVersion("1.0")]
    public class MunicipioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IMunicipioService _municipioService;

        public MunicipioController(IMapper mapper,
            IMunicipioService municipioService,
            ILoggerFactory loggerFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = loggerFactory?.CreateLogger<MunicipioController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _municipioService = municipioService ?? throw new ArgumentNullException(nameof(municipioService));
        }
        [HttpGet("get-lista-municipio")]
        [ProducesResponseType(typeof(MunicipioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMunicipioAsync()
        {
            try
            {
                var municipio = await _municipioService.ObterMunicipioAsync();

                var response = _mapper.Map<IEnumerable<MunicipioDto>>(municipio);

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("get-lista-municipio-estado/estadoId")]
        [ProducesResponseType(typeof(MunicipioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMunicipioPorEstadoIdAsync(int estadoId)
        {
            try
            {
                var municipio = await _municipioService.ObterMunicipioPorEstadoIdAsync(estadoId);

                var response = _mapper.Map<IEnumerable<MunicipioDto>>(municipio);

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
