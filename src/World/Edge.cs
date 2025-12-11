namespace TrafficSimParallel.Worlds
{
    // Edge inicial simple para grafo (de nodo A a nodo B)
    public class Edge
    {
        public Node From { get; set; }
        public Node To { get; set; }
        public double Cost { get; set; } = 1.0; // Costo por defecto
        public Edge () { }
        public Edge (Node from, Node to, double cost = 1.0)
        {
            From = from; To = to; Cost = cost;
        }

        public override string ToString() => $"Edge(From: {From}, To: {To}, Cost: {Cost})";
    }
}