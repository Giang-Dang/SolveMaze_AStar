using System.Drawing;
using AStar_Maze;
using System.Diagnostics;


Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
string filename = string.Empty;
if (args[0] != null)
{
    filename = args[0];
}
// try
// {  
    string path = $"{System.IO.Directory.GetCurrentDirectory()}\\{filename}";
    Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
    Bitmap Image = new Bitmap(path, true);
    Vertex Start = new Vertex(0, 0, false);
    Vertex End = new Vertex(0, 0, false);
    Vertex [,] maze = ImageProcessing.ConvertImageToMatrix(Image, ref Start, ref End);
    OutputMatrix.OutputMatrixToTxt("matrix.txt",maze , Start, End);
    Console.WriteLine("Start : " + Start.myPos.X + " " + Start.myPos.Y);
    Console.WriteLine("End : " + End.myPos.X + " " + End.myPos.Y);
    Console.WriteLine("==============");
    Console.ReadKey();
    Console.WriteLine("Calculating...");
    List<Position> Result = new List<Position>();
    if (AStar.SolveMaze(ref maze, ref Start, ref End, ref Result))
    {
        Bitmap resImage =  ImageProcessing.EditImage(Image, Result, Color.Blue);
        stopwatch.Stop();
        resImage.Save($"{System.IO.Directory.GetCurrentDirectory()}\\{filename}_result_{stopwatch.ElapsedMilliseconds}.png");
    }
