using TrafficSimParallel;
namespace Tests
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new SimulationConfig(
                mapWidth: 200,
                mapHeight: 20,
                agentCount: 2000,
                simulationSteps: 10,
                threads: 8
            );

            await TrafficSimParallel.Program.Run(config);
        }
    }
}
