using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020CSharp
{
    class DaySixSolution
    {
        public List<string> GetQuestionInput()
        {
            using StreamReader sr = new("day6.txt");
            List<string> questions = new();
            StringBuilder sb = new("");

            while (!sr.EndOfStream)
            {
                string temp = sr.ReadLine();

                if (!string.IsNullOrWhiteSpace(temp))
                {
                    sb.Append(temp + " ");
                }
                else
                {
                    questions.Add(sb.ToString());
                    sb.Clear();
                }
            }

            questions.Add(sb.ToString());
            sb.Clear();

            return questions;
        }

        public int GetUniqueYeses(string questions)
        {
            var unique = questions.Where(letter => letter != ' ' && letter != '\n')
                .Distinct();
            return unique.Count();
        }

        public int GetDuplicateYeses(string question)
        {
            int groupMembers = question.Trim().Split(" ").Length;
            var duplicate = question.GroupBy(l => l)
                .Where(g => g.Count() == groupMembers && g.Key != ' ')
                .Select(l => l);
            return duplicate.Count();
        }

        public int SolveTotalQuestions(List<string> questions)
        {
            int sum = 0;
            foreach (string question in questions)
            {
                sum += GetUniqueYeses(question);
            }

            return sum;
        }

        public int SolveDuplicateQuestions(List<string> questions)
        {
            int sum = 0;
            foreach (string question in questions)
            {
                sum += GetDuplicateYeses(question);
            }

            return sum;
        }
    }
}