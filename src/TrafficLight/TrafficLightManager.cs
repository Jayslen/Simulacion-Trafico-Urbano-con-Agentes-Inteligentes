using System.Collections.Concurrent;
using TrafficSimParallel.Models;
using TrafficSimParallel.Types;

namespace TrafficSimParallel.Methods
{
    public class TrafficLightManager
    {
        public static void ChangeTrafficLights(ConcurrentDictionary<Position, TrafficLight> _trafficLights)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--- CAMBIO DE SEMÁFOROS (10 Luces, 2 Cruces) ---");
            Console.ResetColor();

            var cruce1Pos = new List<int> { 19, 20, 21 }; // X-coords para Cruce 1
            var cruce2Pos = new List<int> { 9, 10, 11 };  // X-coords para Cruce 2

            TrafficLightManager.ToggleCruce(cruce1Pos, "Central", _trafficLights);

            TrafficLightManager.ToggleCruce(cruce2Pos, "Oeste", _trafficLights);

            foreach (var light in _trafficLights.Values)
            {
                Console.WriteLine($"Luz en {light.Position} ahora es {light.GetState()}");
            }
        }

        static void ToggleCruce(List<int> xCoords, string name, ConcurrentDictionary<Position, TrafficLight> _trafficLights)
        {
            var cruceLights = _trafficLights.Where(kvp => xCoords.Contains(kvp.Key.X)).Select(kvp => kvp.Value).ToList();

            var horizontalLights = cruceLights.Where(l => l.Position.Y == 10).ToList();
            var verticalAndAuxLights = cruceLights.Where(l => l.Position.Y != 10).ToList();

            if (!horizontalLights.Any()) return; // Evitar error si el cruce está mal definido

            bool isCurrentlyHorizontalGreen = horizontalLights.First().GetState() == LightState.Green;

            Console.WriteLine($"\n> Sincronizando Cruce {name}: H-Green={isCurrentlyHorizontalGreen}");

            if (isCurrentlyHorizontalGreen)
            {
                foreach (var light in horizontalLights.Where(l => l.GetState() == LightState.Green)) light.ToggleState();
                foreach (var light in verticalAndAuxLights.Where(l => l.GetState() == LightState.Red)) light.ToggleState();
            }
            else
            {
                foreach (var light in horizontalLights.Where(l => l.GetState() == LightState.Red)) light.ToggleState();
                foreach (var light in verticalAndAuxLights.Where(l => l.GetState() == LightState.Green)) light.ToggleState();
            }
        }
    }
}
