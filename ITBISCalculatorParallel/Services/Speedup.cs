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
        public async Task<List<ResultadoSpeedup>> Iniciar(int cantidadventa)
        {
            int processors = Environment.ProcessorCount;
            List<Venta> ventas = GeneradorDeVentas.GenerarVentas(cantidadventa);
            var resultados = new List<ResultadoSpeedup>();
            
            foreach (var procesadores in new[] { 1, 4, 8, 16 })
            {
                var resultado = await EjecutarComparacion(ventas, procesadores);
                resultados.Add(resultado);
            }
            
            return resultados;
        }
        
        private async Task<ResultadoSpeedup> EjecutarComparacion(List<Venta> ventas, int procesadores)
        {
            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = procesadores
            };

            //Procesamiento Secuencial
            var procesadorSecuencial = new ProcesadorSecuencial();
            long tiempoSecuencial = procesadorSecuencial.EjecutarSpeedup(ventas);

            //Procesamiento Paralelo
            var procesadorParalelo = new ProcesadorParaleloConLock();
            long tiempoParalelo = await procesadorParalelo.EjecutarSpeedupAsync(ventas);

            double speedup = (double)tiempoSecuencial / tiempoParalelo;
            double eficiencia = speedup / procesadores;

            return new ResultadoSpeedup(
                tiempoSecuencial,
                tiempoParalelo,
                speedup,
                eficiencia,
                procesadores
            );
        }
    }
}
