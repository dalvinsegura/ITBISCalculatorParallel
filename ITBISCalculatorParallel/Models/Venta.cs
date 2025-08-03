namespace ITBISCalculatorParallel.Models;

public class Venta
{

    public string Colmado { get; set; }           // Nombre o codigo del colmado
    public string Categoria { get; set; }         // Bebidas, Basicos, Cigarros, etc.
    public decimal Monto { get; set; }            // Monto total de la venta
    public DateTime Fecha { get; set; }           // Fecha de la venta

    public decimal ITBIS => Monto * 0.18m;        // Cálculo automático del ITBIS
    
}