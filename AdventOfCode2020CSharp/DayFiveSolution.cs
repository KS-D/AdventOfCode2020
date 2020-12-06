using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020CSharp
{
    class DayFiveSolution
    {
        private List<string> GetInput()
        {
            List<string> seatRules = new();
            StreamReader sr = new("day5.txt");

            while (!sr.EndOfStream)
            {
                string seats = sr.ReadLine();
                if (seats is not null)
                {
                    seatRules.Add(seats);
                }
            }
            
            return seatRules;
        }
        
        public (int row, int col) SolveSeatProblem(string seatRule)
        {
            int frontEdge = 0;
            int backEdge = 127;
            char[] rules = seatRule.ToCharArray();

            int leftEdge = 0;
            int rightEdge = 7;

            foreach (char rule in rules)
            {
                var midDist = 0;
                if (rule == 'F')
                {
                    midDist = (backEdge - frontEdge)/2;
                    backEdge -= midDist + 1;
                } 
                else if (rule == 'B')
                {
                    midDist = (backEdge - frontEdge)/2;
                    ++midDist;
                    frontEdge += midDist;
                }
                else
                {
                    var midColumn = 7;
                    if (rule == 'R')
                    {
                        midColumn = (rightEdge - leftEdge) / 2;
                        leftEdge += midColumn + 1;
                    }
                    else if (rule == 'L')
                    {
                        midColumn = (rightEdge - leftEdge) / 2;
                        rightEdge -= midColumn + 1;
                    }
                }
            }

            if (frontEdge == backEdge && leftEdge == rightEdge)
            {
                return (frontEdge, leftEdge);
            }
            else
            {
                throw new ArithmeticException("A seat was not successfully found");
            }
        }

        public int CalculateSeatId(int row, int col)
        {
            return row * 8 + col;
        }

        public int FindHighestSeatId()
        {
            List<string> seatRule = GetInput();
            int highest = 0;

            foreach (string seat in seatRule)
            {
                (int row, int column) = SolveSeatProblem(seat);
                int temp = CalculateSeatId(row, column);
                if (temp > highest)
                {
                    highest = temp;
                }
            }
            return highest;
        }

        // return a sorted list of ids
        public List<int> GetAllSeatIds()
        {
            List<int> ids = new();
            List<string> seatRule = GetInput();
            
            foreach (string seat in seatRule)
            {
                (int row, int column) = SolveSeatProblem(seat);
                int id = CalculateSeatId(row, column);
                ids.Add(id);
            }
            
            ids.Sort();
            return ids;
        }

        // return negative -1 if not found
        public int FindMySeat(List<int> ids)
        {
            for (int i = 0, j = 1, k = 2; k < ids.Count; ++i, ++j, ++k)
            {
                if (ids[i] != ids[j] - 1) // difference of greater than 1 means a missing seat;
                {
                    return ids[j] - 1;
                }
            }
            
            return -1;
        }
    }
}
