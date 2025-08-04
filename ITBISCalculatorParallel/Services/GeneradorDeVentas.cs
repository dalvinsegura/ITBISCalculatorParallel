using ITBISCalculatorParallel.Models;

namespace ITBISCalculatorParallel.Services
{
    public class GeneradorDeVentas
    {
        private static string[] colmados = { "Colmado A", "Colmado B", "Colmado C" };
        private static string[] categorias = { "Bebidas", "Basicos", "Limpieza", "Cigarros" };
        private static Random rnd = new Random();

        public static List<Venta> GenerarVentas(int cantidad)
        {
            var ventas = new List<Venta>();

            for (int i = 0; i < cantidad; i++)
            {
                var venta = new Venta
                {
                    Colmado = colmados[rnd.Next(colmados.Length)],
                    Categoria = categorias[rnd.Next(categorias.Length)],
                    Monto = (decimal)(rnd.NextDouble() * (500 - 100) + 100),
                    Fecha = DateTime.Today
                };

                ventas.Add(venta);
            }

            return ventas;
        }
    }
}
