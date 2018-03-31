using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solution
{
    public static class Solution
    {
        private static int[] GradingStudents(IEnumerable<int> grades)
        {
            return grades.Select(GradeStudent).ToArray();
        }

        private static int GradeStudent(int grade)
        {
            if (grade < LowestRoundUpValue)
            {
                return grade;
            }
            var multiplier = grade / RoundUpToNearest;
            var nextValueUp = (multiplier + 1) * RoundUpToNearest;
            return (nextValueUp - grade > DiffToRoundUp) ? grade : nextValueUp;
        }

        private const int LowestRoundUpValue = 38;
        private const int DiffToRoundUp = 2;
        private const int RoundUpToNearest = 5;

        public static void Main(string[] args)
        {
            TextWriter tw = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);
            var n = Convert.ToInt32(Console.ReadLine());
            var grades = new int[n];

            for (var gradesItr = 0; gradesItr < n; gradesItr++)
            {
                var gradesItem = Convert.ToInt32(Console.ReadLine());
                grades[gradesItr] = gradesItem;
            }

            var result = GradingStudents(grades);

            tw.WriteLine(string.Join("\n", result));

            tw.Flush();
            tw.Close();
        }
    }
}
