using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode2020CSharp
{
    class DayFourSolutions
    {
        public List<string> GetInput()
        {
            StreamReader sr = new("day4.txt");
            List<string> passports = new();

            StringBuilder sb = new("");
            while (!sr.EndOfStream)
            {
                string temp = sr.ReadLine();

                if (!string.IsNullOrEmpty(temp))
                {
                    sb.Append(temp + " ");
                }
                else
                {
                    passports.Add(sb.ToString());
                    sb.Clear();
                }
            }

            passports.Add(sb.ToString());

            return passports;
        }

        public int GetValidPassports(List<string> passports, Regex rx)
        {
            int count = 0;

            foreach (var s in passports)
            {
                if (rx.IsMatch(s))
                {
                    ++count;
                }
            }

            return count;
        }
    }
}