using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solution
{
    public static class Solution
    {
        private static void MiniMaxSum(int[] arr)
        {
            long total = arr.Sum();
            var smallestValue = long.MaxValue;
            var largestValue = long.MinValue;

            foreach (var integer in arr)
            {
                var value = total - integer;
                smallestValue = (value < smallestValue) ? value : smallestValue;
                largestValue = (value > largestValue) ? value : largestValue;
            }
            Console.WriteLine($"{smallestValue} {largestValue}");
        }

        public static void Main(string[] args)
        {
            var arr = Array.ConvertAll(Console.ReadLine()?.Split(' '), Convert.ToInt32);
            MiniMaxSum(arr);
        }
    }
}
