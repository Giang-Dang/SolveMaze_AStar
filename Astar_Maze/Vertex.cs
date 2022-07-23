namespace AStar_Maze
{
    public class Vertex
    {
        public Position myPos, basePos;
        public bool isAllowed;
        public bool isChecked;
        public double G, H, F;
        public Vertex(int _X, int _Y, bool _isAllowed)
        {
            myPos = new Position(_X, _Y);
            isAllowed = _isAllowed;
            isChecked = false;
            basePos = new Position(-1, -1);
            G = 0;
            H = 0;
        }

        public void UpdateG(double newG)
        {
            G = newG;
        }
        public void CalculateAndUpdateH(Vertex end)
        {
            H = Math.Sqrt(Math.Pow(end.myPos.X - myPos.X, 2) + Math.Pow(end.myPos.Y - myPos.Y, 2));
        }
        public void CalculateAndUpdateF()
        {
            F = G + H;
        }
        public double CalculateF(double newG)
        {
            return newG + H;
        }
    }
}