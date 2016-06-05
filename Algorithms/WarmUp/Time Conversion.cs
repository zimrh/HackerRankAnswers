using System;

namespace HackerRank
{
    class Solution {

        static void Main(String[] args)
        {
        
            string time = Console.ReadLine();
        
            var hours = int.Parse(time.Substring(0, 2));
            var minutes = int.Parse(time.Substring(3, 2));
            var seconds = int.Parse(time.Substring(6, 2));

            if (hours != 12 && time.EndsWith("PM")) hours += 12;
            if (hours == 12 && time.EndsWith("AM")) hours = 0;

            Console.WriteLine($"{hours:d2}:{minutes:d2}:{seconds:d2}");
        }
    }
}