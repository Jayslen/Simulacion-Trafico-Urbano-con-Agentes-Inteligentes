using System;
using System.Text.Json.Serialization;

namespace TrafficSimParallel.Worlds
{
    public class MapGrid
    {
        public int Width { get; init; }   
        public int Height { get; init; }

        [JsonIgnore]
        public Node[,] Grid { get; private set; }

        public MapType[,] CellsLinear { get; set; }

        public MapGrid() {}

        public MapGrid(int width, int height)
        {
            Width = width;
            Height = height;

            Grid = new Node[width, height];
            CellsLinear = new MapType[width, height];

            // Inicializar nodos y tipos
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Grid[x, y] = new Node(x, y, MapType.Empty);
                    CellsLinear[x, y] = MapType.Empty;
                }
            }
        }

        public MapType GetCell(int x, int y) => CellsLinear[x, y];

        public void SetCell(int x, int y, MapType type)
        {
            CellsLinear[x, y] = type;
            Grid[x, y].Type = type;
        }

        // Rebuild del Grid para pasar el set sin que tenga que ser expuesto 
        public void RebuildGrid()
        {
            Grid = new Node[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Grid[x, y] = new Node(x, y, CellsLinear[x, y]);
                }
            }
        }
    }
}