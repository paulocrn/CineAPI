using Application.DTOs.Asiento;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Exceptions;

namespace API.Controllers
{
    [ApiController]
    [Route("api/asientos")]
    public class AsientoController : ControllerBase
    {
        private readonly IServicioAsiento _servicioAsiento;
        private readonly ILogger<AsientoController> _logger;

        public AsientoController(IServicioAsiento servicioAsiento, ILogger<AsientoController> logger)
        {
            _servicioAsiento = servicioAsiento;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AsientoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerTodos()
        {
            var asientos = await _servicioAsiento.ObtenerTodosAsync();
            return Ok(asientos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AsientoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var asiento = await _servicioAsiento.ObtenerPorIdAsync(id);
            return Ok(asiento);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AsientoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Crear([FromBody] CrearAsientoDto dto)
        {
            var creado = await _servicioAsiento.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarAsientoDto dto)
        {
            if (id != dto.Id) 
                throw new ReglaDeNegocioException("El ID del cuerpo no coincide con el de la URL");
            
            await _servicioAsiento.ActualizarAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _servicioAsiento.EliminarAsync(id);
            return NoContent();
        }

        [HttpGet("por-sala/{salaId}")]
        [ProducesResponseType(typeof(IEnumerable<AsientoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerPorSala(int salaId)
        {
            var asientos = await _servicioAsiento.ObtenerPorSalaAsync(salaId);
            return Ok(asientos);
        }

        [HttpGet("por-dia")]
        [ProducesResponseType(typeof(IEnumerable<AsientosPorSalaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerAsientosPorSalaDelDia()
        {
            var resultado = await _servicioAsiento.ObtenerAsientosPorSalaDelDiaAsync();
            return Ok(resultado);
        }

        [HttpPost("inhabilitar-y-cancelar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InhabilitarYCancelar([FromQuery] int asientoId, [FromQuery] int reservaId)
        {
            await _servicioAsiento.InhabilitarAsientoYCancelarReserva(asientoId, reservaId);
            return NoContent();
        }
    }
}