# Manual de Ejecución - Calculadora de ITBIS con Paralelismo

## Descripción del Sistema
La Calculadora de ITBIS con Paralelismo es una aplicación de consola desarrollada en .NET 9.0 que permite calcular el Impuesto de Transferencia de Bienes Industrializados y Servicios (ITBIS) utilizando diferentes métodos de procesamiento para comparar el rendimiento entre procesamiento secuencial y paralelo.

## Requisitos del Sistema
- **.NET 9.0 Runtime** o superior
- **Sistema Operativo**: Windows, macOS, o Linux
- **Memoria RAM**: Mínimo 4GB (recomendado 8GB para procesar grandes volúmenes)
- **Procesador**: Multinúcleo recomendado para aprovechar el paralelismo

## Instalación

### Desde el código fuente:
1. Clonar o descargar el proyecto
2. Abrir terminal en la carpeta `ITBISCalculatorParallel`
3. Ejecutar: `dotnet restore`
4. Compilar: `dotnet build`

### Ejecutar directamente:
```bash
dotnet run
```

## Funcionalidades del Sistema

### Menú Principal
Al ejecutar la aplicación, se presenta un menú con 6 opciones:

```
MENU PRINCIPAL
1. Probar Procesador Secuencial
2. Probar Procesador Paralelo
3. Probar Procesador Paralelo con Lock
4. Probar Comparación de Speedup
5. Probar Todos los Procesadores
6. Salir
```

## Guía de Uso

### 1. Procesador Secuencial
- **Función**: Procesa las ventas una por una de forma tradicional
- **Uso**: Seleccionar opción `1`
- **Input**: Cantidad de ventas a procesar (por defecto: 1,000,000)
- **Output**: Total de ventas, total ITBIS calculado y tiempo de ejecución

### 2. Procesador Paralelo
- **Función**: Procesa las ventas utilizando múltiples hilos simultáneamente
- **Uso**: Seleccionar opción `2`
- **Input**: Cantidad de ventas a procesar
- **Output**: Mismos datos que el secuencial pero con tiempo optimizado

### 3. Procesador Paralelo con Lock
- **Función**: Procesamiento paralelo con sincronización de hilos
- **Uso**: Seleccionar opción `3`
- **Input**: Cantidad de ventas a procesar
- **Output**: Resultados con control de concurrencia

### 4. Análisis de Speedup
- **Función**: Compara el rendimiento entre procesamiento secuencial y paralelo
- **Uso**: Seleccionar opción `4`
- **Input**: Cantidad de ventas a procesar
- **Output**: Tabla comparativa con métricas de rendimiento:
  - Número de procesadores utilizados
  - Tiempo secuencial vs paralelo
  - Factor de speedup
  - Eficiencia del paralelismo

### 5. Probar Todos
- **Función**: Ejecuta todos los procesadores secuencialmente para comparación completa
- **Uso**: Seleccionar opción `5`
- **Input**: Cantidad de ventas a procesar
- **Output**: Resultados de todos los métodos + análisis de speedup

## Interpretación de Resultados

### Métricas Mostradas:
- **Total Ventas**: Suma monetaria de todas las ventas procesadas
- **Total ITBIS**: Impuesto calculado sobre las ventas
- **Tiempo Ejecución**: Tiempo en milisegundos que tomó el procesamiento
- **Speedup**: Factor de mejora del procesamiento paralelo vs secuencial
- **Eficiencia**: Qué tan bien se aprovechan los recursos del procesador

### Valores Ideales:
- **Speedup**: Cercano al número de núcleos del procesador
- **Eficiencia**: Cercano a 1.0 (100%)

## Ejemplos de Ejecución

### Ejemplo 1: Prueba rápida
```
Cantidad de ventas: 100000
Resultado esperado: Procesamiento en ~100-500ms
```

### Ejemplo 2: Prueba de rendimiento
```
Cantidad de ventas: 1000000 (por defecto)
Resultado esperado: Diferencia notable entre secuencial y paralelo
```

### Ejemplo 3: Prueba intensiva
```
Cantidad de ventas: 10000000
Resultado esperado: Mayor diferencia de rendimiento, mejor análisis de speedup
```

## Solución de Problemas

### Error: "Memoria insuficiente"
- **Causa**: Cantidad de ventas muy alta para la RAM disponible
- **Solución**: Reducir la cantidad de ventas a procesar

### Error: "Aplicación no responde"
- **Causa**: Procesamiento de gran volumen de datos
- **Solución**: Esperar a que termine o reiniciar con menos datos

### Rendimiento bajo en paralelo
- **Causa**: Procesador con pocos núcleos o datos insuficientes
- **Solución**: Usar más datos o verificar especificaciones del hardware

## Comandos Útiles

### Ejecutar con cantidad específica:
No hay parámetros de línea de comandos, pero se puede ingresar la cantidad cuando la aplicación lo solicite.

### Ejecutar tests:
```bash
cd TestProject
dotnet test
```

### Limpiar y reconstruir:
```bash
dotnet clean
dotnet build
```

## Equipo
- ### Edgard J. Mendez | Encargado de los datos
- ### Estarling German | Encargado de la prog. secuencial
- ### Dalvin Segura | Encargado de la prog. paralela y interactividad mediante consola
- ### Gregoris I. De La Cruz | Encargado de la documentación