using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020CSharp
{
    class DayOneSolution
    {
        public List<int> GetExpenses()
        {
            var stream = new StreamReader("Day1.txt");
            List<int> expenses = new List<int>();
            while (!stream.EndOfStream)
            {
                string expense = stream.ReadLine();
                if (expense is not null)
                {
                    expenses.Add(Int32.Parse(expense));
                }
            }

            return expenses;
        }

        public int SolveExpenseReport(List<int> expenses)
        {
            expenses.Sort();
            int low = 0;
            int high = expenses.Count - 1;
            while (low < high)
            {
                int sum = expenses[low] + expenses[high];
                if (sum == 2020)
                {
                    return expenses[low] * expenses[high];
                }
                else if (sum > 2020)
                {
                    --high;
                }
                else
                {
                    ++low;
                }
            }

            return -1;
        }

        public int SolveExpenseReport2(List<int> expenses)
        {
            for (int start = 0; start < expenses.Count - 3; start++)
            {
                for (int mid = start + 1, end = expenses.Count - 1; mid < end; /* incremented in the loop */)
                {
                    int sum = expenses[start] + expenses[mid] + expenses[end];

                    if (sum == 2020)
                    {
                        Console.WriteLine($"start: {expenses[start]} mid: {expenses[mid]} end: {expenses[end]}");
                        return expenses[start] * expenses[mid] * expenses[end];
                    }
                    else if (sum > 2020)
                    {
                        --end;
                    }
                    else
                    {
                        ++mid;
                    }
                }
            }

            return 0;
        }
    }
}