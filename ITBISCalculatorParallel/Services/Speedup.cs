using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITBISCalculatorParallel.Models;
using ITBISCalculatorParallel.Processing;

namespace ITBISCalculatorParallel.Services
{
    public class Speedup
    {
        public async Task Iniciar(int cantidadventa)
        {
            int processors = Environment.ProcessorCount;
            Console.WriteLine($"Procesadores Total: {processors}");
            Console.WriteLine();
            List<Venta> ventas = GeneradorDeVentas.GenerarVentas(cantidadventa);
            foreach (var procesadores in new[] { 1, 4, 8, 16 })
            {
                await EjecutarComparacion(ventas, procesadores);
            }
        }
        private async Task EjecutarComparacion(List<Venta> ventas, int procesadores)
        {
            Console.WriteLine($"Procesadores a usar: {procesadores}");

            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = procesadores
            };

            //Procesamiento Secuencial
            var sw = Stopwatch.StartNew();
            var procesadorSecuencial = new ProcesadorSecuencial();
            procesadorSecuencial.EjecutarSpeedup(ventas);
            sw.Stop();
            long tiempoSecuencial = sw.ElapsedMilliseconds;

            //Procesamiento Paralelo
            sw.Restart();

            var procesadorParalelo = new ProcesadorParaleloConLock();
            await procesadorParalelo.EjecutarSpeedupAsync(ventas);
            sw.Stop();
            long tiempoParalelo = sw.ElapsedMilliseconds;

            double speedup = (double)tiempoSecuencial / tiempoParalelo;
            double eficiencia = speedup / procesadores;

            Console.WriteLine($"Tiempo Secuencial: {tiempoSecuencial} ms");
            Console.WriteLine($"Tiempo Paralelo: {tiempoParalelo} ms");
            Console.WriteLine($"Speedup: {speedup:F2}x");
            Console.WriteLine($"Eficiencia con {procesadores} hilos: {eficiencia:P2}");
            Console.WriteLine();
        }
    }
}

