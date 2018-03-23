using System;
using System.Collections.Generic;
using System.IO;

class Solution
{
    public static void next_move(int posr, int posc, String[] board)
    {
        // Check if current cell is dirty to save time splitting up the board
        if (board[posr][posc] == DirtyCell)
        {
            Console.WriteLine(Action.Clean.ToString().ToUpper());
            return;
        }

        var botLocation = new Location() { Row = posr, Column = posc };

        var nearestDirtyCellLocation = GetNearestDirtyCell(board, botLocation);

        var action = GetMovementAction(botLocation, nearestDirtyCellLocation);

        Console.WriteLine(action.ToString().ToUpper());
    }

    private static Location GetNearestDirtyCell(string[] inputBoard, Location botLocation)
    {
        var board = SplitBoard(inputBoard);
        var rows = board.GetLength(0);
        var columns = board.GetLength(1);
        var maxRadius = rows > columns ? rows : columns;

        for (var radias = 1; radias <= maxRadius; radias++)
        {
            for (var angle = 0; angle < 360; angle++)
            {
                var l = (angle * Math.PI / 180);
                var rowOffset = Math.Round(radias * Math.Cos(l));
                var colOffset = Math.Round(radias * Math.Sin(l));
                var checkRow = (int)(botLocation.Row + rowOffset);
                var checkCol = (int)(botLocation.Column + colOffset);

                if (checkRow < 0 ||
                    checkRow >= rows ||
                    checkCol < 0 ||
                    checkCol >= columns)
                {
                    continue;
                }

                if (board[checkRow, checkCol] == DirtyCell)
                {
                    return new Location()
                    {
                        Row = checkRow,
                        Column = checkCol
                    };
                }
            }
        }

        throw new Exception("Cannot find a dirty cell!");
    }

    private static Action GetMovementAction(Location source, Location target)
    {
        var rowDiff = source.Row - target.Row;
        var colDiff = source.Column - target.Column;

        if (rowDiff > 0) return Action.Up;
        if (rowDiff < 0) return Action.Down;
        if (colDiff > 0) return Action.Left;
        if (colDiff < 0) return Action.Right;
        return Action.None;
    }

    private static char[,] SplitBoard(String[] board)
    {
        var rows = board.Length;
        var columns = board[0].Length;

        var newBoard = new char[rows, columns];
        for (var x = 0; x < rows; x++)
        {
            for (var y = 0; y < columns; y++)
            {
                newBoard[x, y] = board[x][y];
            }
        }
        return newBoard;
    }

    private const char DirtyCell = 'd';
    private const char EmptyCell = '-';
    private const char BotCell = 'b';

    static void Main(String[] args)
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

public enum Action
{
    None,
    Left,
    Right,
    Up,
    Down,
    Clean
}

public class Location
{
    public int Row { get; set; }
    public int Column { get; set; }
}
