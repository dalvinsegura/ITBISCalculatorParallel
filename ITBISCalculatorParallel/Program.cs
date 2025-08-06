using ITBISCalculatorParallel.Models;
using ITBISCalculatorParallel.Processing;
using ITBISCalculatorParallel.Services;

Console.WriteLine("=== CALCULADORA DE ITBIS CON RECURSIVIDAD PARALELA ===\n");

// Generar ventas de prueba
{
   var ventas = GeneradorDeVentas.GenerarVentas(10_000_000);

    var procesadorParalelo = new ProcesadorParalelo();
    await procesadorParalelo.EjecutarAsync(ventas);

    Console.WriteLine();

    var procesadorParaleloconLock = new ProcesadorParaleloConLock();
    await procesadorParaleloconLock.EjecutarAsync(ventas);

    Console.WriteLine();

    // prueba secuencial weeee
    var procesadorSecuencial = new ProcesadorSecuencial();
    procesadorSecuencial.Ejecutar(ventas);

    Console.WriteLine();

    var speedup = new Speedup();
    await speedup.Iniciar(10_000_000);

}