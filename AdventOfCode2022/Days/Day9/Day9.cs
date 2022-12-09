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
        //static string inputPath = $@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\SampleInput2.txt";

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

        private int GetTailPositions(string[] lines, int ropeLength)
        {
            var rope = new List<(int, int)>();
            var tailPositions = new List<(int, int)>();

            for (int i = 0; i < ropeLength; i++)
            {
                rope.Add((0, 0));
            }

            for (int i = 0; i < lines.Length; i++)
            {
                var direction = lines[i].Substring(0, lines[i].IndexOf(" "));
                var steps = Int32.Parse(lines[i].Substring(lines[i].IndexOf(" "), lines[i].Length - lines[i].IndexOf(" ")));

                Console.WriteLine($"=====Direction: {direction}=====");

                for (int j = 0; j < steps; j++)
                {
                    for (int k = 0; k < rope.Count; k++)
                    {
                        var currentKnot = rope[k];
                        //Console.WriteLine($"initial knot: {currentKnot.Item1};{currentKnot.Item2}");
                        if (k == 0)
                        {
                            var row = currentKnot.Item1 + (direction == "U" ? 1 : direction == "D" ? -1 : 0);
                            var col = currentKnot.Item2 + (direction == "R" ? 1 : direction == "L" ? -1 : 0);

                            currentKnot = (row, col);
                            Console.WriteLine($"head: {currentKnot.Item1};{currentKnot.Item2}");
                        }
                        else
                        {
                            var previousKnot = rope[k - 1];

                            // Up
                            if (previousKnot.Item1 - currentKnot.Item1 > 1)
                            {
                                //Console.WriteLine($"knot before: {currentKnot.Item1};{currentKnot.Item2}");
                                currentKnot = (currentKnot.Item1 + 1, currentKnot.Item2);

                                //Console.WriteLine($"knot mid: {currentKnot.Item1};{currentKnot.Item2}");

                                if (previousKnot.Item2 - currentKnot.Item2 > 1)
                                {
                                    currentKnot = (currentKnot.Item1, previousKnot.Item2 - 1);
                                }
                                else if (currentKnot.Item2 - previousKnot.Item2 > 1)
                                {
                                    currentKnot = (currentKnot.Item1, previousKnot.Item2 + 1);
                                }
                                else if (Math.Abs(previousKnot.Item2 - currentKnot.Item2) > 0)
                                {
                                    currentKnot = (currentKnot.Item1, previousKnot.Item2);
                                }

                                //Console.WriteLine($"knot after: {currentKnot.Item1};{currentKnot.Item2}");
                            }

                            // Down
                            else if (currentKnot.Item1 - previousKnot.Item1 > 1)
                            {
                                currentKnot = (currentKnot.Item1 - 1, currentKnot.Item2);

                                if (previousKnot.Item2 - currentKnot.Item2 > 1)
                                {
                                    currentKnot = (currentKnot.Item1, previousKnot.Item2 - 1);
                                }
                                else if (currentKnot.Item2 - previousKnot.Item2 > 1)
                                {
                                    currentKnot = (currentKnot.Item1, previousKnot.Item2 + 1);
                                }
                                else if (Math.Abs(previousKnot.Item2 - currentKnot.Item2) > 0)
                                {
                                    currentKnot = (currentKnot.Item1, previousKnot.Item2);
                                }
                            }

                            // Left
                            else if (currentKnot.Item2 - previousKnot.Item2 > 1)
                            {
                                currentKnot = (currentKnot.Item1, currentKnot.Item2 - 1);

                                if (previousKnot.Item1 - currentKnot.Item1 > 1)
                                {
                                    currentKnot = (previousKnot.Item1 + 1, currentKnot.Item2);
                                }
                                else if (currentKnot.Item1 - previousKnot.Item1 > 1)
                                {
                                    currentKnot = (previousKnot.Item1 - 1, currentKnot.Item2);
                                }
                                else if (Math.Abs(previousKnot.Item1 - currentKnot.Item1) > 0)
                                {
                                    currentKnot = (previousKnot.Item1, currentKnot.Item2);
                                }
                            }

                            // Right
                            else if (previousKnot.Item2 - currentKnot.Item2 > 1)
                            {
                                //Console.WriteLine($"knot before: {currentKnot.Item1};{currentKnot.Item2}");
                                currentKnot = (currentKnot.Item1, currentKnot.Item2 + 1);

                                if (previousKnot.Item1 - currentKnot.Item1 > 1)
                                {
                                    currentKnot = (previousKnot.Item1 + 1, currentKnot.Item2);
                                }
                                else if (currentKnot.Item1 - previousKnot.Item1 > 1)
                                {
                                    currentKnot = (previousKnot.Item1 - 1, currentKnot.Item2);
                                }
                                else if (Math.Abs(previousKnot.Item1 - currentKnot.Item1) > 0)
                                {
                                    currentKnot = (previousKnot.Item1, currentKnot.Item2);
                                }

                                //Console.WriteLine($"knot after: {currentKnot.Item1};{currentKnot.Item2}");
                            }

                            if (k == rope.Count - 1 && !tailPositions.Any(t => t.Item1 == currentKnot.Item1 && t.Item2 == currentKnot.Item2))
                            {
                                tailPositions.Add(currentKnot);

                                Console.WriteLine($"tailKnot: {currentKnot.Item1};{currentKnot.Item2}");
                            }
                        }

                        Console.WriteLine($"currentKnot: {currentKnot.Item1};{currentKnot.Item2}");
                        rope[k] = currentKnot;
                    }
                }
            }

            foreach (var item in tailPositions)
            {
                Console.WriteLine($"tailPosition: {item}");
            }

            return tailPositions.Count;
        }

        private void SolveProblem(string[] lines)
        {
            lines = lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

            //part1 = GetTailPositions(lines, 2);
            part2 = GetTailPositions(lines, 10);

            Console.WriteLine($"part1: {part1}");
            Console.WriteLine($"part2: {part2}");
        }
    }
}