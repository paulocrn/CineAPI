using Application.DTOs.Cartelera;
using Application.DTOs.Reserva;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/cartelera")]
    public class CarteleraController : ControllerBase
    {
        private readonly IServicioCartelera _servicioCartelera;

        public CarteleraController(IServicioCartelera servicioCartelera)
        {
            _servicioCartelera = servicioCartelera;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var resultado = await _servicioCartelera.ObtenerTodasAsync();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _servicioCartelera.ObtenerPorIdAsync(id);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearCarteleraDto dto)
        {
            var resultado = await _servicioCartelera.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = resultado.Id }, resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarCarteleraDto dto)
        {
            if (id != dto.Id) return BadRequest("IDs no coinciden");

            await _servicioCartelera.ActualizarAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _servicioCartelera.EliminarAsync(id);
            return NoContent();
        }

        [HttpPost("cancelar/{carteleraId}")]
        public async Task<IActionResult> CancelarCarteleraYReservas(int carteleraId)
        {
            await _servicioCartelera.CancelarCarteleraYReservas(carteleraId);
            return NoContent();
        }

        [HttpGet("reservas-genero")]
        public async Task<IActionResult> ObtenerReservasPorGeneroYRangoFechas([FromQuery] GeneroPelicula genero, [FromQuery] DateTime desde, [FromQuery] DateTime hasta)
        {
            var resultado = await _servicioCartelera.ObtenerReservasPorGeneroYRangoFechas(genero, desde, hasta);
            return Ok(resultado);
        }

        [HttpGet("estadisticas-asientos")]
        public async Task<IActionResult> ObtenerEstadisticasAsientosPorSala([FromQuery] DateTime fecha)
        {
            var resultado = await _servicioCartelera.ObtenerEstadisticasAsientosPorSala(fecha);
            return Ok(resultado);
        }
    }
}
