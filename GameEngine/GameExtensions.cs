using System;

namespace GameEngine
{
    public static class GameExtensions
    {
        public static Tuple<int, int> CoordinatesOf(this GameCell[,] matrix, CellType type)
        {
            int w = matrix.GetLength(0); // width
            int h = matrix.GetLength(1); // height

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x, y].Value.Equals(type))
                        return Tuple.Create(x, y);
                }
            }

            return null;
        }
        public static GameCell[,] ToGameCells(this CellType[,] enums2d)
        {
            var grid = new GameCell[enums2d.GetLength(0), enums2d.GetLength(1)];
            for (int x = 0; x < enums2d.GetLength(0); x += 1)
            {
                for (int y = 0; y < enums2d.GetLength(1); y += 1)
                {
                    grid[x, y] = new GameCell(enums2d[x, y]);
                }
            }

            return grid;
        }

        public static char[,] To2dChars(this GameCell[,] cells)
        {
            var grid = new char[cells.GetLength(0), cells.GetLength(1)];
            for (int x = 0; x < cells.GetLength(0); x += 1)
            {
                for (int y = 0; y < cells.GetLength(1); y += 1)
                {
                    grid[x, y] = (char)(cells[x, y].Value);
                }
            }

            return grid;
        }
        public static string ToText(this char[,] cells)
        {
            string text = string.Empty;

            for (int x = 0; x < cells.GetLength(0); x += 1)
            {
                for (int y = 0; y < cells.GetLength(1); y += 1)
                {
                    text += cells[x, y];
                }

                text += "\n";
            }

            return text;
        }
    }
}