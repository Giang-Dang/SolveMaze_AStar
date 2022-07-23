using System.Drawing;
using System.Collections.Generic;

namespace AStar_Maze
{
    public class ImageProcessing
    {
        public static Vertex[,] ConvertImageToMatrix(Bitmap image, ref Vertex start, ref Vertex end)
        {
            Vertex[,] res = new Vertex[image.Width, image.Height];
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    bool isBlue = color.R < 60 && color.G < 60 && color.B > 150;
                    bool isRed = color.R > 150 && color.G < 60 && color.B < 60;
                    bool isBlack = color.R < 60 && color.B < 60 && color.G < 60;
                    bool isAllowed = !isBlack;

                    res[x,y] = new Vertex(x, y, isAllowed);
                    if(isRed)
                    {
                        end = res[x,y];
                    }
                    if(isBlue)
                    {
                        start = res[x,y];
                    }
                }
            }
            return res;
        }
        public static Bitmap EditImage(Bitmap image, List<Position> points, Color color) {
            foreach (var point in points) {
                image.SetPixel(point.X, point.Y, color);
            }
            return image;
        }
    }
}