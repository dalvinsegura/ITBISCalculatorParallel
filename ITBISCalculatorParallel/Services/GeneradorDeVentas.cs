using ITBISCalculatorParallel.Models;

namespace ITBISCalculatorParallel.Services
{
    public class GeneradorDeVentas
    {
        private static Random rnd = new Random();

        public static List<Venta> GenerarVentas(int cantidad)
        {
            var ventas = new List<Venta>();

            for (int i = 0; i < cantidad; i++)
            {
                var venta = new Venta
                {
                    Colmado = DatosGenerales.colmados[rnd.Next(DatosGenerales.colmados.Length)],
                    Categoria = DatosGenerales.categorias[rnd.Next(DatosGenerales.categorias.Length)],
                    Monto = (decimal)(rnd.NextDouble() * (500 - 100) + 100),
                    Fecha = DateTime.Today
                };

                ventas.Add(venta);
            }

            return ventas;
        }
    }
}
