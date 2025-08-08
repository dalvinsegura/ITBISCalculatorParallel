using ITBISCalculatorParallel.Models;
using ITBISCalculatorParallel.Processing;
using ITBISCalculatorParallel.Services;
using Xunit;

namespace ITBISCalculatorParallel.Tests
{
    public class ComparacionProcesadoresTests
    {
        [Fact]
        public async Task Procesadores_DeberianDarMismoResultado()
        {
            // Arrange
            var ventas = GeneradorDeVentas.GenerarVentas(10_000);
            var secuencial = new ProcesadorSecuencial();
            var paralelo = new ProcesadorParalelo();
            var paraleloConLock = new ProcesadorParaleloConLock();
            
            // Act
            var resultadoSecuencial = secuencial.Ejecutar(ventas);
            var resultadoParalelo = await paralelo.EjecutarAsync(ventas);
            var resultadoParaleloConLock = await paraleloConLock.EjecutarAsync(ventas);
            
            // Assert
            Assert.Equal(resultadoSecuencial.TotalVentas, resultadoParalelo.TotalVentas);
            Assert.Equal(resultadoSecuencial.TotalITBIS, resultadoParalelo.TotalITBIS);
            
            Assert.Equal(resultadoSecuencial.TotalVentas, resultadoParaleloConLock.TotalVentas);
            Assert.Equal(resultadoSecuencial.TotalITBIS, resultadoParaleloConLock.TotalITBIS);
        }
    }
}