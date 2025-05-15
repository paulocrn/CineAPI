using Domain.Entities;

namespace Infrastructure.Seed;

public static class FakeAsientoGenerator
{
    public static List<Asiento> Generate(int salaId, int cantidadFilas = 5, int asientosPorFila = 10)
    {
        var asientos = new List<Asiento>();

        for (short fila = 1; fila <= cantidadFilas; fila++)
        {
            for (short numero = 1; numero <= asientosPorFila; numero++)
            {
                asientos.Add(new Asiento
                {
                    Fila = fila,
                    Numero = numero,
                    SalaId = salaId,
                    Status = true,
                    Reservas = null!
                });
            }
        }

        return asientos;
    }
}