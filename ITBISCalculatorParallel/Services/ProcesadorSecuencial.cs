using ITBISCalculatorParallel.Models;
using System.Diagnostics;

namespace ITBISCalculatorParallel.Services;

public class ProcesadorSecuencial
{
    private const decimal TASA_ITBIS = 0.18m;

    public void Ejecutar(List<Venta> ventas)
    {
        decimal totalVentas = 0;
        decimal totalITBIS = 0;

        var sw = Stopwatch.StartNew();

        foreach (var venta in ventas)
        {
            totalVentas += venta.Monto;
            totalITBIS += venta.Monto * TASA_ITBIS;
        }

        sw.Stop();

        Console.WriteLine($"[Secuencial] Total Ventas: {totalVentas:C}");
        Console.WriteLine($"[Secuencial] Total ITBIS: {totalITBIS:C}");
        Console.WriteLine($"[Secuencial] Tiempo: {sw.ElapsedMilliseconds} ms");
    }

}