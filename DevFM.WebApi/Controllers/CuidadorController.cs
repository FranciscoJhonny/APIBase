using AutoMapper;
using DevFM.Domain.Models;
using DevFM.Domain.Services;
using DevFM.WebApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFM.WebApi.Controllers
{

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
        [HttpGet("cuidador/get-lista-cuidador")]
        [ProducesResponseType(typeof(CuidadorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCuidadoresAsync()
        {
            try
            {
                var Cuidador = await _cuidadorService.ObterCuidadorAsync();
                var listaCuidadorDTO = _mapper.Map<IEnumerable<CuidadorDto>>(Cuidador);

                foreach (var item in listaCuidadorDTO)
                {
                    var listaTelefonesCuidador = await _cuidadorService.ObterTelefonesCuidadorAsync(item.CuidadorId);
                    item.TelefonesCuidador = _mapper.Map<IEnumerable<TelefoneDto>>(listaTelefonesCuidador);
                }

                var response = listaCuidadorDTO;

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("cuidador/get-lista-cuidador-nome")]
        [ProducesResponseType(typeof(CuidadorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCuidadoresParametroAsync(int filtro, string nome)
        {
            try
            {
                //filtro = 0 todos
                //filtro = 1 começa com
                //filtro  = 2 contem 
                var Cuidador = await _cuidadorService.ObterCuidadorNomeAsync(filtro, nome);
                var listaCuidadorDTO = _mapper.Map<IEnumerable<CuidadorDto>>(Cuidador);

                foreach (var item in listaCuidadorDTO)
                {
                    var listaTelefonesCuidador = await _cuidadorService.ObterTelefonesCuidadorAsync(item.CuidadorId);
                    item.TelefonesCuidador = _mapper.Map<IEnumerable<TelefoneDto>>(listaTelefonesCuidador);
                }

                var response = listaCuidadorDTO;

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //[Authorize(Roles = "Adminstrador")]
        [HttpGet("cuidador/get-cuidador/{cuidadorId}")]
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

                var listaTelefonesCuidador = await _cuidadorService.ObterTelefonesCuidadorAsync(response.CuidadorId);
                response.TelefonesCuidador = _mapper.Map<IEnumerable<TelefoneDto>>(listaTelefonesCuidador);

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //[Authorize(Roles = "Adminstrador")]
        [HttpPost("cuidador/post-cuidador")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostCuidador([FromBody] CuidadorPostDto cuidadorDto)
        {
            if (cuidadorDto is null)
                throw new ArgumentNullException(nameof(cuidadorDto));

            var cuidador = _mapper.Map<Cuidador>(cuidadorDto);

            var cuidadorId = await _cuidadorService.NewCuidadorAsync(cuidador);

            return CreatedAtAction(nameof(GetCuidadorPorIdAsync), new { cuidadorId }, cuidadorId);

        }

        /// <summary>
        /// Editar cuidador
        /// </summary>
        /// <param name="cuidadorDto">Parametro do cuidador</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        //[Authorize(Roles = "Adminstrador")]
        [HttpPut("cuidador/put-cuidador")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutCuidador([FromBody] CuidadorPutDto cuidadorDto)
        {
            if (cuidadorDto is null)
                throw new ArgumentNullException(nameof(cuidadorDto));

            var cuidador = _mapper.Map<Cuidador>(cuidadorDto);

            await _cuidadorService.UpdateCuidador(cuidador);

            return Ok();
        }
    }
}
