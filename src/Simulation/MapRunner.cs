using System.Collections.Concurrent;
using TrafficSimParallel.Types;
using TrafficSimParallel.Models;

namespace TrafficSimParallel.Simulation
{
    public class MapRender
    {
        public static void PrintMap(string title, int frameNumber, bool isFinalFrame, SimulationConfig _config, ConcurrentDictionary<Position, int> _globalGrid, ConcurrentDictionary<Position, TrafficLight> _trafficLights)
        {
            Console.WriteLine($"[{title}]");
            char[,] buffer = new char[_config.MapHeight, _config.MapWidth];

            for (int y = 0; y < _config.MapHeight; y++)
                for (int x = 0; x < _config.MapWidth; x++)
                    buffer[y, x] = '.';

            foreach (var kvp in _globalGrid)
                buffer[kvp.Key.Y, kvp.Key.X] = (char)('0' + (kvp.Value % 10));

            foreach (var light in _trafficLights)
                buffer[light.Key.Y, light.Key.X] = 'T';

            for (int y = 0; y < _config.MapHeight; y++)
            {
                for (int x = 0; x < _config.MapWidth; x++)
                {
                    if (buffer[y, x] == 'T')
                    {
                        var light = _trafficLights[new Position(x, y)];
                        Console.ForegroundColor = light.GetState() == LightState.Green ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write("T ");
                    }
                    else if (buffer[y, x] != '.')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(buffer[y, x] + " ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }

                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }

}