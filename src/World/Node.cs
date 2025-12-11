using System.Collections.Generic;

namespace TrafficSimParallel.Worlds
{
    public class Node
    {
       public int X { get; set; }
       public int Y { get; set; }
       public MapType Type { get; set; } = MapType.Empty;


       // Aristas salientes o conectadas

       public List<Edge> Edges { get; set; } = new List<Edge>();

       public Node() {}

         public Node(int x, int y, MapType type)
         {
              X = x; Y = y; Type = type;
         }

         public override string ToString() => $"Node({X}, {Y}, {Type})";
    }
}