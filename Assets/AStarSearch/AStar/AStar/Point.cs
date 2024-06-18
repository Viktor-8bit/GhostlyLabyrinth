using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;


namespace AStar
{
    public class Point
    {
        public int Id { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public override string ToString()
        {
            return $" Id: {Id} \n x: {x} \n y: {y}";
        }

    }
}