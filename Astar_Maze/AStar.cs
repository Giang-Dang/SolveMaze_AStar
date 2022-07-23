namespace AStar_Maze
{
    public class AStar
    {
        public static bool SolveMaze(ref Vertex[,] Maze, ref Vertex Start, ref Vertex End, ref List<Position> Result)
        {
            List<Vertex> priorityQueue = new List<Vertex>();
            Vertex baseVertex;
            Start.UpdateG(0);
            Start.CalculateAndUpdateH(End);
            priorityQueue.Add(Start);
            Maze[Start.myPos.X, Start.myPos.Y].isChecked = true;

            //test
            // string path = $"{System.IO.Directory.GetCurrentDirectory()}\\Solving.txt";
            // Console.WriteLine($"Start ({Start.myPos.X},{Start.myPos.Y})");
            // Console.WriteLine($"End ({End.myPos.X},{End.myPos.Y})");

            bool isEndInQueue = false, isEndPoppedOut = false;

            while(priorityQueue.Any() && !isEndPoppedOut)
            {
                //test
                // Console.WriteLine($"Maze({priorityQueue[0].myPos.X},{priorityQueue[0].myPos.Y}) is popped out");

                //pop 1st element in queue -> currentVertex;
                baseVertex = priorityQueue.MinBy(v => v.F);
                int baseX = baseVertex.myPos.X;
                int baseY = baseVertex.myPos.Y;
                Maze[baseX,baseY].isAllowed = false;

                //test
                // Console.WriteLine($"Maze({baseVertex.myPos.X},{baseVertex.myPos.Y}) isChecked={baseVertex.isChecked} is popped out");

                priorityQueue.Remove(priorityQueue.MinBy(v => v.F));
                

                //Expand surrounding cells and check valid?, isAllowed?, isBaseVertex?
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int newX = baseX+i;
                        int newY = baseY+j;
                        bool isValid = newX >= 0 && newY >= 0 && newX < Maze.GetLength(0) && newY < Maze.GetLength(1);
                        bool isBaseVertex = newX == baseX && newY == baseY;
                        if (isValid && !isBaseVertex)
                        {
                            //if not allowed -> skip to next loop
                            if(!Maze[newX, newY].isAllowed)
                            {
                                // //test
                                // Console.WriteLine($"Maze({newX},{newY}).isAllowed={Maze[newX, newY].isAllowed}");

                                continue;
                            }

                            //skip if Vertex is checked and newG is not better than oldG;
                            if(Maze[newX, newY].isChecked)
                            {
                                if (baseVertex.G + 1 > Maze[newX, newY].G)
                                {
                                    // //test
                                    // Console.WriteLine($"Maze({newX},{newY}): newG={baseVertex.G + 1} > oldG={Maze[newX, newY].G}");

                                    continue;
                                }
                            }

                            // //test
                            // Console.WriteLine($"Maze({newX},{newY}).isAllowed={Maze[newX, newY].isAllowed}");
                            // Console.WriteLine($"Maze({newX},{newY}): newG={baseVertex.G + 1} || oldG={Maze[newX, newY].G}");

                            //if cell is not in queue -> update G & H, updateH because H is not calculated, update baseVertex -> Maze[newX, newY].basePos, then add to Queue
                            if(!IsContainsVertex(ref priorityQueue, Maze[newX, newY]))
                            {
                                // //test
                                // Console.WriteLine($"Maze({newX},{newY}) is not in Queue.");

                                Maze[newX, newY].UpdateG(baseVertex.G + 1);
                                Maze[newX, newY].CalculateAndUpdateH(End);
                                Maze[newX, newY].CalculateAndUpdateF();
                                Maze[newX, newY].basePos = baseVertex.myPos;
                                //Check if End is pushed into queue
                                if(End.myPos.Equals(Maze[newX, newY].myPos))
                                {
                                    isEndInQueue = true;
                                }
                                priorityQueue.Add(Maze[newX, newY]);
                                Maze[newX, newY].isChecked = true;

                                // //test
                                // Console.WriteLine($"G=({Maze[newX, newY].G}) H=({Maze[newX, newY].H}) F=({Maze[newX, newY].F}) basePos=({Maze[newX, newY].basePos.X},{Maze[newX, newY].basePos.Y}) - Maze({newX},{newY}) is added into Queue.");
                            }

                            //if cell already in queue and newG is better -> update newG to vertex in maze, baseVertex->basePos 
                            if(IsContainsVertex(ref priorityQueue, Maze[newX, newY]))
                            {
                                // //test
                                // Console.WriteLine($"Maze({newX},{newY}) is in Queue");
                                int indexOfCurrentVertex = IndexOfVertexPosition(ref priorityQueue, Maze[newX, newY]);

                                if( priorityQueue[indexOfCurrentVertex].F < Maze[newX, newY].CalculateF(baseVertex.G + 1))
                                {
                                    //test
                                    // Console.Write($"priorityQueue[indexOfCurrentVertex].F=({priorityQueue[indexOfCurrentVertex].F}) Maze[newX, newY].CalculateF(baseVertex.G)=({Maze[newX, newY].CalculateF(baseVertex.G)}). ");
                                    // Console.WriteLine($"Old basePos ({Maze[newX, newY].basePos.X},{Maze[newX, newY].basePos.Y})");

                                    Maze[newX, newY].G = baseVertex.G + 1;
                                    Maze[newX, newY].CalculateAndUpdateF();
                                    Maze[newX, newY].basePos = baseVertex.myPos;
                                    
                                    // //test
                                    // Console.WriteLine($"G=({Maze[newX, newY].G}) H=({Maze[newX, newY].H}) F=({Maze[newX, newY].F}) basePos=({Maze[newX, newY].basePos.X},{Maze[newX, newY].basePos.Y}) - Maze({newX},{newY}) is added into Queue.");
                                }    
                            }

                        }
                    }
                }
                //test
                // Console.WriteLine("Before sorting: ");
                // foreach(var vertex in priorityQueue)
                // {
                //     Console.WriteLine($" - Maze({vertex.myPos.X},{vertex.myPos.Y}) - G=({vertex.G}) H=({vertex.H}) F=({vertex.F}) basePos=({vertex.basePos.X},{vertex.basePos.Y}) ");
                // }
                // Console.WriteLine("\n==================");

                //sort
                // priorityQueue = priorityQueue.OrderBy(v => v.F).ToList();
                
                //test
                // Console.WriteLine("After sorting: ");
                // foreach(var vertex in priorityQueue)
                // {
                //     Console.WriteLine($" - Maze({vertex.myPos.X},{vertex.myPos.Y}) - G=({vertex.G}) H=({vertex.H}) F=({vertex.F}) basePos=({vertex.basePos.X},{vertex.basePos.Y}) ");
                // }
                // Console.WriteLine("\n==================");
                // Console.ReadLine();

                //if End in queue & poped out
                if (isEndInQueue && baseVertex.myPos.Equals(End.myPos))
                {
                    isEndPoppedOut = true;
                }

                // //test
                // Console.WriteLine($"{priorityQueue.Count}");
                // Console.WriteLine($"{isEndInQueue} && {isEndPoppedOut}");
                // Console.ReadLine();
            }
            if (isEndInQueue && isEndPoppedOut)
            {
                Result = TraceBack(ref Maze, End, Start);
            }
            // //test
            Console.WriteLine($"{isEndInQueue} && {isEndPoppedOut}");
            return isEndInQueue && isEndPoppedOut;
        }

        private static bool IsContainsVertex(ref List<Vertex> queue, Vertex checkingVertex)
        {
            foreach(var vertex in queue)
            {
                if(vertex.myPos.Equals(checkingVertex.myPos))
                {
                    return true;
                }
            }
            return false;
        }

        private static int IndexOfVertexPosition(ref List<Vertex> queue, Vertex checkingVertex)
        {
            for(int i = 0; i < queue.Count ; i++)
            {
                if(queue[i].myPos.Equals(checkingVertex.myPos))
                {
                    return i;
                }
            }
            return -1;
        }

        private static List<Position> TraceBack(ref Vertex[,] maze, Vertex end, Vertex start)
        {
            List<Position> result = new List<Position>();
            Position current = new Position(end.myPos.X, end.myPos.Y);
            // //test
            // Console.WriteLine("\nTraceBack");
            while (!start.myPos.Equals(current))
            {
                result.Add(current);

                // //test
                // Console.Write($"({current.X},{current.Y}) || ");

                current = maze[current.X, current.Y].basePos;
            }
            return result;
        }
    }
}