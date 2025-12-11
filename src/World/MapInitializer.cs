using System.IO;
using System.Text.Json;

namespace TrafficSimParallel.Worlds
{
    public static class MapInitializer
    {
       public static MapGrid LoadFromJson(string path)
        {
            string json = File.ReadAllText(path);
            var map = JsonSerializer.Deserialize<MapGrid>(json);

            // Reconstruccion de nodos 

            map.RebuildGrid();

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    map.Grid[x, y] = new Node(x, y, map.CellsLinear[x, y]);
                }
            }

            return map;
        }

        public static void SaveToJson(MapGrid map, string path)
        {
            string json = JsonSerializer.Serialize(map, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(path, json);
        } 
    }
}