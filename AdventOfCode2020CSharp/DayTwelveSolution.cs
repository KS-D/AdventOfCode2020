using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic;

namespace AdventOfCode2020CSharp
{
    class DayTwelveSolution
   {
       internal enum Direction
       {
           East = 0,
           North = 90,
           West = 180,
           South = 270
       }
       
        public int North { get; set; }
        public int East { get; set; }
        public int WayPointNorth { get; set; } = 1;
        public int WayPointEast { get; set; } = 10;
        public Direction ShipDirection { get; set; } = Direction.East;
        public int Rotation { get; private set; } // maybe set rotation to an enum
        public List<string> Instructions { get; set; }

        public DayTwelveSolution()
        {
            North = 0;
            East = 0;
            Rotation = 0;
            Instructions = new();
        }
        
        public void GetDirections(string inputFile)
        {
            using StreamReader sr = new(inputFile);

            while (!sr.EndOfStream)
            {
                string rule = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(rule))
                {
                    Instructions.Add(rule);
                }
            }
        }

        public int ManhattanDistance() => Math.Abs(East) + Math.Abs(North);

        public void Rotate(string r, int degrees)
        {
            if (r == "R")
            {
                int temp = Rotation - degrees;
                if (temp < 0)
                {
                    Rotation = 360 + temp;
                }
                else
                {
                    Rotation = temp;
                }
            }
            else
            {
                int temp = Rotation + degrees;
                if (temp >= 360)
                {
                    Rotation = temp - 360;
                } 
                else
                {
                    Rotation = temp;
                }
            }

            ShipDirection = Enum.Parse<Direction>(Rotation.ToString());
        }

        // part 1
        public void Parse()
        {
            foreach (var direction in Instructions)
            {
                int value = int.Parse(direction.Substring(1));

                if (direction.Contains("N"))
                {
                    North += value;
                }    
                else if (direction.Contains("S"))
                {
                    North -= value;
                }
                else if (direction.Contains("E"))
                {
                    East += value;
                }
                else if (direction.Contains("W"))
                {
                    East -= value;
                }
                else if (direction.Contains("L"))
                {
                    Rotate("L", value); 
                }
                else if (direction.Contains("R"))
                {
                    Rotate("R", value); 
                }
                else if (direction.Contains("F"))
                {
                    switch (ShipDirection)
                    {
                        case Direction.East:
                            East += value;
                            break;
                        case Direction.West:
                            East -= value;
                            break;
                        case Direction.North:
                            North += value;
                            break;
                        case Direction.South:
                            North -= value; 
                            break;
                    }
                }
            }
        }
        // part 2;
        public void ParseWithWayPoint()
        {
            foreach (var direction in Instructions)
            {
                int value = int.Parse(direction.Substring(1));

                if (direction.Contains("N"))
                {
                    WayPointNorth += value;
                }    
                else if (direction.Contains("S"))
                {
                    WayPointNorth -= value;
                }
                else if (direction.Contains("E"))
                {
                    WayPointEast += value;
                }
                else if (direction.Contains("W"))
                {
                    WayPointEast -= value;
                }
                else if (direction.Contains("L"))
                {
                    RotateWayPoint("L", value); 
                }
                else if (direction.Contains("R"))
                {
                    RotateWayPoint("R", value); 
                }
                else if (direction.Contains("F"))
                {
                    East += value * WayPointEast; 
                    North += value * WayPointNorth;
                }
            }
        }

        public void RotateWayPoint(string r, int degrees)
        {
            // if the way point North is positive and East is positive then it is in the first quadrant
            int moveQuadrants = degrees / 90;
            if (r == "L")
            {
                // 1 and 3 are different quadrants while 4 and 2 are 180 and 360 so they will 
                // produce the same results
                if (moveQuadrants == 1)                 
                {
                    moveQuadrants = 3;
                }
                else if (moveQuadrants == 3)
                {
                    moveQuadrants = 1;
                }
            }
            // everything is done in terms of right rotations 
            foreach (var quadrants in Enumerable.Range(0, moveQuadrants))
            {
                Console.WriteLine(quadrants);

                var temp = WayPointNorth;
                WayPointNorth = -WayPointEast;
                WayPointEast = temp;
            }
        }
    }
}
