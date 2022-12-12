
using AdventOfCode2022.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Xml.Linq;

namespace AdventOfCode2022.Days
{
    internal class Day12
    {
        static int year = 2022;
        static int day = 12;
        static string inputPath = $@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\Input.txt";
        //static string inputPath = $@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\SampleInput.txt";

        static string cookiePath = @"D:\Development\AdventOfCode\cookie.txt";

        static int part1 = 0;
        static int part2 = 0;

        public void Run(int submitPartNumber = -1)
        {
            //var cookie = File.ReadAllText(cookiePath);
            //InputHelper.GetInput(inputPath, year, day, cookie);
            string[] lines = File.ReadAllLines(inputPath);

            SolveProblem(lines);

            try
            {
                //if (submitPartNumber == 1)
                //{
                //    InputHelper.SubmitAnswer(part1, year, day, cookie);
                //}

                //if (submitPartNumber == 2)
                //{
                //    InputHelper.SubmitAnswer(part2, year, day, cookie);
                //}
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to submit!");
                throw;
            }
        }

        private void SolveProblem(string[] lines)
        {
            var map = lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
            GetPath(map, 'S', 'E', (-1, -1));

            for (int i = 0; i < map.Count; i++)
            {
                var occ = map[i].Select((b, i) => b.Equals('a') ? i : -1).Where(i => i != -1).ToList();

                foreach (var oc in occ)
                {
                    GetPath(map, 'E', 'a', (oc, i));
                }
            }
        }

        private void GetPath(List<string> map, char startValue, char endValue, (int, int) endPosition)
        {
            var start = new Tile();
            start.Y = map.FindIndex(x => x.Contains(startValue));
            start.X = map[start.Y].IndexOf(startValue);

            var finish = new Tile();
            finish.Y = map.FindIndex(x => x.Contains(endValue));
            finish.X = map[finish.Y].IndexOf(endValue);

            if (endPosition.Item1 != -1)
            { 
                finish.Y = endPosition.Item1;
                finish.X = endPosition.Item2;
            }

            start.SetDistance(finish.X, finish.Y);

            var activeTiles = new List<Tile>();
            activeTiles.Add(start);
            var visitedTiles = new List<Tile>();

            var totalSteps = 0;

            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                {
                    var tile = checkTile;
                    while (true)
                    {
                        totalSteps++;
                        tile = tile.Parent;

                        if (tile == null)
                        {
                            Console.WriteLine($"part{(endPosition.Item1 == -1 ? 1 : 2)}: {totalSteps - 1}");
                            return;
                        }
                    }
                }

                //if (endPosition.Item1 != -1)
                //    Console.WriteLine($"visitedTile - {checkTile.X} : {checkTile.Y} - {map[checkTile.Y][checkTile.X]}");

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);

                var walkableTiles = GetWalkableTiles(map, checkTile, finish, startValue, endValue);

                foreach (var walkableTile in walkableTiles)
                {
                    if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                        continue;

                    if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    {
                        var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                        if (existingTile.CostDistance > checkTile.CostDistance)
                        {
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(walkableTile);
                        }
                    }
                    else
                    {
                        activeTiles.Add(walkableTile);
                    }
                }
            }

            Console.WriteLine("No Path Found!");
        }

        private static List<Tile> GetWalkableTiles(List<string> map, Tile currentTile, Tile targetTile, char startValue, char endValue)
        {
            var possibleTiles = new List<Tile>()
            {
                new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
            };

            possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

            var currentValue = map[currentTile.Y][currentTile.X];
            var currentHeight = GetHeight(currentValue);
            var maxX = map.First().Length - 1;
            var maxY = map.Count - 1;

            return possibleTiles
                    .Where(tile => tile.X >= 0 && tile.X <= maxX)
                    .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
                    .Where(tile => GetCanPass(currentHeight, GetHeight(map[tile.Y][tile.X])) || GetCanPass(currentHeight, GetHeight(map[tile.Y][tile.X])) && map[tile.Y][tile.X] == endValue)
                    .ToList();
        }

        private static int GetHeight(char currentValue)
        {
            return currentValue == 'S'
                ? HeightValues.a.GetHashCode()
                : currentValue == 'E'
                    ? HeightValues.z.GetHashCode()
                    : Enum.Parse(typeof(HeightValues), currentValue.ToString()).GetHashCode();
        }

        private static bool GetCanPass(int current, int destination)
        {
            return destination >= current && Math.Abs(current - destination) <= 1 || destination < current;
        }

        class Tile
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Cost { get; set; }
            public int Distance { get; set; }
            public int CostDistance => Cost + Distance;
            public Tile Parent { get; set; }
            
            public void SetDistance(int targetX, int targetY)
            {
                this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
            }
        }
    }
}