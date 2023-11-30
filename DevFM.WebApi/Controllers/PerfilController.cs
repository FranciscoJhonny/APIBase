using AutoMapper;
using DevFM.Domain.Services;
using DevFM.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DevFM.WebApi.Controllers
{
    
	[ApiVersion("1.0")]
    public class PerfilController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IPerfilService _PerfilService;

        public PerfilController(IMapper mapper,
            IPerfilService PerfilService,
            ILoggerFactory loggerFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = loggerFactory?.CreateLogger<PerfilController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _PerfilService = PerfilService ?? throw new ArgumentNullException(nameof(PerfilService));
        }
        [HttpGet("get-lista-Perfil")]
        [ProducesResponseType(typeof(PerfilDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPerfilAsync()
        {
            try
            {
                var Perfil = await _PerfilService.ObterPerfilAsync();

                var response = _mapper.Map<IEnumerable<PerfilDto>>(Perfil);

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
