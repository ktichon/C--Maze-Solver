using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    class Maze
    {
        private char[][] maze;
        private Stack<Point> pathStack;
        private Queue<Point> searchQueue;
        public Maze(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            int rows = int.Parse(lines[0].Split(' ')[0]);
            int[] startingPoints = lines[1].Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            char[][] newMaze = new char[rows][];
            for(int line = 0; line < rows; line ++)
            {
                newMaze[line] = lines[line + 2].ToCharArray();
            }
            SetMaze(startingPoints[0], startingPoints[1], newMaze);

        }

        

        public Maze(int startingRow, int StartingColumn, char[][] existingMaze)
        {
            SetMaze(startingRow, StartingColumn, existingMaze);
        }

        public Point StartingPoint { get; private set; }

        public int RowLength { get { return GetMaze().Length; }  }

        public int ColumnLength { get { return GetMaze()[0].Length; } }


        public char[][] GetMaze()
        {
            return maze;
        }

        public string PrintMaze()
        {
            string mazeString = "";
            for(int row = 0; row < RowLength; row ++)
                mazeString += new string(GetMaze()[row]) + '\n';
            mazeString = mazeString.Substring(0, mazeString.Length -1);
            return mazeString;
        }

        public string DepthFirstSearch()
        {
            pathStack = new Stack<Point>();
            searchQueue = null;
            pathStack.Push(StartingPoint);
            
            while (!pathStack.IsEmpty())
            {
                Point currentPoint = pathStack.Top();
                if (Char.ToUpper(GetMaze()[currentPoint.Row][currentPoint.Column]) == 'E')
                    break;
                maze[currentPoint.Row][currentPoint.Column] = 'V';
                               
              
                if (!CheckPoint(currentPoint.Row + 1, currentPoint.Column))
                    if (!CheckPoint(currentPoint.Row, currentPoint.Column + 1))
                        if (!CheckPoint(currentPoint.Row, currentPoint.Column - 1))
                            if (!CheckPoint(currentPoint.Row - 1, currentPoint.Column))
                                pathStack.Pop();
            }


            string depthMessage;

            if (pathStack.IsEmpty())
                depthMessage = "No exit found in maze!\n\n";
            else
            {
                depthMessage = $"Path to follow from Start {StartingPoint} to Exit {pathStack.Top()} - {pathStack.Size} steps:\n";
                Stack<Point> orderedPath = GetPathToFollow();
                while (!orderedPath.IsEmpty())
                    depthMessage += orderedPath.Pop().ToString() + "\n";
            }

            return depthMessage + PrintMaze();
        }

        public Stack<Point> GetPathToFollow()
        {     
            
            if(pathStack == null)
               throw new ApplicationException();
            Stack<Point> orderedStack = new Stack<Point>();
            if (searchQueue == null)
            {
                Node<Point> cloned = pathStack.Head;               

                while (cloned != null)
                {
                    orderedStack.Push(cloned.Element);
                    if (char.ToUpper(maze[cloned.Element.Row][cloned.Element.Column]) != 'E')
                        maze[cloned.Element.Row][cloned.Element.Column] = '.';
                    cloned = cloned.Previous;
                }
            }
            else
            {
                orderedStack = (Stack<Point>) pathStack.Clone();
            }
            

            return  orderedStack;
        }      

        /// <summary>
        /// Sets the Maze. Used in both constructors.
        /// </summary>
        /// <param name="startingRow">Starting x</param>
        /// <param name="StartingColumn">Starting Y</param>
        /// <param name="existingMaze">Maze</param>
        private void SetMaze(int startingRow, int StartingColumn, char[][] existingMaze)
        {            
            StartingPoint = new Point(startingRow, StartingColumn);
            maze = existingMaze.Clone() as Char[][];
            char startValue = maze[startingRow][StartingColumn];
            if (Char.ToUpper(startValue)== 'E' || Char.ToUpper( startValue ) == 'W')
                throw new ApplicationException();
        }

        /// <summary>
        /// Checks to see if a point on the maze is a valid, undiscovered path. If it is, add point to stack.       
        /// </summary>
        /// <param name="row">X of point</param>
        /// <param name="collum">Y of point</param>
        /// <returns>if valid</returns>
        private bool CheckPoint(int row, int collum)
        {
            bool canMove = false;
            if (GetMaze()[row][collum] != 'W' && GetMaze()[row][collum] != 'V')
            {
                canMove = true;
                pathStack.Push(new Point(row, collum));
            }

            return canMove;
        }

        //Stack<Point> orderedPath = GetPathToFollow();
        //        while(!orderedPath.IsEmpty())
        //        {
        //            Point pathPoint = orderedPath.Pop();
        //            if (maze[pathPoint.Row][pathPoint.Column] != 'E')
        //                maze[pathPoint.Row][pathPoint.Column] = '.';
        //            depthMessage += pathPoint.ToString() + "\n";
        //        }
        // Replaces the above code in the search method. FlipOrder is 1 line o
        //private string flipOrder(int iteration, Node<Point> pointNode)
        //    {
        //        if (pointNode == null)
        //            return "";

        //        if (maze[pointNode.Element.Row][pointNode.Element.Column] != 'E')
        //            maze[pointNode.Element.Row][pointNode.Element.Column] = '.';
        //        return flipOrder(iteration + 1, pointNode.Previous) + pointNode.Element.ToString() + '\n';
        //    }



        //-----------------------------------------------------Assignment 3 Milestone 2-----------------------------------------------------------
        public string BreadthFirstSearch()
        {
            pathStack = new Stack<Point>();
            searchQueue = new Queue<Point>();
            searchQueue.Enqueue(StartingPoint);
            Point endPoint = StartingPoint;
            while (!searchQueue.IsEmpty())
            {
                Point currentPoint = searchQueue.Dequeue();

                if (Char.ToUpper(GetMaze()[currentPoint.Row][currentPoint.Column]) == 'E')
                {
                    endPoint = currentPoint;
                    break;
                }

                maze[currentPoint.Row][currentPoint.Column] = 'V';
                foreach (Point checkPoint in currentPoint.PointsAround())
                {
                    if (GetMaze()[checkPoint.Row][checkPoint.Column] != 'W' && GetMaze()[checkPoint.Row][checkPoint.Column] != 'V')
                    {
                        checkPoint.Parent = currentPoint;
                        searchQueue.Enqueue(checkPoint);
                    }
                }

            }
            String depthMessage;
            if (searchQueue.IsEmpty())
                depthMessage = "No exit found in maze!\n\n";
            else
            {
                FindPath(endPoint);
                depthMessage = $"Path to follow from Start {StartingPoint} to Exit {endPoint} - {pathStack.Size} steps:\n";
                Stack<Point> orderedPath = GetPathToFollow();
                while (!orderedPath.IsEmpty())
                    depthMessage += orderedPath.Pop().ToString() + "\n";
            }

            return depthMessage + PrintMaze();
        }

        /// <summary>
        /// Fills the pathStack with quickest path
        /// </summary>
        /// <param name="pointToAdd">Current Point to add to path</param>
        private void FindPath(Point pointToAdd)
        {
            pathStack.Push(pointToAdd);
            if (char.ToUpper(maze[pointToAdd.Row][pointToAdd.Column]) != 'E')
                maze[pointToAdd.Row][pointToAdd.Column] = '.';

            if (pointToAdd != StartingPoint && pointToAdd.Parent != null)
                FindPath(pointToAdd.Parent);
        }


    }
}
