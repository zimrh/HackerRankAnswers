using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solution
{
    public static class Solution
    {
        static int sockMerchant(int n, IEnumerable<int> socks)
        {
            var sockColourCount = new Dictionary<int, int>();
            foreach (var sock in socks)
            {
                if (!sockColourCount.ContainsKey(sock))
                {
                    sockColourCount.Add(sock, 0);
                }
                sockColourCount[sock]++;
            }
            return sockColourCount.Sum(i => i.Value / 2);
        }

        public static void Main(string[] args)
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var arTemp = Console.ReadLine().Split(' ');
            var ar = Array.ConvertAll(arTemp, int.Parse);
            var result = sockMerchant(n, ar);
            Console.WriteLine(result);
        }
    }
}
