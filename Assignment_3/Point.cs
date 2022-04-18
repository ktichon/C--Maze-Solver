using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_3
{
    public class Point
    {
        public int Row { get; set; }
        public int Column { get; set; }

        //Stores where the point came from
        public Point Parent { get; set; }

        public Point(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public override string ToString()
        {
            return $"[{Row}, {Column}]";
        }

        public Point[] PointsAround()
        {
            return new Point[] { new Point(Row + 1, Column), new Point(Row, Column + 1 ), new Point(Row, Column - 1), new Point(Row - 1, Column)};
        }

    }
}
