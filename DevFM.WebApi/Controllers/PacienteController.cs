using AutoMapper;
using DevFM.Domain.Models;
using DevFM.Domain.Services;
using DevFM.WebApi.Dtos;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("paciente/get-lista-paciente")]
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


                foreach (var pacienteDto in response)
                {
                    pacienteDto.Idade = Utilitario.YearsOld(pacienteDto.DataNascimento, DateTime.Now);

                    var listaTelefonesPaciente = await _pacienteService.ObterTelefonesPacienteAsync(pacienteDto.PacienteId);

                    if (listaTelefonesPaciente != null)
                        pacienteDto.TelefonesPaciente = _mapper.Map<IEnumerable<TelefoneDto>>(listaTelefonesPaciente);

                    var listaEnderecoPaciente = await _pacienteService.ObterEnderecoPacienteAsync(pacienteDto.PacienteId);

                    if (listaEnderecoPaciente != null)
                        pacienteDto.EnderecosPaciente = _mapper.Map<IEnumerable<EnderecoDto>>(listaEnderecoPaciente);

                    var listaResponsavelPaciente = await _pacienteService.ObterResponsavelPacienteAsync(pacienteDto.PacienteId);

                    if (listaResponsavelPaciente != null)
                    {
                        pacienteDto.ResponsaveisPaciente = _mapper.Map<IEnumerable<ResponsavelDto>>(listaResponsavelPaciente);

                        foreach (var item in pacienteDto.ResponsaveisPaciente)
                        {
                            var telefoneResponsavel = await _pacienteService.ObterTelefoneResponsavelAsync(item.ResponsavelId);

                            if (telefoneResponsavel != null)
                            {
                                item.TelefonesResponsavel = _mapper.Map<IEnumerable<TelefoneDto>>(telefoneResponsavel);
                            }
                        }
                    }

                    var listaAtendimentoPaciente = await _pacienteService.ObterAtendimentoPacienteAsync(pacienteDto.PacienteId);

                    if (listaAtendimentoPaciente != null)
                    {
                        pacienteDto.AtendimentosPaciente = _mapper.Map<IEnumerable<AtendimentoDto>>(listaAtendimentoPaciente);

                        foreach (var item in pacienteDto.AtendimentosPaciente)
                        {
                            var telefonesCuidador = await _pacienteService.ObterTelefoneCuidadorAsync(item.CuidadorId);
                            if (telefonesCuidador != null)
                                item.TelefonesCuidador = _mapper.Map<IEnumerable<TelefoneDto>>(telefonesCuidador);
                        }
                    }
                    var listaPaciente_Pacote = await _pacienteService.ObterPaciente_PacoteAsync(pacienteDto.PacienteId);

                    if (listaPaciente_Pacote != null)
                        pacienteDto.Paciente_Pacotes = _mapper.Map<IEnumerable<Paciente_PacoteDto>>(listaPaciente_Pacote);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet("paciente/get-lista-paciente-nome")]
        [ProducesResponseType(typeof(PacienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPacientesParametroAsync(int filtro, string nome)
        {
            try
            {
                var Paciente = await _pacienteService.ObterPacienteParametroAsync(filtro, nome);

                var response = _mapper.Map<IEnumerable<PacienteDto>>(Paciente);

                if (response == null)
                    return NotFound();


                foreach (var pacienteDto in response)
                {
                    pacienteDto.Idade = Utilitario.YearsOld(pacienteDto.DataNascimento, DateTime.Now);

                    var listaTelefonesPaciente = await _pacienteService.ObterTelefonesPacienteAsync(pacienteDto.PacienteId);

                    if (listaTelefonesPaciente != null)
                        pacienteDto.TelefonesPaciente = _mapper.Map<IEnumerable<TelefoneDto>>(listaTelefonesPaciente);

                    var listaEnderecoPaciente = await _pacienteService.ObterEnderecoPacienteAsync(pacienteDto.PacienteId);

                    if (listaEnderecoPaciente != null)
                        pacienteDto.EnderecosPaciente = _mapper.Map<IEnumerable<EnderecoDto>>(listaEnderecoPaciente);

                    var listaResponsavelPaciente = await _pacienteService.ObterResponsavelPacienteAsync(pacienteDto.PacienteId);

                    if (listaResponsavelPaciente != null)
                    {
                        pacienteDto.ResponsaveisPaciente = _mapper.Map<IEnumerable<ResponsavelDto>>(listaResponsavelPaciente);

                        foreach (var item in pacienteDto.ResponsaveisPaciente)
                        {
                            var telefoneResponsavel = await _pacienteService.ObterTelefoneResponsavelAsync(item.ResponsavelId);

                            if (telefoneResponsavel != null)
                            {
                                item.TelefonesResponsavel = _mapper.Map<IEnumerable<TelefoneDto>>(telefoneResponsavel);
                            }
                        }
                    }

                    var listaAtendimentoPaciente = await _pacienteService.ObterAtendimentoPacienteAsync(pacienteDto.PacienteId);

                    if (listaAtendimentoPaciente != null)
                    {
                        pacienteDto.AtendimentosPaciente = _mapper.Map<IEnumerable<AtendimentoDto>>(listaAtendimentoPaciente);

                        foreach (var item in pacienteDto.AtendimentosPaciente)
                        {
                            var telefonesCuidador = await _pacienteService.ObterTelefoneCuidadorAsync(item.CuidadorId);
                            if (telefonesCuidador != null)
                                item.TelefonesCuidador = _mapper.Map<IEnumerable<TelefoneDto>>(telefonesCuidador);
                        }


                    }


                    var listaPaciente_Pacote = await _pacienteService.ObterPaciente_PacoteAsync(pacienteDto.PacienteId);

                    if (listaPaciente_Pacote != null)
                        pacienteDto.Paciente_Pacotes = _mapper.Map<IEnumerable<Paciente_PacoteDto>>(listaPaciente_Pacote);
                }




                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //[Authorize(Roles = "Adminstrador")]
        [HttpGet("paciente/get-paciente/{pacienteId}")]
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

                var listaTelefonesPaciente = await _pacienteService.ObterTelefonesPacienteAsync(response.PacienteId);

                if (listaTelefonesPaciente != null)
                    response.TelefonesPaciente = _mapper.Map<IEnumerable<TelefoneDto>>(listaTelefonesPaciente);

                var listaEnderecoPaciente = await _pacienteService.ObterEnderecoPacienteAsync(response.PacienteId);

                if (listaEnderecoPaciente != null)
                    response.EnderecosPaciente = _mapper.Map<IEnumerable<EnderecoDto>>(listaEnderecoPaciente);

                var listaResponsavelPaciente = await _pacienteService.ObterResponsavelPacienteAsync(response.PacienteId);

                if (listaResponsavelPaciente != null)
                {
                    response.ResponsaveisPaciente = _mapper.Map<IEnumerable<ResponsavelDto>>(listaResponsavelPaciente);

                    foreach (var item in response.ResponsaveisPaciente)
                    {
                        var telefoneResponsavel = await _pacienteService.ObterTelefoneResponsavelAsync(item.ResponsavelId);

                        if (telefoneResponsavel != null)
                        {
                            item.TelefonesResponsavel = _mapper.Map<IEnumerable<TelefoneDto>>(telefoneResponsavel);
                        }
                    }
                }

                var listaAtendimentoPaciente = await _pacienteService.ObterAtendimentoPacienteAsync(response.PacienteId);

                if (listaAtendimentoPaciente != null)
                    response.AtendimentosPaciente = _mapper.Map<IEnumerable<AtendimentoDto>>(listaAtendimentoPaciente);

                var listaPaciente_Pacote = await _pacienteService.ObterPaciente_PacoteAsync(response.PacienteId);

                if (listaPaciente_Pacote != null)
                    response.Paciente_Pacotes = _mapper.Map<IEnumerable<Paciente_PacoteDto>>(listaPaciente_Pacote);


                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //[Authorize(Roles = "Adminstrador")]
        [HttpPost("paciente/post-paciente")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostPaciente([FromBody] PacientePostDto pacientePostDto)
        {
            if (pacientePostDto is null)
                throw new ArgumentNullException(nameof(PacientePostDto));

            var paciente = _mapper.Map<Paciente>(pacientePostDto);

            var pacienteId = await _pacienteService.NewPacienteAsync(paciente);

            return CreatedAtAction(nameof(GetPacientePorIdAsync), new { pacienteId }, pacienteId);

        }

        //[Authorize(Roles = "Adminstrador")]
        [HttpPut("paciente/put-paciente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPaciente([FromBody] PacientePutDto pacientePutDto)
        {
            if (pacientePutDto is null)
                throw new ArgumentNullException(nameof(pacientePutDto));

            var paciente = _mapper.Map<Paciente>(pacientePutDto);

            await _pacienteService.UpdatePaciente(paciente);

            return Ok();
        }
    }
}
