namespace TrafficSimParallel.config
{
    public class SimulationConfig
    {
        public int MapWidth { get; }
        public int MapHeight { get; }
        public int AgentCount { get; }
        public int SimulationSteps { get; }
        public int Threads { get; }
        public bool ShowMap { get; }

        public SimulationConfig(int mapWidth, int mapHeight, int agentCount, int simulationSteps, int threads, bool showMap)
        {
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            AgentCount = agentCount;
            SimulationSteps = simulationSteps;
            Threads = threads;
            ShowMap = showMap;
        }
    }

}
