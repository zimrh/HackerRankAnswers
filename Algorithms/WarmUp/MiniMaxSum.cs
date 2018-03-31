using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solution
{
    public static class Solution
    {
        // Turns out .Sum() cannot handle the size of the integers, Aggregate
        // would work fine but thinking about this I decided that it would be 
        // better to just iterate a single time over the array so changed it 
        // to a foreach and picked the smallest and largest values to use to return the answer
        private static void MiniMaxSum(IEnumerable<int> arr)
        {
            long total = 0;
            var smallestInput = int.MaxValue;
            var largestInput = int.MinValue;

            foreach (var i in arr)
            {
                total += i;
                smallestInput = (i < smallestInput) ? i : smallestInput;
                largestInput = (i > largestInput) ? i : largestInput;
            }
            Console.WriteLine($"{total - largestInput} {total - smallestInput}");
        }

        public static void Main(string[] args)
        {
            var arr = Array.ConvertAll(Console.ReadLine()?.Split(' '), Convert.ToInt32);
            MiniMaxSum(arr);
        }
    }
}
