using ITBISCalculatorParallel.Services;

// Configuracion inicial de la consola
Console.OutputEncoding = System.Text.Encoding.UTF8; // Para soportar simbolos de moneda
Console.Title = "Calculadora de ITBIS con Paralelismo";

try
{
    var app = new ConsolaApp();
    await app.Iniciar();
}
catch (Exception ex)
{
    Console.WriteLine($"\nERROR: {ex.Message}");
    Console.WriteLine("Presione cualquier tecla para salir...");
    Console.ReadKey();
}