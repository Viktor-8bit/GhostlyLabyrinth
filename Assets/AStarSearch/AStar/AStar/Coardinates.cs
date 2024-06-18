using System;
using System.Collections;
using System.Collections.Generic;

namespace AStar
{
    public class Coardinates
    {

        private int[,] I { get; } = new int[512, 512]; // +x +y
        private int[,] II { get; } = new int[512, 512]; // -x +y
        private int[,] III { get; } = new int[512, 512]; // -x -y
        private int[,] IV { get; } = new int[512, 512]; // +x -y

        private int[,] GetMassive(Point point)
        {
            if (point.x >= 0 && point.y >= 0) // +x +y
                return this.I;

            if (point.x < 0 && point.y >= 0) // -x +y
                return this.II;

            if (point.x < 0 && point.y < 0) // -x -y
                return this.III;

            if (point.x >= 0 && point.y < 0) // +x -y
                return this.IV;

            throw new Exception($"{point} error !");
        }

        public void SetValue(Point point)
        {
            int x = point.x < 0 ? point.x * -1 : point.x;
            int y = point.y < 0 ? point.y * -1 : point.y;
            GetMassive(point)[x, y] = point.Id;
        }

        public int GetValue(Point point)
        {
            int x = point.x < 0 ? point.x * -1 : point.x;
            int y = point.y < 0 ? point.y * -1 : point.y;
            return GetMassive(point)[x, y];
        }

        public int GetValue(int x, int y)
        {
            var point = new Point() { x=x, y=y };
            x = point.x < 0 ? point.x * -1 : point.x;
            y = point.y < 0 ? point.y * -1 : point.y;
            return GetMassive(point)[x, y];
        }

    }
}