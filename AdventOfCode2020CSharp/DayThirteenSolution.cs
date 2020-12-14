using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace AdventOfCode2020CSharp
{
    class DayThirteenSolution
    {
        public int ArrivalTime { get; set; } = -1;
        public List<int> Buses { get; set; } = new();
        public Dictionary<int, int> BusOffset { get; set; } = new();

        public void GetInput(string inputFile)
        {
            List<string> busSchedule = new();
            using StreamReader sr = new(inputFile);
            while (!sr.EndOfStream)
            {
                var temp = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(temp))
                {
                    busSchedule.Add(temp);
                }
            }

            ArrivalTime = int.Parse(busSchedule[0]);
            var splitBuses = busSchedule[1].Split(",");

            Buses = splitBuses.Where(b => b != "x")
                .Select(int.Parse)
                .ToList();

            BusOffset = GenerateBussOffset(splitBuses);
        }

        private Dictionary<int, int> GenerateBussOffset(string[] unParsed)
        {
            Dictionary<int, int> busOffset = new();
            for (int i = 0; i < unParsed.Length; i++)
            {
                if (unParsed[i] != "x")
                {
                    var key = int.Parse(unParsed[i]);
                    if (i != 0)
                    {
                        busOffset.Add(key, key - i);
                    }
                    else
                    {
                        busOffset.Add(key, 0);
                    }
                }
            }

            return busOffset;
        }
        
        // part 1
        public int FindRemainder()
        {
            double[] quotientCeiling = new double[Buses.Count];
            double[] differences = new double[Buses.Count];
            int count = 0;
            foreach (var bus in Buses)
            {
                quotientCeiling[count] = Math.Ceiling(ArrivalTime / (double) bus);
                differences[count] = (quotientCeiling[count] * bus) - ArrivalTime;
                count++;
            }

            double min = differences.Min();
            int i;
            for (i = 0; i < differences.Length; i++)
            {
                if (Math.Abs(differences[i] - min) < .2)
                {
                    break;
                }
            }

            int wait = Buses[i] * (int) min;

            return wait;
        }
        
        // part 2
        // naive solution, too slow for the real data
        public long TimeStampOffset()
        {
            var pair = BusOffset.First();
            BusOffset.Remove(pair.Key);
            long interval = pair.Key;
            int[] buses = BusOffset.Keys.ToArray();
            int[] tOffset = BusOffset.Values.ToArray();

            long start = -interval;
            int count = 0;
            while (count != buses.Length)
            { 
                start += interval;
                count = 0;
                List<long> validDepartures = new();
                foreach (var t in tOffset)
                {
                    validDepartures.Add(t + start);
                }

                for (int i = 0; i < validDepartures.Count; i++)
                {
                    if (validDepartures[i] % buses[i] == 0)
                    {
                        count++;
                    }
                    else
                    {
                        break; // if one doesn't match then move on;
                    }
                }


            }
            
            return start;
        }
        
        // FindTimeStampCRT by Sieving, Still takes to long for my problem
        public long FindTimeStampCRTSieve()
        {
                        
            List<int> buses = BusOffset.Keys.ToList();
            buses.Sort((x, y) => y.CompareTo(x)); // descending order
            
            int largestBusId = buses[0];

            long start = buses[0] + BusOffset[buses[0]];
            int count = 0;
            long sumModulo = 0; // start at the first index
            while (count != buses.Count)
            {
                int busId = buses[count];
                if (start % busId == BusOffset[busId])
                {
                    if (sumModulo == 0)
                    {
                        sumModulo = busId;
                    }
                    else
                    {
                        sumModulo *= busId;
                    }
                    count++;
                }
                else
                {
                    start += sumModulo;
                }
            }

            Console.WriteLine(count);

            return start;
        }

        public long FindTimeStampCRTCon()
        { 
            long N = 1;
            foreach(var i in BusOffset.Keys)
            {
                N *= i;
            }
            
            int[] buses = BusOffset.Keys.ToArray();
            long sum = 0;
            long nDivNsub;
            for (int i = 0; i < buses.Length; i++)
            {
                nDivNsub = N / buses[i];
                sum += BusOffset[buses[i]] * ModMultInverse(nDivNsub, buses[i]) * nDivNsub;
            }
            
            return sum % N;
        }

        public long ModMultInverse(long a, long m)
        {
            long m0 = m;
            long y = 0, x = 1;

            if (m == 1)
            {
                return 1;
            }

            while (a > 1)
            {
                long q = a / m;

                long temp = m;
                m = a % m;
                a = temp;
                temp = y;

                y = x - q * y;
                x = temp;
            }

            if (x < 0)
            {
                x += m0;
            }

            return x;
        }
        
    }
}