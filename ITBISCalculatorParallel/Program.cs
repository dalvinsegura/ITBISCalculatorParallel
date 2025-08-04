

using ITBISCalculatorParallel.Models;
using ITBISCalculatorParallel.Services;

var clase = GeneradorDeVentas.GenerarVentas(10);


foreach (var venta in clase.GetRange(0, 3))
{
    Console.WriteLine(venta);
}