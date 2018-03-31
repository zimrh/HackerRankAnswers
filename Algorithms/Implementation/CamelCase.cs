using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Solution
{
    public static class Solution
    {
        private static int Camelcase(string s)
        {
            // Linq Solution
            var count = s.Count(c => c >= 'A' && c <= 'Z');
            return count + 1;
            
            //Regex solution
            /*
            var uppercaseMatches = Regex.Matches(s, "[A-Z]");
            return uppercaseMatches.Count + 1;
            */
        }

        public static void Main(string[] args)
        {
            var s = Console.ReadLine();
            var result = Camelcase(s);
            Console.WriteLine(result);
        }
    }
}
