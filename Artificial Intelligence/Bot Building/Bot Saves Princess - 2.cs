using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication9
{
    class ConsoleApp
    {
        private static Dictionary<char, Location> _actors;
        private static char[,] _grid;

        static void Main(String[] args)
        {
            var n = int.Parse(Console.ReadLine());

            _actors = new Dictionary<char, Location>();
            _grid = new char[n, n];
            
            var inputRaw = Console.ReadLine();
            var y = int.Parse(inputRaw.Split(' ').First());
            var x = int.Parse(inputRaw.Split(' ').Last());
            
            _actors['m'] = new Location(x,y);

            PopulateGridAndActors(n, n);
            
            var nextAction = GetMovementAction(_actors['m'], _actors['p']);
            UpdateActor(_actors['m'], nextAction);
            Console.WriteLine(nextAction.ToString().ToUpper());
            
        }

        static void PopulateGridAndActors(int gridXSize, int gridYSize)
        {
            for (var y = 0; y < gridXSize; y++)
            {
                var input = Console.ReadLine();

                for (var x = 0; x < gridYSize; x++)
                {
                    _grid[x, y] = input[x];
                    switch (_grid[x, y])
                    {
                        case 'p':
                            _actors['p'] = new Location(x, y);
                            break;
                    }
                }
            }
        }

        static bool SameLocation(Location loc1, Location loc2)
        {
            return loc1.X == loc2.X &&
                   loc1.Y == loc2.Y;
        }

        static Action GetMovementAction(Location source, Location target)
        {
            var yDiff = source.Y - target.Y;
            var xDiff = source.X - target.X;

            if (yDiff > 0) return Action.Up;
            if (yDiff < 0) return Action.Down;
            if (xDiff > 0) return Action.Left;
            if (xDiff < 0) return Action.Right;
            return Action.None;
        }

        static void UpdateActor(Location movingItem, Action action)
        {
            if (action == Action.None) return;
            if (action == Action.Up) movingItem.Y--;
            if (action == Action.Down) movingItem.Y++;
            if (action == Action.Left) movingItem.X--;
            if (action == Action.Right) movingItem.X++;
        }
    }

    public enum Action
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    public class Location
    {
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
