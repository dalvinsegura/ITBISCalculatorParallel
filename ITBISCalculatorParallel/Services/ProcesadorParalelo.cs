using System.Diagnostics;
using ITBISCalculatorParallel.Models;

// esta solucion no implementa el poder compartir datos entre tareas.
// La razon: cada tarea puede trabajar independientemente. Brinda: escalabilidad y seguridad.
namespace ITBISCalculatorParallel.Processing
{
    public class ProcesadorParalelo
    {
        private const decimal TASA_ITBIS = 0.18m;
        public async Task EjecutarAsync(List<Venta> ventas)
        {
            // List<Venta> ventas = GeneradorDeVentas.GenerarVentas(1_000_000); // 1 millon de ventas
            int umbral = 10_000_000;

            Stopwatch sw = Stopwatch.StartNew();
            var resultado = await CalcularTotalesParalelo(ventas, 0, ventas.Count - 1, umbral);
            sw.Stop();

            Console.WriteLine($"[Paralelo] Total Ventas: {resultado.TotalVentas:C}");
            Console.WriteLine($"[Paralelo] Total ITBIS: {resultado.TotalITBIS:C}");
            Console.WriteLine($"[Paralelo] Tiempo: {sw.ElapsedMilliseconds} ms");
        }

        public record Resultado(decimal TotalVentas, decimal TotalITBIS);

        private async Task<Resultado> CalcularTotalesParalelo(List<Venta> ventas, int inicio, int fin, int umbral)
        {
            int cantidad = fin - inicio + 1;

            if (cantidad <= umbral)
            {
                decimal total = 0;
                decimal itbis = 0;

                for (int i = inicio; i <= fin; i++)
                {
                    total += ventas[i].Monto;
                    itbis += ventas[i].Monto * TASA_ITBIS;
                }

                return new Resultado(total, itbis);
            }

            int medio = (inicio + fin) / 2;

            var tareaIzquierda = Task.Run(() => CalcularTotalesParalelo(ventas, inicio, medio, umbral));
            var tareaDerecha = Task.Run(() => CalcularTotalesParalelo(ventas, medio + 1, fin, umbral));

            var resultados = await Task.WhenAll(tareaIzquierda, tareaDerecha);

            decimal sumaTotal = resultados[0].TotalVentas + resultados[1].TotalVentas;
            decimal sumaITBIS = resultados[0].TotalITBIS + resultados[1].TotalITBIS;

            return new Resultado(sumaTotal, sumaITBIS);
        }
    }
}
