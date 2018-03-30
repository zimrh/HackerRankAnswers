using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution 
{
    static int birthdayCakeCandles(int n, int[] candles) 
    {        
        /*
        non Linq way of solving this problem
        var largestCandleSize = 0;
        var countOfLargestCandles = 0;
        foreach (var candle in candles)
        {
            if (candle > largestCandleSize)
            {
                largestCandleSize = candle;
                countOfLargestCandles = 0;
            }
            if (candle == largestCandleSize)
            {
                countOfLargestCandles++;
            }
        }
        return countOfLargestCandles;
        */
        
        return candles.GroupBy(c => c).OrderByDescending(c => c.Key).First().Count();
    }

    static void Main(string[] args) 
    {
        TextWriter tw = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine());

        int[] ar = Array.ConvertAll(Console.ReadLine().Split(' '), arTemp => Convert.ToInt32(arTemp))
        ;
        int result = birthdayCakeCandles(n, ar);

        tw.WriteLine(result);

        tw.Flush();
        tw.Close();
    }
}
