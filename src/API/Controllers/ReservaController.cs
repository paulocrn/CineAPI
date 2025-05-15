using Application.DTOs.Reserva;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/reserva")]
    public class ReservaController : ControllerBase
    {
        private readonly IServicioReserva _servicioReserva;

        public ReservaController(IServicioReserva servicioReserva)
        {
            _servicioReserva = servicioReserva;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var reservas = await _servicioReserva.ObtenerTodasAsync();
            return Ok(reservas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var reserva = await _servicioReserva.ObtenerPorIdAsync(id);
            if (reserva == null) return NotFound();
            return Ok(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearReservaDto dto)
        {
            var nueva = await _servicioReserva.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nueva.Id }, nueva);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarReservaDto dto)
        {
            if (id != dto.Id) return BadRequest("ID en URL no coincide con el del cuerpo");

            await _servicioReserva.ActualizarAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _servicioReserva.EliminarAsync(id);
            return NoContent();
        }

        [HttpGet("por-cliente/{clienteId}")]
        public async Task<IActionResult> ObtenerPorCliente(int clienteId)
        {
            var reservas = await _servicioReserva.ObtenerPorClienteAsync(clienteId);
            return Ok(reservas);
        }

        [HttpGet("por-cartelera/{carteleraId}")]
        public async Task<IActionResult> ObtenerPorCartelera(int carteleraId)
        {
            var reservas = await _servicioReserva.ObtenerPorCarteleraAsync(carteleraId);
            return Ok(reservas);
        }

        [HttpGet("terror")]
        public async Task<IActionResult> ObtenerReservasDeTerrorPorRango([FromQuery] DateTime desde, [FromQuery] DateTime hasta)
        {
            var resultado = await _servicioReserva.ObtenerReservasDeTerrorPorRangoDeFechas(desde, hasta);
            return Ok(resultado);
        }
    }
}
