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

                    // Two Hits? Hit the opposite one! may save time if we destroy whatever is there!
                    // If we destroy it we do not need to come back to this hit on the next iteration
                    // I am sure this can be made cleaner btw...
                    if (grid.GetCell(r, c - 1) == Helper.Hit &&
                        grid.GetCell(r, c + 1) == Helper.Empty)
                    {
                        return new Location(r, c + 1);
                    }

                    if (grid.GetCell(r, c + 1) == Helper.Hit &&
                        grid.GetCell(r, c - 1) == Helper.Empty)
                    {
                        return new Location(r, c - 1);
                    }

                    if (grid.GetCell(r + 1, c) == Helper.Hit &&
                        grid.GetCell(r - 1, c) == Helper.Empty)
                    {
                        return new Location(r - 1, c);
                    }

                    if (grid.GetCell(r - 1, c) == Helper.Hit &&
                        grid.GetCell(r + 1, c) == Helper.Empty)
                    {
                        return new Location(r + 1, c);
                    }

                    // ok, wack the empty cells just in case!
                    if (grid.GetCell(r - 1, c) == Helper.Empty) { return new Location(r - 1, c); }
                    if (grid.GetCell(r + 1, c) == Helper.Empty) { return new Location(r + 1, c); }
                    if (grid.GetCell(r, c - 1) == Helper.Empty) { return new Location(r, c - 1); }
                    if (grid.GetCell(r, c + 1) == Helper.Empty) { return new Location(r, c + 1); }
                    
                }
            }

            // Checked entire grid, no existing hits found, pick a random spot and FIRE!
            // Interesting: If the smallest ship was Length 2 we could checker our random hits to reduce the number of hits
            // we need to do (i.e. like a chess board) but the sub is Length 1 so random it is!
            var rand = new Random();
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
                    var rp1 = randGen.Next(Helper.GridSize);
                    var cp1 = randGen.Next(Helper.GridSize);
                    var direction = randGen.Next(2);

                    // 0 = right, 1 = down
                    var rp2 = (direction == 1) ? rp1 : rp1 + ship.Length - 1;
                    var cp2 = (direction == 1) ? cp1 + ship.Length - 1 : cp1;

                    if (!IsShipGoingToFit(grid, cp1, rp1, cp2, rp2)) { continue; }

                    AddShipToGrid(grid, cp1, rp1, cp2, rp2);
                    ship.Positions = new[] { cp1, rp1, cp2, rp2 };

                } while (!ship.IsShipInPlace());
            }

            return ships.OrderBy(s => s.Length);
        }

        private static void AddShipToGrid(char[,] grid, int cp1, int rp1, int cp2, int rp2)
        {
            for (var c = cp1; c <= cp2; c++)
            {
                for (var r = rp1; r <= rp2; r++)
                {
                    grid[r, c] = Helper.Ship;
                }
            }
        }

        private static bool IsShipGoingToFit(char[,] grid, int cp1, int rp1, int cp2, int rp2)
        {
            if (cp2 >= Helper.GridSize || rp2 >= Helper.GridSize)
            {
                return false;
            }
            for (var c = cp1; c <= cp2; c++)
            {
                for (var r = rp1; r <= rp2; r++)
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
        public int Row { get; set; }
        public int Column { get; set; }
        public override string ToString()
        {
            return $"{Row} {Column}";
        }
    }

    public class Ship
    {
        public int Length { get; set; }
        public int[] Positions { get; set; } = { -1, -1, -1, -1 };
        public bool IsShipInPlace() => Positions.All(p => p != -1);

        public string GetPosition()
        {
            return (Positions[0] == Positions[2] && Positions[1] == Positions[3])
                ? $"{Positions[0]} {Positions[1]}"
                : $"{Positions[0]} {Positions[1]}:{Positions[2]} {Positions[3]}";
        }
    }
}
