using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution 
{
    private static string CatAndMouse(int catALocation, int catBLocation, int mouseC) 
    {
        var distanceFromCatA = Math.Abs(mouseC - catALocation);
        var distanceFromCatB = Math.Abs(mouseC - catBLocation);

        if (distanceFromCatA == distanceFromCatB)
        {
            return MouseC;
        }

        return (distanceFromCatA > distanceFromCatB) ? CatB : CatA;
    }

    private const string CatA = "Cat A";
    private const string CatB = "Cat B";
    private const string MouseC = "Mouse C";

    static void Main(string[] args) 
    {
        TextWriter tw = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine());

        for (int qItr = 0; qItr < q; qItr++) {
            string[] xyz = Console.ReadLine().Split(' ');

            int x = Convert.ToInt32(xyz[0]);
            int y = Convert.ToInt32(xyz[1]);
            int z = Convert.ToInt32(xyz[2]);

            string result = CatAndMouse(x, y, z);
            tw.WriteLine(result);
        }

        tw.Flush();
        tw.Close();
    }
}
