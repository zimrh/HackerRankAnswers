using System;
using System.Collections.Generic;
using System.IO;

class Solution
{
    public static void next_move(int posr, int posc, String[] board)
    {
        // Check if current cell is dirty to save time splitting up the board
        if (board[posr][posc] == TextHelper.DirtyCell)
        {
            Console.WriteLine(TextHelper.Clean);
            return;
        }

        var botLocation = new Location() { Row = posr, Column = posc };
        var nearestDirtyCellLocation = GetNearestDirtyCell(board, botLocation);

        Console.WriteLine(GetMovementAction(botLocation, nearestDirtyCellLocation));
    }

    private static Location GetNearestDirtyCell(string[] inputBoard, Location botLocation)
    {
        var board = inputBoard;
        var rows = board.Length;
        var columns = board[0].Length;
        var closestLocation = new Location();
        double shortestDistance = rows * columns;

        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < columns; c++)
            {
                if (board[r][c] != TextHelper.DirtyCell)
                {
                    continue;
                }
                var distance = Math.Sqrt((Math.Pow(botLocation.Column - c, 2) + Math.Pow(botLocation.Row - r, 2)));
                if (distance >= shortestDistance)
                {
                    continue;
                }
                shortestDistance = distance;
                closestLocation.Row = r;
                closestLocation.Column = c;
            }
        }

        return closestLocation;
    }

    private static string GetMovementAction(Location source, Location target)
    {
        var rowDiff = source.Row - target.Row;
        var colDiff = source.Column - target.Column;
        if (rowDiff > 0) return TextHelper.Up;
        if (rowDiff < 0) return TextHelper.Down;
        if (colDiff > 0) return TextHelper.Left;
        if (colDiff < 0) return TextHelper.Right;
        return TextHelper.None;
    }
    
    static void Maiwn(String[] args)
    {
        String temp = Console.ReadLine();
        String[] position = temp.Split(' ');
        int[] pos = new int[2];
        String[] board = new String[5];
        for (int i = 0; i < 5; i++)
        {
            board[i] = Console.ReadLine();
        }
        for (int i = 0; i < 2; i++) pos[i] = Convert.ToInt32(position[i]);
        next_move(pos[0], pos[1], board);
    }
}

public static class TextHelper
{
    public const string Left = "LEFT";
    public const string Right = "RIGHT";
    public const string Up = "UP";
    public const string Down = "DOWN";
    public const string None = "NONE";
    public const string Clean = "CLEAN";
    public const char DirtyCell = 'd';
    public const char EmptyCell = '-';
    public const char BotCell = 'b';
}

public class Location
{
    public int Row { get; set; }
    public int Column { get; set; }
}
