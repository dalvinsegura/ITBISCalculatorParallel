using ITBISCalculatorParallel.Models;
using ITBISCalculatorParallel.Processing;
using ITBISCalculatorParallel.Services;

Console.WriteLine("=== CALCULADORA DE ITBIS CON RECURSIVIDAD PARALELA ===\n");

// Generar ventas de prueba
{
    var ventas = GeneradorDeVentas.GenerarVentas(10_000_000);
    Console.WriteLine(ventas);
    var procesadorParalelo = new ProcesadorParalelo();
    await procesadorParalelo.EjecutarAsync(ventas);

}