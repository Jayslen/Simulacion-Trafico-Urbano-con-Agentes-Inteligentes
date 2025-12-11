using System.Collections.Concurrent;
using TrafficSimParallel.Models;
using TrafficSimParallel.Types;
using TrafficSimParallel.Worlds;

namespace TrafficSimParallel.Methods
{
    public static class SimulationInitializer
    {

        public static void InitializeSimulation(
            int mapWidth,
            int mapHeight,
            int agentCount,
            MapType[,] mapTypes,
            Dictionary<Position, Position> flowMap,
            ConcurrentDictionary<Position, TrafficLight> trafficLights,
            List<Position> intersections,
            ConcurrentDictionary<Position, int> globalGrid,
            List<Agent> agents
        )
        {
            // =============================
            // 1. Llenar mapa de paredes
            // =============================
            for (int y = 0; y < mapHeight; y++)
                for (int x = 0; x < mapWidth; x++)
                    mapTypes[y, x] = MapType.Wall;

            // Carretera horizontal Y=10
            if (mapHeight > 10)
                for (int x = 0; x < mapWidth; x++)
                    mapTypes[10, x] = MapType.Road;

            // Carretera vertical X=10 y X=20
            for (int y = 0; y < mapHeight; y++)
            {
                if (mapWidth > 10)
                    mapTypes[y, 10] = MapType.Road;

                if (mapWidth > 20)
                    mapTypes[y, 20] = MapType.Road;
            }

            // =============================
            // 2. Flujo de movimiento
            // =============================

            // Horizontal
            if (mapHeight > 10)
            {
                for (int x = 0; x < mapWidth - 1; x++)
                {
                    if (mapTypes[10, x] == MapType.Road)
                        flowMap.TryAdd(new Position(x, 10), new Position(x + 1, 10));
                }
            }

            // Vertical
            if (mapWidth > 10)
            {
                for (int y = 0; y < mapHeight - 1; y++)
                {
                    if (mapTypes[y, 10] == MapType.Road)
                        flowMap.TryAdd(new Position(10, y), new Position(10, y + 1));
                }
            }

            // =============================
            // 3. Semáforos
            // =============================

            // Cruce 1
            if (mapWidth > 21 && mapHeight > 11)
            {
                trafficLights.TryAdd(new Position(19, 10), new TrafficLight(new Position(19, 10), LightState.Green));
                trafficLights.TryAdd(new Position(21, 10), new TrafficLight(new Position(21, 10), LightState.Green));
                trafficLights.TryAdd(new Position(20, 9), new TrafficLight(new Position(20, 9), LightState.Red));
                trafficLights.TryAdd(new Position(20, 11), new TrafficLight(new Position(20, 11), LightState.Red));
                trafficLights.TryAdd(new Position(19, 9), new TrafficLight(new Position(19, 9), LightState.Red));

                intersections.Add(new Position(20, 10));
            }

            // Cruce 2
            if (mapWidth > 11 && mapHeight > 11)
            {
                trafficLights.TryAdd(new Position(9, 10), new TrafficLight(new Position(9, 10), LightState.Red));
                trafficLights.TryAdd(new Position(11, 10), new TrafficLight(new Position(11, 10), LightState.Red));
                trafficLights.TryAdd(new Position(10, 9), new TrafficLight(new Position(10, 9), LightState.Green));
                trafficLights.TryAdd(new Position(10, 11), new TrafficLight(new Position(10, 11), LightState.Green));
                trafficLights.TryAdd(new Position(9, 9), new TrafficLight(new Position(9, 9), LightState.Red));

                intersections.Add(new Position(10, 10));
            }

            // =============================
            // 4. Crear agentes
            // =============================
            var rnd = new Random();

            for (int i = 0; i < agentCount; i++)
            {
                Position startPos;
                do
                {
                    startPos = new Position(rnd.Next(0, mapWidth), rnd.Next(0, mapHeight));
                } while (globalGrid.ContainsKey(startPos)); // Evitar duplicados

                var agent = new Agent
                {
                    Id = i,
                    CurrentPos = startPos,
                    Speed = rnd.Next(10, 50)
                };

                agents.Add(agent);
                globalGrid.TryAdd(startPos, agent.Id);
            }
        }
    }
}