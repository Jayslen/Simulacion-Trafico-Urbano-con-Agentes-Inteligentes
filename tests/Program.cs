using TrafficSimParallel;

namespace Tests
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await SequentialSimulation();
        }

        public static async Task SequentialSimulation()
        {
            var config = new SimulationConfig(
                mapWidth: 50,
                mapHeight: 20,
                agentCount: 1000,
                simulationSteps: 10,
                sequential: true,
                threads: 1,
                showMap: false
            );

            await TrafficSimParallel.Program.Run(config);
        }
    }
}
