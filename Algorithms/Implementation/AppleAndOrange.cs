using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solution
{
    public static class Solution
    {
        private static void CountApplesAndOranges(int s, int t, int a, int b, IEnumerable<int> apples, IEnumerable<int> oranges)
        {
            Console.WriteLine(CountFruitThatLandedOnRoof(s, t, a, apples));
            Console.WriteLine(CountFruitThatLandedOnRoof(s, t, b, oranges));
        }

        private static int CountFruitThatLandedOnRoof(int roofStart, int roofEnd, int pos, IEnumerable<int> fruits)
        {
            return fruits
                .Select(fruit => pos + fruit)
                .Count(landedAt => landedAt >= roofStart && landedAt <= roofEnd);
        }
        
        public static void Main(string[] args)
        {
            var st = Console.ReadLine().Split(' ');
            var s = Convert.ToInt32(st[0]);
            var t = Convert.ToInt32(st[1]);

            var ab = Console.ReadLine().Split(' ');
            var a = Convert.ToInt32(ab[0]);
            var b = Convert.ToInt32(ab[1]);

            Console.ReadLine();
            //var m = Convert.ToInt32(mn[0]);
            //var n = Convert.ToInt32(mn[1]);

            var apple = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToInt32);
            var orange = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToInt32);

            CountApplesAndOranges(s, t, a, b, apple, orange);
        }
    }
}
