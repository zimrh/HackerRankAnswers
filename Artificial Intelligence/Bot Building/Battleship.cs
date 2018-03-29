using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution
{
    public class Solution
    {
        public static void Main(string[] args)
        {
            var state = Console.ReadLine();
            if (string.Equals(state, Helper.Init))
            {
                foreach (var ship in GetRandomShipLayouts())
                {
                    Console.WriteLine(ship.GetPosition());
                }
                return;
            }

            var gridSize = int.Parse(state ?? "0");
            // Might as well check since we are getting this value
            if (gridSize != Helper.GridSize)
            {
                throw new Exception($"{nameof(gridSize)} != {Helper.GridSize}!");
            }

            var grid = ReadGrid(gridSize);
            Console.WriteLine(GetNextTarget(grid, gridSize));
        }

        private static Location GetNextTarget(char[,] grid, int size)
        {
            var rand = new Random();

            // Scan Grid
            for (var r = 0; r < size; r++)
            {
                for (var c = 0; c < size; c++)
                {
                    // LOTS OF IF STATEMENTS AHEAD!!! A SEA OF IFS!!!
                    // Version 1 of this attempt, not pretty!

                    // Not a hit?, move on, will come back to this later
                    if (grid[r, c] != Helper.Hit) { continue; }

                    // All Cells around this hit have been attacked? 
                    // This is not the hit you are looking for, move along!
                    if (grid.GetCell(r, c - 1) != Helper.Empty &&
                        grid.GetCell(r, c + 1) != Helper.Empty &&
                        grid.GetCell(r - 1, c) != Helper.Empty &&
                        grid.GetCell(r + 1, c) != Helper.Empty)
                    {
                        continue;
                    }

                    if (grid.GetCell(r - 1, c) == Helper.Hit ||
                        grid.GetCell(r + 1, c) == Helper.Hit)
                    {
                        for (var row = r - 1; row >= 0; row--)
                        {
                            if (grid.GetCell(row, c) == Helper.Miss ||
                                grid.GetCell(row, c) == Helper.Destroyed)
                            { break; }

                            if (grid.GetCell(row, c) == Helper.Empty)
                            {
                                return new Location(row, c);
                            }
                        }

                        for (var row = r + 1; row < size; row++)
                        {
                            if (grid.GetCell(row, c) == Helper.Miss ||
                                grid.GetCell(row, c) == Helper.Destroyed)
                            { break; }

                            if (grid.GetCell(row, c) == Helper.Empty)
                            {
                                return new Location(row, c);
                            }
                        }
                    }


                    if (grid.GetCell(r, c - 1) == Helper.Hit ||
                        grid.GetCell(r, c + 1) == Helper.Hit)
                    {
                        for (var col = c - 1; col >= 0; col--)
                        {
                            if (grid.GetCell(r, col) == Helper.Miss ||
                                grid.GetCell(r, col) == Helper.Destroyed)
                            { break; }

                            if (grid.GetCell(r, col) == Helper.Empty)
                            {
                                return new Location(r, col);
                            }
                        }

                        for (var col = c + 1; col < size; col++)
                        {
                            if (grid.GetCell(r, col) == Helper.Miss ||
                                grid.GetCell(r, col) == Helper.Destroyed)
                            { break; }

                            if (grid.GetCell(r, col) == Helper.Empty)
                            {
                                return new Location(r, col);
                            }
                        }
                    }

                    do
                    {
                        // ok, lets pick a random empty cell nearby!
                        var randomDirection = rand.Next(4);
                        switch (randomDirection)
                        {
                            case 0:
                                if (grid.GetCell(r - 1, c) == Helper.Empty) { return new Location(r - 1, c); }
                                break;

                            case 1:
                                if (grid.GetCell(r + 1, c) == Helper.Empty) { return new Location(r + 1, c); }
                                break;

                            case 2:
                                if (grid.GetCell(r, c - 1) == Helper.Empty) { return new Location(r, c - 1); }
                                break;

                            default:
                                if (grid.GetCell(r, c + 1) == Helper.Empty) { return new Location(r, c + 1); }
                                break;

                        }
                    } while (true);
                }
            }

            // Checked entire grid, no existing hits found, pick a random spot and FIRE!
            // Interesting: If the smallest ship was Length 2 we could checker our random hits to reduce the number of hits
            // we need to do (i.e. like a chess board) but the sub is Length 1 so random it is!

            do
            {
                var targetRow = rand.Next(size);
                var targetCol = rand.Next(size);
                if (grid[targetRow, targetCol] == Helper.Empty)
                {
                    return new Location(targetRow, targetCol);
                }
            } while (true);
        }


        private static char[,] ReadGrid(int size)
        {
            var grid = new char[size, size];
            for (var row = 0; row < size; row++)
            {
                var input = Console.ReadLine();
                for (var col = 0; col < size; col++)
                {
                    grid[row, col] = input[col];
                }
            }
            return grid;
        }

        #region PlacementCode

        private static IEnumerable<Ship> GetRandomShipLayouts()
        {
            var grid = Helper.GenerateGrid(Helper.GridSize);
            var ships = Helper.Ships;

            // Place larger ships first (save on collisions when placing)
            foreach (var ship in ships.OrderByDescending(s => s.Length))
            {
                do
                {
                    var randGen = new Random();
                    var startPos = new Location()
                    {
                        Row = randGen.Next(Helper.GridSize),
                        Column = randGen.Next(Helper.GridSize)
                    };

                    var direction = randGen.Next(2);

                    // 0 = right or 1 = down
                    var endPos = new Location()
                    {
                        Row = (direction == 1) ? startPos.Row : startPos.Row + ship.Length - 1,
                        Column = (direction == 1) ? startPos.Column + ship.Length - 1 : startPos.Column
                    };

                    if (!IsShipGoingToFit(grid, startPos, endPos)) { continue; }

                    AddShipToGrid(grid, startPos, endPos);
                    ship.StartPosition = startPos;
                    ship.EndPosition = endPos;

                } while (!ship.IsShipInPlace());
            }

            return ships.OrderBy(s => s.Length);
        }

        private static void AddShipToGrid(char[,] grid, Location startPos, Location endPos)
        {
            for (var c = startPos.Column; c <= endPos.Column; c++)
            {
                for (var r = startPos.Row; r <= endPos.Row; r++)
                {
                    grid[r, c] = Helper.Ship;
                }
            }
        }

        private static bool IsShipGoingToFit(char[,] grid, Location startPos, Location endPos)
        {
            if (endPos.Column >= Helper.GridSize || endPos.Row >= Helper.GridSize)
            {
                return false;
            }
            for (var c = startPos.Column; c <= endPos.Column; c++)
            {
                for (var r = startPos.Row; r <= endPos.Column; r++)
                {
                    if (grid[r, c] == Helper.Ship) 
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
    }

    public static class Helper
    {
        public const int GridSize = 10;
        public const string Init = "INIT";
        public const char Ship = 's';
        public const char Empty = '-';
        public const char Miss = 'm';
        public const char Hit = 'h';
        public const char Destroyed = 'd';
        public static readonly List<Ship> Ships = new List<Ship>()
        {
            new Ship { Length = 5 },
            new Ship { Length = 4 },
            new Ship { Length = 3 },
            new Ship { Length = 2 },
            new Ship { Length = 2 },
            new Ship { Length = 1 },
            new Ship { Length = 1 }
        };
        public static char[,] GenerateGrid(int size)
        {
            return new char[size, size];
        }
    }

    public static class GridExtensions
    {
        public static char GetCell(this char[,] grid, int row, int col)
        {
            var rows = grid.GetLength(0);
            var cols = grid.GetLength(1);
            // Out of bounds, assume it is a miss
            if (row < 0 || row >= rows || col < 0 || col >= cols)
            {
                return Helper.Miss;
            }
            return grid[row, col];
        }
    }

    public class Location
    {
        public Location() { }
        public Location(int row, int col)
        {
            Row = row;
            Column = col;
        }

        public int Row { get; set; } = -1;
        public int Column { get; set; } = -1;
        public bool HasLocationBeenSet => Row != -1 && Column != -1;
        public override string ToString()
        {
            return $"{Row} {Column}";
        }
    }

    public class Ship
    {
        public int Length { get; set; }
        public Location StartPosition { get; set; } = new Location();
        public Location EndPosition { get; set; } = new Location();
        public bool IsShipInPlace() => StartPosition.HasLocationBeenSet && EndPosition.HasLocationBeenSet;
        public string GetPosition()
        {
            return (StartPosition.Row == EndPosition.Row &&
                    StartPosition.Column == EndPosition.Column)
                ? $"{StartPosition.Row} {StartPosition.Column}"
                : $"{StartPosition.Row} {StartPosition.Column}:{EndPosition.Row} {EndPosition.Column}";
        }
    }
}
