using Moq;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using AutoMapper;
using Application.Exceptions;

namespace UnitTests.Services
{
    public class ServicioAsientoTests
    {
        private readonly Mock<IRepositorioAsiento> _mockRepoAsiento = new();
        private readonly Mock<IRepositorioReserva> _mockRepoReserva = new();
        private readonly Mock<IUnidadDeTrabajo> _mockUnidadDeTrabajo = new();
        private readonly Mock<IMapper> _mockMapper = new();

        [Fact]
        public async Task InhabilitarAsientoYCancelarReserva_AsientoYReservaExisten_RealizaTransaccionCorrectamente()
        {
            // Arrange
            var asiento = new Asiento
            {
                Id = 1,
                Numero = 5,
                Fila = 2,
                SalaId = 10,
                Sala = default!,
                Status = true,
                Reservas = new List<Reserva>()
            };

            var reserva = new Reserva
            {
                Id = 2,
                FechaReserva = DateTime.Today,
                ClienteId = 1,
                Cliente = default!,
                AsientoId = 1,
                Asiento = asiento,
                CarteleraId = 1,
                Cartelera = default!
            };

            _mockRepoAsiento.Setup(r => r.ObtenerPorIdAsync(asiento.Id)).ReturnsAsync(asiento);
            _mockRepoReserva.Setup(r => r.ObtenerPorIdAsync(reserva.Id)).ReturnsAsync(reserva);

            var servicio = new ServicioAsiento(
                _mockRepoAsiento.Object,
                _mockRepoReserva.Object,
                _mockUnidadDeTrabajo.Object,
                _mockMapper.Object
            );

            // Act
            await servicio.InhabilitarAsientoYCancelarReserva(asiento.Id, reserva.Id);

            // Assert
            _mockUnidadDeTrabajo.Verify(u => u.IniciarTransaccionAsync(CancellationToken.None), Times.Once);
            _mockRepoAsiento.Verify(r => r.ActualizarAsync(It.Is<Asiento>(a => a.Status == false)), Times.Once);
            _mockRepoReserva.Verify(r => r.EliminarAsync(reserva), Times.Once);
            _mockUnidadDeTrabajo.Verify(u => u.ConfirmarTransaccionAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task InhabilitarAsientoYCancelarReserva_AsientoNoExiste_LanzaExcepcion()
        {
            _mockRepoAsiento.Setup(r => r.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync((Asiento?)null);

            var servicio = new ServicioAsiento(
                _mockRepoAsiento.Object,
                _mockRepoReserva.Object,
                _mockUnidadDeTrabajo.Object,
                _mockMapper.Object
            );

            await Assert.ThrowsAsync<EntidadNoEncontradaException>(() =>
                servicio.InhabilitarAsientoYCancelarReserva(1, 2));
        }

        [Fact]
        public async Task InhabilitarAsientoYCancelarReserva_ReservaNoExiste_LanzaExcepcion()
        {
            var asiento = new Asiento
            {
                Id = 1,
                Numero = 5,
                Fila = 2,
                SalaId = 10,
                Sala = default!,
                Status = true,
                Reservas = new List<Reserva>()
            };

            _mockRepoAsiento.Setup(r => r.ObtenerPorIdAsync(asiento.Id)).ReturnsAsync(asiento);
            _mockRepoReserva.Setup(r => r.ObtenerPorIdAsync(It.IsAny<int>())).ReturnsAsync((Reserva?)null);

            var servicio = new ServicioAsiento(
                _mockRepoAsiento.Object,
                _mockRepoReserva.Object,
                _mockUnidadDeTrabajo.Object,
                _mockMapper.Object
            );

            await Assert.ThrowsAsync<EntidadNoEncontradaException>(() =>
                servicio.InhabilitarAsientoYCancelarReserva(1, 2));
        }
    }
}
