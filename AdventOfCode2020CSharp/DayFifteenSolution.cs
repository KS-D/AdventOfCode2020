using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020CSharp
{
    class DayFifteenSolution
    {

        public int NumberGame(string input, int range = 2020)
        {
            Dictionary<int, Stack<int>> valueAndTurn = new();
            List<int> parsed = input.Split(",").Select(int.Parse).ToList();
            for (int i = 0; i < parsed.Count; i++)
            {
                valueAndTurn.Add(parsed[i], new Stack<int>());
                valueAndTurn[parsed[i]].Push(i + 1);
            }
            
            int count = valueAndTurn.Count + 1;
            int lastKey = valueAndTurn.Last().Key;
            while (count <= range)
            {
                var lastValue = valueAndTurn[lastKey];
                if (lastValue.Count > 1)
                {
                    int newestIndex = lastValue.Pop();
                    int oldestIndex = lastValue.Pop();
                    lastValue.Push(newestIndex);

                    lastKey = newestIndex - oldestIndex;
                }
                else
                {
                    lastKey = 0;
                }
                
                if (valueAndTurn.ContainsKey(lastKey))
                { 
                    valueAndTurn[lastKey].Push(count);
                }
                else
                { 
                    valueAndTurn.Add(lastKey, new Stack<int>()); 
                    valueAndTurn[lastKey].Push(count);
                }

                count++;
            }
             
            return lastKey;
        }
    }
}
