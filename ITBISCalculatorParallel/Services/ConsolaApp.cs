using ITBISCalculatorParallel.Models;
using ITBISCalculatorParallel.Processing;
using ITBISCalculatorParallel.Services;

namespace ITBISCalculatorParallel.Services
{
    public class ConsolaApp
    {
        private const int CANTIDAD_VENTAS_DEFAULT = 1_000_000;
        
        public async Task Iniciar()
        {
            Console.WriteLine("=== CALCULADORA DE ITBIS CON RECURSIVIDAD PARALELA ===");
            
            while (true)
            {
                MostrarMenu();
                var opcion = Console.ReadLine();
                
                switch (opcion)
                {
                    case "1":
                        await ProbarProcesadorSecuencial();
                        break;
                    case "2":
                        await ProbarProcesadorParalelo();
                        break;
                    case "3":
                        await ProbarProcesadorParaleloConLock();
                        break;
                    case "4":
                        await ProbarSpeedup();
                        break;
                    case "5":
                        await ProbarTodos();
                        break;
                    case "6":
                        Console.WriteLine("Saliendo de la aplicacipn...");
                        return;
                    default:
                        Console.WriteLine("Opcion no valida. Intente nuevamente.");
                        break;
                }
                
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        
        private void MostrarMenu()
        {
            Console.WriteLine("\nMENU PRINCIPAL");
            Console.WriteLine("1. Probar Procesador Secuencial");
            Console.WriteLine("2. Probar Procesador Paralelo");
            Console.WriteLine("3. Probar Procesador Paralelo con Lock");
            Console.WriteLine("4. Probar Comparacion de Speedup");
            Console.WriteLine("5. Probar Todos los Procesadores");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opcion: ");
        }
        
        private async Task ProbarProcesadorSecuencial()
        {
            Console.Write($"\nIngrese cantidad de ventas (default {CANTIDAD_VENTAS_DEFAULT:N0}): ");
            var input = Console.ReadLine();
            var cantidad = string.IsNullOrWhiteSpace(input) ? CANTIDAD_VENTAS_DEFAULT : long.Parse(input);
            
            var ventas = GeneradorDeVentas.GenerarVentas(cantidad);
            var procesador = new ProcesadorSecuencial();
            
            Console.WriteLine($"\nProcesando {cantidad:N0} ventas de forma secuencial...");
            var resultado = procesador.Ejecutar(ventas);
            
            MostrarResultado(resultado);
        }
        
        private async Task ProbarProcesadorParalelo()
        {
            Console.Write($"\nIngrese cantidad de ventas (default {CANTIDAD_VENTAS_DEFAULT:N0}): ");
            var input = Console.ReadLine();
            var cantidad = string.IsNullOrWhiteSpace(input) ? CANTIDAD_VENTAS_DEFAULT : int.Parse(input);
            
            var ventas = GeneradorDeVentas.GenerarVentas(cantidad);
            var procesador = new ProcesadorParalelo();
            
            Console.WriteLine($"\nProcesando {cantidad:N0} ventas en paralelo...");
            var resultado = await procesador.EjecutarAsync(ventas);
            
            MostrarResultado(resultado);
        }
        
        private async Task ProbarProcesadorParaleloConLock()
        {
            Console.Write($"\nIngrese cantidad de ventas (default {CANTIDAD_VENTAS_DEFAULT:N0}): ");
            var input = Console.ReadLine();
            var cantidad = string.IsNullOrWhiteSpace(input) ? CANTIDAD_VENTAS_DEFAULT : int.Parse(input);
            
            var ventas = GeneradorDeVentas.GenerarVentas(cantidad);
            var procesador = new ProcesadorParaleloConLock();
            
            Console.WriteLine($"\nProcesando {cantidad:N0} ventas en paralelo con Lock...");
            var resultado = await procesador.EjecutarAsync(ventas);
            
            MostrarResultado(resultado);
        }
        
        private async Task ProbarSpeedup()
        {
            Console.Write($"\nIngrese cantidad de ventas (default {CANTIDAD_VENTAS_DEFAULT:N0}): ");
            var input = Console.ReadLine();
            var cantidad = string.IsNullOrWhiteSpace(input) ? CANTIDAD_VENTAS_DEFAULT : int.Parse(input);
            
            Console.WriteLine($"\nEjecutando analisis de speedup con {cantidad:N0} ventas...");
            var speedup = new Speedup();
            var resultados = await speedup.Iniciar(cantidad);
            
            MostrarResultadosSpeedup(resultados);
        }
        
        private async Task ProbarTodos()
        {
            Console.Write($"\nIngrese cantidad de ventas (default {CANTIDAD_VENTAS_DEFAULT:N0}): ");
            var input = Console.ReadLine();
            var cantidad = string.IsNullOrWhiteSpace(input) ? CANTIDAD_VENTAS_DEFAULT : int.Parse(input);
            
            var ventas = GeneradorDeVentas.GenerarVentas(cantidad);
            
            // Procesador Secuencial
            Console.WriteLine($"\nProcesando {cantidad:N0} ventas de forma secuencial...");
            var secuencial = new ProcesadorSecuencial();
            var resultadoSecuencial = secuencial.Ejecutar(ventas);
            MostrarResultado(resultadoSecuencial);
            
            // Procesador Paralelo
            Console.WriteLine($"\nProcesando {cantidad:N0} ventas en paralelo...");
            var paralelo = new ProcesadorParalelo();
            var resultadoParalelo = await paralelo.EjecutarAsync(ventas);
            MostrarResultado(resultadoParalelo);
            
            // Procesador Paralelo con Lock
            Console.WriteLine($"\nProcesando {cantidad:N0} ventas en paralelo con Lock...");
            var paraleloConLock = new ProcesadorParaleloConLock();
            var resultadoParaleloConLock = await paraleloConLock.EjecutarAsync(ventas);
            MostrarResultado(resultadoParaleloConLock);
            
            // Speedup
            Console.WriteLine("\nEjecutando analisis de speedup...");
            var speedup = new Speedup();
            var resultadosSpeedup = await speedup.Iniciar(cantidad);
            MostrarResultadosSpeedup(resultadosSpeedup);
        }
        
        private void MostrarResultado(ResultadoProcesamiento resultado)
        {
            Console.WriteLine("\nRESULTADOS:");
            Console.WriteLine($"Tipo de Procesamiento: {resultado.TipoProcesamiento}");
            Console.WriteLine($"Total Ventas: {resultado.TotalVentas:C2}");
            Console.WriteLine($"Total ITBIS: {resultado.TotalITBIS:C2}");
            Console.WriteLine($"Tiempo Ejecucipn: {resultado.TiempoEjecucionMs} ms");
        }
        
        private void MostrarResultadosSpeedup(List<ResultadoSpeedup> resultados)
        {
            Console.WriteLine("\nRESULTADOS DE ANALISIS DE SPEEDUP:");
            Console.WriteLine("=".PadRight(80, '='));
            Console.WriteLine($"{"Procesadores",-12} {"Tiempo Seq (ms)",-15} {"Tiempo Par (ms)",-15} {"Speedup",-10} {"Eficiencia",-12}");
            Console.WriteLine("-".PadRight(80, '-'));
            
            foreach (var resultado in resultados)
            {
                Console.WriteLine($"{resultado.Procesadores,-12} " +
                                $"{resultado.TiempoSecuencialMs,-15} " +
                                $"{resultado.TiempoParaleloMs,-15} " +
                                $"{resultado.Speedup,-10:F2} " +
                                $"{resultado.Eficiencia,-12:F2}");
            }
            
            Console.WriteLine("=".PadRight(80, '='));
            Console.WriteLine("\nNota:");
            Console.WriteLine("- Speedup = Tiempo Secuencial / Tiempo Paralelo");
            Console.WriteLine("- Eficiencia = Speedup / Numero de Procesadores");
            Console.WriteLine("- Eficiencia ideal = 1.0 (100%)");
        }
    }
}