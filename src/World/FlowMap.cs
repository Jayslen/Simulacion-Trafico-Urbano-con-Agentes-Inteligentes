namespace TrafficSimParallel.Worlds
{
    public class FlowMap
    {
        public int[,] Density { get; set; }

        public FlowMap(int width, int height)
        {
            Density = new int[width, height];
        }

        public void Increase(int x, int y)
        {
            Density[x, y]++;
        }
    }
}