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
                //Console.WriteLine($"row: {tree.Item1}, column: {tree.Item2}, tree: {tree.Item3}");

                var score = 1;
                var currentScore = 0;
                
                var treesInRow = lines[tree.Item1].Substring(tree.Item2 + 1, lines[tree.Item1].Length - tree.Item2 - 1);
                //Console.WriteLine($"- row trees: {treesInRow}");

                foreach (var nextTree in treesInRow)
                {
                    currentScore++;
                    if (Int32.Parse(nextTree.ToString()) >= tree.Item3)
                    {
                        //Console.WriteLine($"-- currentScore: {currentScore}");
                        break;
                    }
                }

                //Console.WriteLine($"trees right: {treesInRow}, currentScore: {currentScore}");
                score *= currentScore;

                currentScore = 0;

                treesInRow = new string(lines[tree.Item1].Substring(0, tree.Item2).Reverse().ToArray());
                //Console.WriteLine($"- row trees: {treesInRow}");

                foreach (var nextTree in treesInRow)
                {
                    currentScore++;
                    if (Int32.Parse(nextTree.ToString()) >= tree.Item3)
                    {
                        //Console.WriteLine($"-- currentScore: {currentScore}");
                        break;
                    }
                }

                //Console.WriteLine($"trees left: {treesInRow}, currentScore: {currentScore}");
                score *= currentScore;

                currentScore = 0;

                treesInRow = new string(lines.Skip(tree.Item1 + 1).Select(l => l[tree.Item2]).ToArray());
                //Console.WriteLine($"- row trees: {treesInRow}");

                foreach (var nextTree in treesInRow)
                {
                    currentScore++;
                    if (Int32.Parse(nextTree.ToString()) >= tree.Item3)
                    {
                        //Console.WriteLine($"-- currentScore: {currentScore}");
                        break;
                    }
                }

                //Console.WriteLine($"trees down: {treesInRow}, currentScore: {currentScore}");
                score *= currentScore;

                currentScore = 0;

                treesInRow = new string(lines.Take(tree.Item1).Select(l => l[tree.Item2]).Reverse().ToArray());
                //Console.WriteLine($"- row trees: {treesInRow}");

                foreach (var nextTree in treesInRow)
                {
                    currentScore++;
                    if (Int32.Parse(nextTree.ToString()) >= tree.Item3)
                    {
                        //Console.WriteLine($"-- currentScore: {currentScore}");
                        break;
                    }
                }

                //Console.WriteLine($"trees up: {treesInRow}, currentScore: {currentScore}");
                score *= currentScore;

                //Console.WriteLine($"score: {score}");
                if (score > maxScore)
                {
                    maxScore = score;
                }
            }

            part1 = tallestTrees.Distinct().Where(t => t.Item1 > 0 && t.Item1 < lines.Length - 1 && t.Item2 > 0 && t.Item2 < lines[0].Length - 1).Count() + width * 2 + height * 2 - 4;
            part2 = maxScore;

            Console.WriteLine($"part1: {part1}");
            Console.WriteLine($"part2: {part2}");
        }
    }
}