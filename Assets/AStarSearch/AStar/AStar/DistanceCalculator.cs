using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AStar
{
    public class DistanceCalculator : MonoBehaviour
    {


        private Coardinates Map;
        public DistanceCalculator(Coardinates map)
        {
            this.Map = map;
        }

        private Coardinates _temp = new Coardinates();


        // TODO сжечь этот код и написать заново  
        public Coardinates CalculateMap(Point playerPoint)
        {
            _temp = new Coardinates();
            _temp.SetValue(new Point() { Id = 1, x = playerPoint.x, y = playerPoint.y });

            List<Point> aroundPoints = new List<Point>();

            aroundPoints.Add(new Point() { Id = 1, x = playerPoint.x, y = playerPoint.y });

            while (aroundPoints.Count != 0)
            {
                Point temPoint;

                // right
                temPoint = new Point() { x = aroundPoints[0].x, y = aroundPoints[0].y };
                temPoint.x = aroundPoints[0].x + 1;

                if (Map.GetValue(temPoint) == 0 && _temp.GetValue(temPoint) == 0)
                {
                    Point deltaPoint = new Point() { Id = aroundPoints[0].Id + 1, y = aroundPoints[0].y, x = aroundPoints[0].x + 1 };
                    _temp.SetValue(deltaPoint);
                    aroundPoints.Add(deltaPoint);
                }

                // left
                temPoint = new Point() { x = aroundPoints[0].x, y = aroundPoints[0].y };
                temPoint.x = aroundPoints[0].x - 1;

                if (Map.GetValue(temPoint) == 0 && _temp.GetValue(temPoint) == 0)
                {
                    Point deltaPoint = new Point() { Id = aroundPoints[0].Id + 1, y = aroundPoints[0].y, x = aroundPoints[0].x - 1 };
                    _temp.SetValue(deltaPoint);
                    aroundPoints.Add(deltaPoint);  
                }

                // Up
                temPoint = new Point() { x = aroundPoints[0].x, y = aroundPoints[0].y };
                temPoint.y = aroundPoints[0].y + 1;

                if (Map.GetValue(temPoint) == 0 && _temp.GetValue(temPoint) == 0)
                {
                    Point deltaPoint = new Point() { Id = aroundPoints[0].Id + 1, y = aroundPoints[0].y + 1, x = aroundPoints[0].x };
                    _temp.SetValue(deltaPoint);
                    aroundPoints.Add(deltaPoint);
                }

                // Down
                temPoint = new Point() { x = aroundPoints[0].x, y = aroundPoints[0].y };
                temPoint.y = aroundPoints[0].y - 1;

                if (Map.GetValue(temPoint) == 0 && _temp.GetValue(temPoint) == 0)
                {
                    Point deltaPoint = new Point() { Id = aroundPoints[0].Id + 1, y = aroundPoints[0].y - 1, x = aroundPoints[0].x };
                    _temp.SetValue(deltaPoint);
                    aroundPoints.Add(deltaPoint);
                }

                aroundPoints.Remove(aroundPoints[0]);
            }

            return _temp;
        }

    }
}
