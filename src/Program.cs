using System.Collections.Concurrent;
using System.Diagnostics;
using TrafficSimParallel.Models;
using TrafficSimParallel.Methods;
using TrafficSimParallel.Types;
using TrafficSimParallel.Simulation;
using TrafficSimParallel.Worlds;

namespace TrafficSimParallel
{

    public record struct Position(int X, int Y);


    public class SimulationConfig
    {
        public int MapWidth { get; }
        public int MapHeight { get; }
        public int AgentCount { get; }
        public int SimulationSteps { get; }
        public int Threads { get; }
        public bool ShowMap { get; }
        public bool Sequential { get; }

        public SimulationConfig(int mapWidth, int mapHeight, int agentCount, int simulationSteps, int threads, bool showMap, bool sequential)
        {
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            AgentCount = agentCount;
            SimulationSteps = simulationSteps;
            Threads = threads;
            ShowMap = showMap;
            Sequential = sequential;
        }
    }

    public class Program
    {
        static SimulationConfig _config;
        static Dictionary<Position, Position> _flowMap = new();
        static MapType[,] _mapTypes;
        static ConcurrentDictionary<Position, int> _globalGrid = new();
        static ConcurrentDictionary<Position, TrafficLight> _trafficLights = new();
        static List<Position> _intersectionPoints = new();
        static List<Agent> _agents = new();


        public static async Task Run(SimulationConfig config)
        {
            _config = config;
            _mapTypes = new MapType[_config.MapHeight, _config.MapWidth];


            Stopwatch time = Stopwatch.StartNew();
            Console.WriteLine($"=== Simulación {(_config.Sequential ? "Secuencial" : "Paralela")} con Parámetros ===");
            Console.WriteLine($"Mapa: {_config.MapWidth}x{_config.MapHeight}, Agentes: {_config.AgentCount}, Steps: {_config.SimulationSteps} {(!_config.Sequential ? $"Threads: {_config.Threads}" : "")}");
            Console.WriteLine("===========================================");

            SimulationInitializer.InitializeSimulation(
                    _config.MapWidth,
                    _config.MapHeight,
                    _config.AgentCount,
                    _mapTypes,
                    _flowMap,
                    _trafficLights,
                    _intersectionPoints,
                    _globalGrid,
                    _agents
                );

                if(_config.ShowMap)
                {
                    MapRender.PrintMap(
                        "Estado Inicial",
                        0,
                        false,
                        _config,
                        _globalGrid,
                        _trafficLights
                    );
                }


            for (int step = 1; step <= _config.SimulationSteps; step++)
            {
                Console.WriteLine($"\n--- Paso {step} ---");

                Stopwatch sw = Stopwatch.StartNew();


                if (step % 5 == 0)
                {
                    TrafficLightManager.ChangeTrafficLights(_trafficLights);
                }

                if(_config.Sequential) {
                    foreach(var agent in _agents) {
                        AgentHandler.MoveAgentLogic(agent, _config.MapWidth, _trafficLights, _globalGrid);
                    }
                }
                else {
                Parallel.ForEach(
                    _agents,
                    new ParallelOptions
                    {
                        MaxDegreeOfParallelism = _config.Threads
                    },
                    agent =>
                    {
                        AgentHandler.MoveAgentLogic(agent, _config.MapWidth, _trafficLights, _globalGrid);
                    }
                );
                }


                sw.Stop();

                var timeElapsed = sw.Elapsed.TotalMilliseconds;
                if(_config.ShowMap){

                MapRender.PrintMap(
                   $"Renderizado (Tiempo de cálculo: {timeElapsed:F4} ms",
                    step,
                    step == _config.SimulationSteps,
                    _config,
                    _globalGrid,
                    _trafficLights
                );
                } else {
                    Console.WriteLine($"Renderizado (Tiempo de cálculo: {timeElapsed:F4} ms");
                }

                await Task.Delay(500);
            }

            time.Stop();

            var executionTime = time.Elapsed.TotalMilliseconds;


            Console.WriteLine($"Tiempo total de simulación: {executionTime:F4} ms");

            Console.WriteLine("\nSimulación finalizada.");
        }


        static async Task Main(string[] args)
        {
            var config = new SimulationConfig(
                mapWidth: 100,
                mapHeight: 50,
                agentCount: 5000,
                simulationSteps: 10,
                threads: 4,
                showMap: false,
                sequential: false
            );

            await Run(config);
        }
    }
}
