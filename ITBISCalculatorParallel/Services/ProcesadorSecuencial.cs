using ITBISCalculatorParallel.Models;
using System.Diagnostics;

namespace ITBISCalculatorParallel.Services;

public class ProcesadorSecuencial
{
    private const decimal TASA_ITBIS = 0.18m;

    public ResultadoProcesamiento Ejecutar(List<Venta> ventas)
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

        return new ResultadoProcesamiento(
            totalVentas,
            totalITBIS,
            sw.ElapsedMilliseconds,
            "Secuencial"
        );
    }

    public long EjecutarSpeedup(List<Venta> ventas)
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

        return sw.ElapsedMilliseconds;
    }
}