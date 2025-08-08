using ITBISCalculatorParallel.Models;
using ITBISCalculatorParallel.Services;
using Xunit;

namespace ITBISCalculatorParallel.Tests
{
    public class ProcesadorSecuencialTests
    {
        [Fact]
        public void Ejecutar_DeberiaCalcularCorrectamente()
        {
            // Arrange
            var procesador = new ProcesadorSecuencial();
            var ventas = new List<Venta>
            {
                new Venta { Monto = 100 },
                new Venta { Monto = 200 }
            };
            
            // Act
            var resultado = procesador.Ejecutar(ventas);
            
            // Assert
            Assert.Equal(300, resultado.TotalVentas);
            Assert.Equal(300 * 0.18m, resultado.TotalITBIS);
            Assert.True(resultado.TiempoEjecucionMs >= 0);
        }

        [Fact]
        public void EjecutarSpeedup_DeberiaRetornarTiempo()
        {
            // Arrange
            var procesador = new ProcesadorSecuencial();
            var ventas = new List<Venta>
            {
                new Venta { Monto = 100 },
                new Venta { Monto = 200 }
            };
            
            // Act
            var tiempo = procesador.EjecutarSpeedup(ventas);
            
            // Assert
            Assert.True(tiempo >= 0);
        }
    }
}