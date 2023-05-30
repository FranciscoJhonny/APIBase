using AutoMapper;
using DevFM.Domain.Models;
using DevFM.Domain.Services;
using DevFM.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DevFM.WebApi.Controllers
{
    /// <summary>
	/// Controller pacote
	/// </summary>
	[ApiVersion("1.0")]
    public class PacoteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IPacoteService _pacoteService;

        public PacoteController(IMapper mapper,
            IPacoteService pacoteService,
            ILoggerFactory loggerFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = loggerFactory?.CreateLogger<PacoteController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _pacoteService = pacoteService ?? throw new ArgumentNullException(nameof(pacoteService));
        }
        [HttpGet("get-lista-pacote")]
        [ProducesResponseType(typeof(PacoteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPacoteesAsync()
        {
            try
            {
                var Pacote = await _pacoteService.ObterPacoteAsync();

                var response = _mapper.Map<IEnumerable<PacoteDto>>(Pacote);

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet("get-Pacote/pacoteId")]
        [ActionName(nameof(GetPacotePorIdAsync))]
        [ProducesResponseType(typeof(PacoteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPacotePorIdAsync(int pacoteId)
        {
            try
            {
                var pacote = await _pacoteService.ObterPacotePorIdAsync(pacoteId);

                var response = _mapper.Map<PacoteDto>(pacote);

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("post-pacote")]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType( StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostPacote([FromBody] PacotePostDto pacotePostDto)
        {
            if (pacotePostDto is null)
                throw new ArgumentNullException(nameof(pacotePostDto));

            var pacote = _mapper.Map<Pacote>(pacotePostDto);

            var pacoteId = await _pacoteService.NewPacoteAsync(pacote);

            return CreatedAtAction(nameof(GetPacotePorIdAsync), new { pacoteId }, pacoteId);

        }

        /// <summary>
        /// Editar pacote
        /// </summary>
        /// <param name="pacotePostDto">Parametro do aluno</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPut("put-aluno")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPacote([FromBody] PacoteUpdateDto pacotePostDto)
        {
            if (pacotePostDto is null)
                throw new ArgumentNullException(nameof(pacotePostDto));

            var msAluno = _mapper.Map<Pacote>(pacotePostDto);

            await _pacoteService.UpdatePacote(msAluno);

            return Ok();
        }
    }
}
