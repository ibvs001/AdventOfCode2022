using AdventOfCode2022.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace AdventOfCode2022.Days
{
    internal class Day8
    {
        static int year = 2022;
        static int day = 8;
        //static string inputPath = $@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\Input.txt";
        static string inputPath = $@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\SampleInput.txt";
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

            var width = lines[0].Length;
            var height = lines.Length;
            var tallestTrees = new List<(int, int, int)>();

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                var tallestTreeInRow = (0, 0, 0);

                if (i == 0)
                {
                    for (int j = 0; j < line.Length; j++)
                    {
                        var tallestTreeInColumn = (0, 0, 0);

                        for (int k = 0; k < lines.Length; k++)
                        {
                            var tree = lines[k][j].ToString();
                            //Console.WriteLine($"row: {k}, column: {j}, tree: {tree}");

                            if (Int32.Parse(tree) > tallestTreeInColumn.Item3)
                            {
                                tallestTreeInColumn = (k, j, Int32.Parse(tree));
                                tallestTrees.Add(tallestTreeInColumn);
                                //Console.WriteLine($"row: {tallestTreeInColumn.Item1}, column: {tallestTreeInColumn.Item2}, tallestTreeInColumn: {tallestTreeInColumn.Item3}");
                            }
                        }
                    }

                    for (int j = 0; j < line.Length; j++)
                    {
                        var tallestTreeInColumn = (0, 0, 0);

                        for (int k = lines.Length - 1; k > 0; k--)
                        {
                            var tree = lines[k][j].ToString();
                            //Console.WriteLine($"row: {k}, column: {j}, tree: {tree}");

                            if (Int32.Parse(tree) > tallestTreeInColumn.Item3)
                            {
                                tallestTreeInColumn = (k, j, Int32.Parse(tree));
                                tallestTrees.Add(tallestTreeInColumn);
                                //Console.WriteLine($"row: {tallestTreeInColumn.Item1}, column: {tallestTreeInColumn.Item2}, tallestTreeInColumn: {tallestTreeInColumn.Item3}");
                            }
                        }
                    }
                }

                // Left
                for (int j = 0; j < line.Length; j++)
                {
                    var tree = line[j].ToString();

                    if (Int32.Parse(tree) > tallestTreeInRow.Item3)
                    {
                        tallestTreeInRow = (i, j, Int32.Parse(tree));
                        tallestTrees.Add(tallestTreeInRow);
                        //Console.WriteLine($"row: {tallestTreeInRow.Item1}, column: {tallestTreeInRow.Item2}, tallestTreeInRow: {tallestTreeInRow.Item3}");
                    }
                }

                tallestTreeInRow = (0, 0, 0);

                // Right
                for (int j = line.Length - 1; j > 0; j--)
                {
                    var tree = line[j].ToString();

                    if (Int32.Parse(tree) > tallestTreeInRow.Item3)
                    {
                        tallestTreeInRow = (i, j, Int32.Parse(tree));
                        tallestTrees.Add(tallestTreeInRow);
                        //Console.WriteLine($"row: {tallestTreeInRow.Item1}, column: {tallestTreeInRow.Item2}, tallestTreeInRow: {tallestTreeInRow.Item3}");
                    }
                }
            }

            var maxScore = 0;

            foreach (var tree in tallestTrees.Distinct())
            {
                var right = 0;

                for (int i = tree.Item1; i < lines[tree.Item1].Length - tree.Item1; i++)
                {
                    var nextTree = lines[tree.Item1][i].ToString();

                    if (Int32.Parse(nextTree) < tree.Item3)
                    {
                        right++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                right = right == 0 ? 1 : right;

                var left = 0;

                for (int i = tree.Item1; i > 0; i--)
                {
                    var nextTree = lines[tree.Item1][i].ToString();

                    if (Int32.Parse(nextTree) < tree.Item3)
                    {
                        left++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                left = left == 0 ? 1 : left;

                var up = 0;

                for (int i = tree.Item2; i < lines.Length - tree.Item1; i++)
                {
                    var nextTree = lines[i][tree.Item2].ToString();

                    if (Int32.Parse(nextTree) < tree.Item3)
                    {
                        up++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                up = up == 0 ? 1 : up;

                var down = 0;

                for (int i = tree.Item2; i > 0; i--)
                {
                    var nextTree = lines[i][tree.Item2].ToString();

                    if (Int32.Parse(nextTree) < tree.Item3)
                    {
                        down++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                down = down == 0 ? 1 : down;

                var score = left * right * up * down;

                Console.WriteLine($"row: {tree.Item1}, column: {tree.Item2}, tree height: {tree.Item3}, left: {left}, right: {right}, up: {up}, down: {down}");

                if (score > maxScore)
                {
                    maxScore = score;
                }
            }

            Console.WriteLine($"maxScore: {maxScore}");

            part1 = tallestTrees.Distinct().Where(t => t.Item1 > 0 && t.Item1 < lines.Length - 1 && t.Item2 > 0 && t.Item2 < lines[0].Length - 1).Count() + width * 2 + height * 2 - 4;

            Console.WriteLine($"part1: {part1}");
            Console.WriteLine($"part2: {part2}");
        }
    }
}