# TrafficSimParallel — Simulación de Tráfico con Paralelismo en C#
Nuestro proyecto, Simulación Paralela de Tráfico Urbano con Agentes Inteligentes, consiste en el desarrollo de una simulación de tráfico urbano utilizando programación paralela en C#. El sistema modela el comportamiento de múltiples agentes (vehículos) desplazándose en un entorno bidimensional, gestionando su interacción con semáforos, carreteras, intersecciones y otros elementos del mapa.

La simulación emplea estructuras de datos thread-safe, paralelismo con Parallel.ForEach, control concurrente de semáforos y un mapa generado dinámicamente. El objetivo principal es analizar el rendimiento, la sincronización y la escalabilidad de un sistema de tráfico urbano cuando se ejecuta en múltiples núcleos de CPU.

El proyecto permite evaluar:

- El movimiento simultáneo de miles de vehículos.
- El impacto de los semáforos y las intersecciones.
- La eficiencia de estructuras concurrentes.
- El rendimiento comparado entre ejecución secuencial y paralela.


## Características Principales
### Simulación configurable

El usuario puede ajustar:

- Ancho y alto del mapa
- Cantidad de agentes
- Cantidad de pasos o iteraciones de la simulación

Todo esto mediante la clase SimulationConfig.

### Paralelismo real con Parallel.ForEach

Cada agente se mueve simultáneamente gracias al uso del Task Parallel Library (TPL), mejorando la performance en comparación con ejecuciones secuenciales.

#### Semáforos dinámicos

- Los semáforos cambian de estado automáticamente cada cierto número de steps.
- Su lógica se maneja con estructuras thread-safe (ConcurrentDictionary).

#### Mapa representado en consola

- Representación visual del mapa y los agentes en cada paso.
- Colores para distinguir tipos de celdas, semáforos y tráfico acumulado

#### Estructuras thread-safe
El proyecto utiliza:

- ConcurrentDictionary
- Manejo seguro de posiciones y actualizaciones globales
- Sincronización mínima para maximizar rendimiento

#### Arquitectura modular
El proyecto está dividido en:

- SimulationInitializer: prepara mapa, flujo, semáforos y agentes
- AgentHandler: lógica de movimiento por agente
- TrafficLightManager: control de semáforos
- MapRender: dibuja el mapa en consola
- Carpetas para Models, Types, Methods y Simulation
