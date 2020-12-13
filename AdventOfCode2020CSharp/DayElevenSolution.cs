using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020CSharp
{
    class DayElevenSolution
    {
        public List<string> GetInput(string inputFile)
        {
            List<string> adapters = new();
            using StreamReader sr = new(inputFile);

            while (!sr.EndOfStream)
            {
                string rule = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(rule))
                {
                    adapters.Add(rule);
                }
            }
            return adapters;
        }

        public char[][] GetSeats(List<string> input) => input.Select(x => x.ToCharArray()).ToArray();

        public char[][] FillSeats(char[][] seats)
        {
            char[][] newSeats = CloneChars(seats);
            
            for (int i = 0; i < seats.Length; i++)
            {
                for (int j = 0; j < seats[0].Length; j++)
                {
                    char spot = seats[i][j];
                    if (spot == 'L' && CheckBounds(seats, i, j, '#') == 0)
                    {
                        newSeats[i][j] = '#';
                    }

                    if (spot == '#' && CheckBounds(seats, i, j, '#') >= 4)
                    {
                        newSeats[i][j] = 'L';
                    }
                }
            }

            return newSeats;
        }

        public int CountFullSeats(char[][] seats) =>  seats.SelectMany(s => s).ToArray().Count(c => c == '#');
        
        private char[][] CloneChars(char[][] seats)
        {
            char[][] temp = new char[seats.Length][];
            int count = 0;

            for (int i = 0; i < seats.Length; i++)
            {
                temp[i] = new char[seats[0].Length];
                for (int j = 0; j < seats[0].Length; j++)
                {
                    temp[i][j] = seats[i][j];
                }
            }

            return temp;
        }

        // Part 1
        private int CheckBounds(char[][] seats, int row, int col, char seatState)
        {
            // need logic for tile
            int seatsFilled = 0;
            int downBound = row + 1;
            int upBound = row - 1;
            int leftBound = col - 1;
            int rightBound = col + 1;
            bool canGoUp = upBound >= 0;
            bool canGoDown = downBound < seats.Length;
            bool canGoLeft = leftBound >= 0;
            bool canGoRight = rightBound < seats[0].Length;
           
            if (canGoUp && seats[upBound][col] == seatState)
            {
                seatsFilled++;
            }

            if (canGoDown && seats[downBound][col] == seatState )
            {
                seatsFilled++;
            }

            if (canGoLeft && seats[row][leftBound] == seatState)
            {
                seatsFilled++;
            }
            
            if (canGoRight && seats[row][rightBound] == seatState)
            {
                seatsFilled++;
            }

            if (canGoUp && canGoRight && seats[upBound][rightBound] == seatState)
            {
                seatsFilled++;
            }

            if (canGoUp && canGoLeft && seats[upBound][leftBound] == seatState)
            {
                seatsFilled++;
            }

            if (canGoDown && canGoRight && seats[downBound][rightBound] == seatState)
            {
                seatsFilled++;
            }

            if (canGoDown && canGoLeft && seats[downBound][leftBound] == seatState)
            {
                seatsFilled++;
            }
             
            return seatsFilled;
        }
        
        // Part 2
        /* need to check line of sight for each seat. calculate all
           the indexes in line of sight for current position and then
         */

        public int GenerateLineOfSite(int row, int col, int rowBound, int colBound, char[][] seats)
        {
            var rowRange = Enumerable.Range(0, rowBound).Where(x => x != row).ToList();
            var aboveRow= rowRange.Where(r => r < row).Reverse().ToList();
            var belowRow = rowRange.Where(r => r > row).ToList();

           
            var leftColRange = Enumerable.Range(0, col);
            var rightColRange = Enumerable.Range(col + 1, colBound - col - 1);
            int slope1 = 1;
            int slope2 = -1;
            int b1 = YIntersect(col, row, slope1);
            int b2 = YIntersect(col, row, slope2);
            var upBelowDiagonal = GetDiagonalsRowCol(b1, slope1, belowRow, rowBound, colBound);
            var upAboveDiagonal = GetDiagonalsRowCol(b1, slope1, aboveRow, rowBound, colBound);
            var downBelowDiagonal = GetDiagonalsRowCol(b2, slope2, belowRow, rowBound, colBound);
            var downAboveDiagonal = GetDiagonalsRowCol(b2, slope2, aboveRow, rowBound, colBound);

//#if DEBUG
//            Console.WriteLine("Lcol");
//            foreach (var lCol in leftColRange)
//            {
//                Console.WriteLine(lCol);
//            }

//            Console.WriteLine("rCol");
//            foreach (var rCol in rightColRange)
//            {
//                Console.WriteLine(rCol); 
//            }

//            Console.WriteLine("aboveRow");
//            foreach (var aRow in aboveRow)
//            {
//                Console.WriteLine(aRow);
//            }

//            Console.WriteLine("belowRow");
//            foreach (var bRow in belowRow)
//            {
//                Console.WriteLine(bRow);
//            }

//            Console.WriteLine("upBelowDiagonal");
//            foreach (var upBDiag in upBelowDiagonal)
//            {
//                Console.WriteLine(upBDiag);
//            }

//            Console.WriteLine("UpAboveDiagonal");
//            foreach (var upADiag in  upAboveDiagonal)  
//            {
//                Console.WriteLine(upADiag);
//            }

//            Console.WriteLine("DownBelowDiagonal");
//            foreach (var dbDiagonal in downBelowDiagonal)
//            {
//                Console.WriteLine(dbDiagonal);
//            }

//            Console.WriteLine("downAboveDiagonal");
//            foreach (var downAdiag in downAboveDiagonal)
//            {
//                Console.WriteLine(downAdiag);
//            }
//#endif
            
            int seatsInView = 0;
           
            foreach (var r in belowRow)
            {
                var pos = seats[r][col];
                if (pos == '#')
                {
                    seatsInView++;
                    break;
                }

                if (pos == 'L')
                {
                    break;
                }
            }

            foreach (var r in aboveRow)
            {
                var pos = seats[r][col];
                if (pos == '#')
                {
                    seatsInView++;
                    break;
                }

                if (pos == 'L')
                {
                    break;
                }
            }
            
            foreach (var lCol in leftColRange.Reverse())
            {
                var pos = seats[row][lCol];
                if (pos == '#')
                {
                    seatsInView++;
                    break;
                }
                
                if (pos == 'L')
                {
                    break;
                }
            }

            foreach (var rCol in rightColRange)
            {
                var pos = seats[row][rCol];
                if (pos == '#')
                {
                    seatsInView++;
                    break;
                }

                if (pos == 'L')
                {
                    break;
                }
            }

            foreach (var diag in upBelowDiagonal)
            {
                var pos = seats[diag.Key][diag.Value];
                if (pos == '#')
                {
                    seatsInView++;
                    break;
                }
                
                if (pos == 'L')
                {
                    break;
                }
            }
            
            foreach (var diag in upAboveDiagonal)
            {
                var pos = seats[diag.Key][diag.Value];
                if (pos == '#')
                {
                    seatsInView++;
                    break;
                }
                
                if (pos == 'L')
                {
                    break;
                }
            }
            
            foreach (var diag in downAboveDiagonal)
            {
                var pos = seats[diag.Key][diag.Value];
                if (pos == '#')
                {
                    seatsInView++;
                    break;
                }
                
                if (pos == 'L')
                {
                    break;
                }
            }
            
            foreach (var diag in downBelowDiagonal)
            {
                var pos = seats[diag.Key][diag.Value];
                if (pos == '#')
                {
                    seatsInView++;
                    break;
                }
                
                if (pos == 'L')
                {
                    break;
                }
            }
 
            return seatsInView;

        }
        
        public char[][] FillSeatsPart2(char[][] seats)
        {
            char[][] newSeats = CloneChars(seats);
            
            for (int i = 0; i < seats.Length; i++)
            {
                for (int j = 0; j < seats[0].Length; j++)
                {
                    char spot = seats[i][j];
                    if (spot == 'L' && 
                        GenerateLineOfSite(i, j, seats.Length, seats[0].Length, seats) == 0)
                    {
                        newSeats[i][j] = '#';
                    }

                    if (spot == '#' && 
                        GenerateLineOfSite(i, j, seats.Length, seats[0].Length, seats) >= 5)
                    {
                        newSeats[i][j] = 'L';
                    }
                }
            }

            return newSeats;
        }

 
        //Does not return negative numbers
        public Dictionary<int, int> GetDiagonalsRowCol(int b, int slope, IEnumerable<int> rowRange, int rowBound, int colBound)
        {
            var diagonal = rowRange.Select(y => GenerateDiagonalPoints(y, b, slope))
                                                    .Where(c => c.row >= 0 && c.row < rowBound && c.col >= 0 && c.col < colBound)
                                                    .ToDictionary(x=>x.row, 
                                                                  x => x.col);
            return diagonal;
        }
        public int YIntersect(int x, int y, int slope)
        {
            int b = y - (x * slope);
            return b;
        }
        // must subract 1 to adjust for the zero offset
        public (int row, int col) GenerateDiagonalPoints(int y, int b, int slope)
        {
            int x = (y -b) * slope;
            return (y, x); //adjust for 0 offset
        } 
    }
}
