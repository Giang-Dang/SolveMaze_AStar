using System.IO;
namespace AStar_Maze
{
    public class OutputMatrix
    {
        public static void OutputMatrixToTxt(string filename, Vertex[,] matrix, Vertex start, Vertex end)
        {
            string path = $"{System.IO.Directory.GetCurrentDirectory()}\\{filename}";
            using(StreamWriter sw = File.CreateText(path))
            {
                for (int x = 0; x < matrix.GetLength(0); x++)
                {
                    for (int y = 0; y < matrix.GetLength(1); y++)
                    {
                        if (x == start.myPos.X && y == start.myPos.Y)
                        {
                            sw.Write("S  ");
                            continue;
                        }
                        if (x == end.myPos.X && y == end.myPos.Y)
                        {
                            sw.Write("E  ");
                            continue;
                        }
                        sw.Write($"{Convert.ToByte(matrix[x, y].isAllowed)}  ");

                    }
                    sw.WriteLine();
                }
            }

        }
    }
}