using AutoMapper;
using DevFM.Domain.Services;
using DevFM.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DevFM.WebApi.Controllers
{
    
	[ApiVersion("1.0")]
    public class CategoriaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(IMapper mapper,
            ICategoriaService categoriaService,
            ILoggerFactory loggerFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = loggerFactory?.CreateLogger<CategoriaController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _categoriaService = categoriaService ?? throw new ArgumentNullException(nameof(categoriaService));
        }
        [HttpGet("get-lista-categoria")]
        [ProducesResponseType(typeof(CategoriaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoriaAsync()
        {
            try
            {
                var categoria = await _categoriaService.ObterCategoriaAsync();

                var response = _mapper.Map<IEnumerable<CategoriaDto>>(categoria);

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
