using AdventOfCode2022.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace AdventOfCode2022.Days
{
    internal class Day9
    {
        static int year = 2022;
        static int day = 9;
        static string inputPath = $@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\Input.txt";
        //static string inputPath = $@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\SampleInput.txt";
        static string cookiePath = @"D:\Development\AdventOfCode\cookie.txt";

        static int part1 = -1;
        static int part2 = -1;

        public void Run(int submitPartNumber = -1)
        {
            var cookie = File.ReadAllText(cookiePath);
            InputHelper.GetInput(inputPath, year, day, cookie);
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
            lines = lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
            var currentHead = (0, 0);
            var currentTail = (0, 0);
            var visitedPositions = new List<(int, int)>();
            var visitedPositionsLong = new List<(int, int)>();
            var rope = new List<(int, int)>();

            for (int i = 0; i < 9; i++)
            {
                rope.Add((0, 0));
            }

            for (int i = 0; i < lines.Length; i++)
            {
                var direction = lines[i].Substring(0, lines[i].IndexOf(" "));
                var steps = Int32.Parse(lines[i].Substring(lines[i].IndexOf(" "), lines[i].Length - lines[i].IndexOf(" ")));

                for (int j = 0; j < steps; j++)
                {
                    if (direction == "U")
                    {
                        currentHead = (currentHead.Item1 + 1, currentHead.Item2);

                        if (Math.Abs(currentHead.Item1 - currentTail.Item1) > 1)
                        {
                            currentTail = (currentHead.Item1 - 1, currentTail.Item2);

                            if (Math.Abs(currentHead.Item2 - currentTail.Item2) > 0)
                            {
                                currentTail = (currentTail.Item1, currentHead.Item2);
                            }

                            if (!visitedPositions.Any(p => p.Item1 == currentTail.Item1 && p.Item2 == currentTail.Item2))
                            {
                                visitedPositions.Add(currentTail);
                            }
                        }

                        var previousKnot = currentHead;

                        for (int k = 0; k < rope.Count; k++)
                        {
                            var knot = rope[k];
                            if (Math.Abs(previousKnot.Item1 - knot.Item1) > 1)
                            {
                                knot = (previousKnot.Item1 - 1, knot.Item2);

                                if (Math.Abs(previousKnot.Item2 - knot.Item2) > 0)
                                {
                                    knot = (knot.Item1, previousKnot.Item2);
                                }

                                if (k == rope.Count - 1 && !visitedPositionsLong.Any(p => p.Item1 == knot.Item1 && p.Item2 == knot.Item2))
                                {
                                    visitedPositionsLong.Add(knot);
                                }
                            }

                            previousKnot = knot;
                        }
                    }
                    else if (direction == "D")
                    {
                        currentHead = (currentHead.Item1 - 1, currentHead.Item2);

                        if (Math.Abs(currentHead.Item1 - currentTail.Item1) > 1)
                        {
                            currentTail = (currentHead.Item1 + 1, currentTail.Item2);

                            if (Math.Abs(currentHead.Item2 - currentTail.Item2) > 0)
                            {
                                currentTail = (currentTail.Item1, currentHead.Item2);
                            }

                            if (!visitedPositions.Any(p => p.Item1 == currentTail.Item1 && p.Item2 == currentTail.Item2))
                            {
                                visitedPositions.Add(currentTail);
                            }
                        }

                        var previousKnot = currentHead;

                        for (int k = 0; k < rope.Count; k++)
                        {
                            var knot = rope[k];

                            if (Math.Abs(previousKnot.Item1 - knot.Item1) > 1)
                            {
                                knot = (previousKnot.Item1 + 1, knot.Item2);

                                if (Math.Abs(previousKnot.Item2 - knot.Item2) > 0)
                                {
                                    knot = (knot.Item1, previousKnot.Item2);
                                }

                                if (k == rope.Count - 1 && !visitedPositionsLong.Any(p => p.Item1 == knot.Item1 && p.Item2 == knot.Item2))
                                {
                                    visitedPositionsLong.Add(knot);
                                }
                            }

                            previousKnot = knot;
                        }
                    }
                    else if (direction == "L")
                    {
                        currentHead = (currentHead.Item1, currentHead.Item2 - 1);

                        if (Math.Abs(currentHead.Item2 - currentTail.Item2) > 1)
                        {
                            currentTail = (currentTail.Item1, currentHead.Item2 + 1);

                            if (Math.Abs(currentHead.Item1 - currentTail.Item1) > 0)
                            {
                                currentTail = (currentHead.Item1, currentTail.Item2);
                            }

                            if (!visitedPositions.Any(p => p.Item1 == currentTail.Item1 && p.Item2 == currentTail.Item2))
                            {
                                visitedPositions.Add(currentTail);
                            }
                        }

                        var previousKnot = currentHead;

                        for (int k = 0; k < rope.Count; k++)
                        {
                            var knot = rope[k];

                            if (Math.Abs(previousKnot.Item2 - knot.Item2) > 1)
                            {
                                knot = (knot.Item1, previousKnot.Item2 + 1);

                                if (Math.Abs(previousKnot.Item1 - knot.Item1) > 0)
                                {
                                    knot = (previousKnot.Item1, knot.Item2);
                                }

                                if (k == rope.Count - 1 && !visitedPositionsLong.Any(p => p.Item1 == knot.Item1 && p.Item2 == knot.Item2))
                                {
                                    visitedPositionsLong.Add(knot);
                                }
                            }

                            previousKnot = knot;
                        }
                    }
                    else if (direction == "R")
                    {
                        currentHead = (currentHead.Item1, currentHead.Item2 + 1);

                        if (Math.Abs(currentHead.Item2 - currentTail.Item2) > 1)
                        {
                            currentTail = (currentTail.Item1, currentHead.Item2 - 1);

                            if (Math.Abs(currentHead.Item1 - currentTail.Item1) > 0)
                            {
                                currentTail = (currentHead.Item1, currentTail.Item2);
                            }

                            if (!visitedPositions.Any(p => p.Item1 == currentTail.Item1 && p.Item2 == currentTail.Item2))
                            {
                                visitedPositions.Add(currentTail);
                            }
                        }

                        var previousKnot = currentHead;

                        for (int k = 0; k < rope.Count; k++)
                        {
                            var knot = rope[k];

                            if (Math.Abs(previousKnot.Item2 - knot.Item2) > 1)
                            {
                                knot = (knot.Item1, previousKnot.Item2 - 1);

                                if (Math.Abs(previousKnot.Item1 - knot.Item1) > 0)
                                {
                                    knot = (previousKnot.Item1, knot.Item2);
                                }

                                if (k == rope.Count - 1 && !visitedPositionsLong.Any(p => p.Item1 == knot.Item1 && p.Item2 == knot.Item2))
                                {
                                    visitedPositionsLong.Add(knot);
                                }
                            }

                            previousKnot = knot;
                        }
                    }

                    //Console.WriteLine($"currentHead row: {currentHead.Item1}," +
                    //    $"currentHead col: {currentHead.Item2}, " +
                    //    $"currentTail row: {currentTail.Item1}, " +
                    //    $"currentTail col: {currentTail.Item2}, " +
                    //    $"step: {j}, " +
                    //    $"positionsVisited: {visitedPositions.Count}");
                }

                Console.WriteLine($"direction: {direction}, " +
                    $"steps: {steps}, " +
                    $"currentHead row: {currentHead.Item1}, " +
                    $"currentHead col: {currentHead.Item2}, " +
                    $"currentTail row: {currentTail.Item1}, " +
                    $"currentTail col: {currentTail.Item2}, " +
                    $"visitedPositions: {visitedPositions.Count}, " +
                    $"visitedPositionsLong: {visitedPositionsLong.Count}");
            }

            part1 = visitedPositions.Count + 1;
            part2 = visitedPositionsLong.Count + 1;

            Console.WriteLine($"part1: {part1}");
            Console.WriteLine($"part2: {part2}");
        }
    }
}