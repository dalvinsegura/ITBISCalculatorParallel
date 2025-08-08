using ITBISCalculatorParallel.Models;
using ITBISCalculatorParallel.Processing;
using ITBISCalculatorParallel.Services;
using Xunit;

namespace ITBISCalculatorParallel.Tests
{
    public class ProcesadorParaleloConLockTests
    {
        [Fact]
        public async Task EjecutarAsync_DeberiaCalcularCorrectamente()
        {
            // Arrange
            var procesador = new ProcesadorParaleloConLock();
            var ventas = new List<Venta>
            {
                new Venta { Monto = 100 },
                new Venta { Monto = 200 }
            };
            
            // Act
            var resultado = await procesador.EjecutarAsync(ventas);
            
            // Assert
            Assert.Equal(300, resultado.TotalVentas);
            Assert.Equal(300 * 0.18m, resultado.TotalITBIS);
            Assert.True(resultado.TiempoEjecucionMs >= 0);
        }

        [Fact]
        public async Task EjecutarSpeedupAsync_DeberiaRetornarTiempo()
        {
            // Arrange
            var procesador = new ProcesadorParaleloConLock();
            var ventas = new List<Venta>
            {
                new Venta { Monto = 100 },
                new Venta { Monto = 200 }
            };
            
            // Act
            var tiempo = await procesador.EjecutarSpeedupAsync(ventas);
            
            // Assert
            Assert.True(tiempo >= 0);
        }

        [Fact]
        public async Task CalcularTotalesConLock_DeberiaProtegerRecursosCompartidos()
        {
            // Arrange
            var procesador = new ProcesadorParaleloConLock();
            var ventas = GeneradorDeVentas.GenerarVentas(10_000);
            
            // Act
            var resultado = await procesador.EjecutarAsync(ventas);
            var totalEsperado = ventas.Sum(v => v.Monto);
            var itbisEsperado = ventas.Sum(v => v.Monto * 0.18m);
            
            // Assert
            Assert.Equal(totalEsperado, resultado.TotalVentas);
            Assert.Equal(itbisEsperado, resultado.TotalITBIS);
        }
    }
}