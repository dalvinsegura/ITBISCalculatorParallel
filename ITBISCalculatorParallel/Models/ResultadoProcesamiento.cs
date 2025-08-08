namespace ITBISCalculatorParallel.Models;

public record ResultadoProcesamiento(
    decimal TotalVentas,
    decimal TotalITBIS,
    long TiempoEjecucionMs,
    string TipoProcesamiento
);

public record ResultadoSpeedup(
    long TiempoSecuencialMs,
    long TiempoParaleloMs,
    double Speedup,
    double Eficiencia,
    int Procesadores
);
