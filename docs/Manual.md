# Manual de Usuario — Simulación de Tráfico Paralela en C#

Este manual describe el funcionamiento, configuración y ejecución del programa de simulación paralela de tráfico vehicular, desarrollado en C# utilizando Programación Paralela y estructuras concurrentes.

El objetivo de la herramienta es simular el movimiento de miles de agentes (vehículos) dentro de un entorno urbano que incluye semáforos, intersecciones y límites definidos.

## ¿Cómo ejecutar el programa?

### Clonar repositorio
```bash
git clone https://github.com/Jayslen/Simulacion-Trafico-Urbano-con-Agentes-Inteligentes
```

### Ejecutar programa
```bash
cd proyecto/src
dotnet run
```

### Ejecutar test
```bash
cd proyecto/test
# Modificar el config en Program.cs
dotnet run
```

## Requerimientos
- .NET 8.0 SDK o superior
- Librerias:
  - System.Collections.Concurrent
  - System.Threading
  - System.Threading.Tasks
  - System.Diagnostics

 ## Archivos del Proyecto
El proyecto está compuesto por los siguientes módulos:

```
/src
 ├── Program.cs
 ├── SimulationInitializer.cs
 ├── AgentHandler.cs
 ├── TrafficLightManager.cs
 ├── MapRender.cs
 ├── Models/
 │     ├── Agent.cs
 │     ├── TrafficLight.cs
 └── Types/
       ├── Position.cs
       ├── MapType.cs
       ├── LightState.cs
       ├── SimulationConfig.cs

```

## Configuración del Programa
El archivo SimulationConfig.cs contiene los parámetros modificables:
| Parámetro        | Descripción                   | Ejemplo      |
| ---------------- | ----------------------------- | ------------ |
| `agentCount`     | Cantidad de vehículos         | 3000         |
| `mapWidth`       | Ancho del mapa                | 30           |
| `mapHeight`      | Alto del mapa                 | 20           |
| `threads`        | Cantidad de hilos en paralelo | 8            |
| `showMap`        | Mostrar el mapa en consola    | true / false |
| `simulationSteps`| Cantidad de pasos a simular   | 100 |
