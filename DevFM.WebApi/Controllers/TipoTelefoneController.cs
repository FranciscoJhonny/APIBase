using AutoMapper;
using DevFM.Domain.Services;
using DevFM.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DevFM.WebApi.Controllers
{
    /// <summary>
	/// Controller Tipo Telefone
	/// </summary>
	[ApiVersion("1.0")]
    public class TipoTelefoneController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ITipoTelefoneService _tipoTelefoneService;

        public TipoTelefoneController(IMapper mapper,
            ITipoTelefoneService tipoTelefoneService,
            ILoggerFactory loggerFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = loggerFactory?.CreateLogger<TipoTelefoneController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _tipoTelefoneService = tipoTelefoneService ?? throw new ArgumentNullException(nameof(tipoTelefoneService));
        }
        [HttpGet("get-lista-tipoTelefone")]
        [ProducesResponseType(typeof(TipoTelefoneDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTipoTelefoneAsync()
        {
            try
            {
                var tipoTelefone = await _tipoTelefoneService.ObterTipoTelefoneAsync();

                var response = _mapper.Map<IEnumerable<TipoTelefoneDto>>(tipoTelefone);

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
