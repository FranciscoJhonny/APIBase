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
    public class PacienteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IPacienteService _pacienteService;

        public PacienteController(IMapper mapper,
            IPacienteService PacienteService,
            ILoggerFactory loggerFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = loggerFactory?.CreateLogger<PacienteController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _pacienteService = PacienteService ?? throw new ArgumentNullException(nameof(PacienteService));
        }
        [HttpGet("get-lista-paciente")]
        [ProducesResponseType(typeof(PacienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPacienteesAsync()
        {
            try
            {
                var Paciente = await _pacienteService.ObterPacienteAsync();

                var response = _mapper.Map<IEnumerable<PacienteDto>>(Paciente);

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet("get-Paciente/pacienteId")]
        [ActionName(nameof(GetPacientePorIdAsync))]
        [ProducesResponseType(typeof(PacienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPacientePorIdAsync(int pacienteId)
        {
            try
            {
                var Paciente = await _pacienteService.ObterPacientePorIdAsync(pacienteId);

                var response = _mapper.Map<PacienteDto>(Paciente);

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("post-paciente")]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType( StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostPaciente([FromBody] PacientePostDto pacientePostDto)
        {
            if (pacientePostDto is null)
                throw new ArgumentNullException(nameof(PacientePostDto));

            var paciente = _mapper.Map<Paciente>(pacientePostDto);

            var pacienteId = await _pacienteService.NewPacienteAsync(paciente);

            return CreatedAtAction(nameof(GetPacientePorIdAsync), new { pacienteId }, pacienteId);

        }
    }
}
