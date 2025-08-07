using System.Diagnostics;
using ITBISCalculatorParallel.Models;

namespace ITBISCalculatorParallel.Processing
{
    public class ProcesadorParaleloConLock
    {
        private const decimal TASA_ITBIS = 0.18m;

        private decimal totalVentasCompartido = 0;
        private decimal totalITBISCompartido = 0;
        private readonly object candado = new();

        public async Task EjecutarAsync(List<Venta> ventas)
        {
            // List<Venta> ventas = GeneradorDeVentas.GenerarVentas(1_000_000);
            int umbral = 10000;

            var sw = Stopwatch.StartNew();
            await CalcularTotalesConLock(ventas, 0, ventas.Count - 1, umbral);
            sw.Stop();

            Console.WriteLine($"[Con Lock] Total Ventas: {totalVentasCompartido:C}");
            Console.WriteLine($"[Con Lock] Total ITBIS: {totalITBISCompartido:C}");
            Console.WriteLine($"[Con Lock] Tiempo: {sw.ElapsedMilliseconds} ms");
        }

        public async Task EjecutarSpeedupAsync(List<Venta> ventas)
        {
            // List<Venta> ventas = GeneradorDeVentas.GenerarVentas(1_000_000);
            int umbral = 10000;

            var sw = Stopwatch.StartNew();
            await CalcularTotalesConLock(ventas, 0, ventas.Count - 1, umbral);
            sw.Stop();

        }

        private async Task CalcularTotalesConLock(List<Venta> ventas, int inicio, int fin, int umbral)
        {
            int cantidad = fin - inicio + 1;

            if (cantidad <= umbral)
            {
                decimal subtotal = 0;
                decimal subITBIS = 0;

                for (int i = inicio; i <= fin; i++)
                {
                    subtotal += ventas[i].Monto;
                    subITBIS += ventas[i].Monto * TASA_ITBIS;
                }

                // Proteccion del recurso compartido
                lock (candado)
                {
                    totalVentasCompartido += subtotal;
                    totalITBISCompartido += subITBIS;
                }

                return;
            }

            int medio = (inicio + fin) / 2;

            var tareaIzq = Task.Run(() => CalcularTotalesConLock(ventas, inicio, medio, umbral));
            var tareaDer = Task.Run(() => CalcularTotalesConLock(ventas, medio + 1, fin, umbral));

            await Task.WhenAll(tareaIzq, tareaDer);
        }
    }
}
