using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution 
{
    // This would be even simpler if they did not pass in each variable individually!
    static int[] solve(int a0, int a1, int a2, int b0, int b1, int b2) 
    {
        var scores = new int[2]{0,0};
        CheckAndIncrement(scores, a0, b0);
        CheckAndIncrement(scores, a1, b1);
        CheckAndIncrement(scores, a2, b2);
        return scores;
    }

    private static void CheckAndIncrement(int[] scores, int playerOneScore, int playerTwoScore)
    {
        if (playerOneScore > playerTwoScore) { scores[0]++; }
        if (playerTwoScore > playerOneScore) { scores[1]++; }
    }
    
    static void Main(string[] args) 
    {
        TextWriter tw = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] a0A1A2 = Console.ReadLine().Split(' ');
        int a0 = Convert.ToInt32(a0A1A2[0]);
        int a1 = Convert.ToInt32(a0A1A2[1]);
        int a2 = Convert.ToInt32(a0A1A2[2]);

        string[] b0B1B2 = Console.ReadLine().Split(' ');
        int b0 = Convert.ToInt32(b0B1B2[0]);
        int b1 = Convert.ToInt32(b0B1B2[1]);
        int b2 = Convert.ToInt32(b0B1B2[2]);

        int[] result = solve(a0, a1, a2, b0, b1, b2);
        tw.WriteLine(string.Join(" ", result));
        tw.Flush();
        tw.Close();
    }
}
