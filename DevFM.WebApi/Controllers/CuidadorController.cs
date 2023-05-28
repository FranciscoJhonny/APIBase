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
    public class CuidadorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICuidadorService _cuidadorService;

        public CuidadorController(IMapper mapper,
            ICuidadorService cuidadorService,
            ILoggerFactory loggerFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = loggerFactory?.CreateLogger<CuidadorController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _cuidadorService = cuidadorService ?? throw new ArgumentNullException(nameof(cuidadorService));
        }
        [HttpGet("get-lista-cuidador")]
        [ProducesResponseType(typeof(CuidadorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCuidadoresAsync()
        {
            try
            {
                var Cuidador = await _cuidadorService.ObterCuidadorAsync();

                var response = _mapper.Map<IEnumerable<CuidadorDto>>(Cuidador);

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet("get-cuidador/cuidadorId")]
        [ActionName(nameof(GetCuidadorPorIdAsync))]
        [ProducesResponseType(typeof(CuidadorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCuidadorPorIdAsync(int cuidadorId)
        {
            try
            {
                var Cuidador = await _cuidadorService.ObterCuidadorPorIdAsync(cuidadorId);

                var response = _mapper.Map<CuidadorDto>(Cuidador);

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("post-cuidador")]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType( StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostCuidador([FromBody] CuidadorPostDto cuidadorDto)
        {
            if (cuidadorDto is null)
                throw new ArgumentNullException(nameof(cuidadorDto));

            var cuidador = _mapper.Map<Cuidador>(cuidadorDto);

            var cuidadorId = await _cuidadorService.NewCuidadorAsync(cuidador);

            return CreatedAtAction(nameof(GetCuidadorPorIdAsync), new { cuidadorId }, cuidadorId);

        }
    }
}
