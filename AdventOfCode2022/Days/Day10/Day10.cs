using AdventOfCode2022.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace AdventOfCode2022.Days
{
    internal class Day10
    {
        static int year = 2022;
        static int day = 10;
        static string inputPath = $@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\Input.txt";
        //static string inputPath = $@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\SampleInput.txt";
        //static string inputPath = $@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\SampleInput2.txt";

        static string cookiePath = @"D:\Development\AdventOfCode\cookie.txt";

        static int part1 = 0;
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
            var newLines = lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
            var x = 1;
            var actions = new List<(int, int)>();
            var noops = newLines.Where(l => l == "noop").Count();
            var addx = newLines.Where(l => l != "noop" && l.Substring(0, l.IndexOf(" ")) == "addx").Count() * 2;
            var cycles = noops + addx;
            var cycleCounter = 20;
            var crtPixels = new List<string>();

            for (int i = 0; i < cycles; i++)
            {
                var line = i < newLines.Count ? newLines[i] : string.Empty;
                if (!string.IsNullOrEmpty(line) && line != "noop" && line.Substring(0, line.IndexOf(" ")) == "addx")
                {
                    var action = (i + 1, Int32.Parse(line.Substring(line.IndexOf(" "), line.Length - line.IndexOf(" "))));
                    actions.Add(action);
                    newLines.Insert(i + 1, String.Empty);
                    //Console.WriteLine($"action - current index: {i}, complete index: {action.Item1}, value: {action.Item2}");
                }

                // Draw
                if (string.IsNullOrEmpty(line))
                {

                }

                if (i == cycleCounter - 1)
                {
                    part1 += (i + 1) * x;
                    Console.WriteLine($"signal: {i + 1}, strength: {part1}");
                    cycleCounter += 40;
                }

                for (int j = 0; j < actions.Where(a => a.Item1 == i).Count(); j++)
                {
                    x += actions[j].Item2;
                    actions.Remove(actions[j]);
                }

                Console.WriteLine($"cycle: {i + 1}, x: {x}");
            }

            Console.WriteLine($"part1: {part1}");
            Console.WriteLine($"part2: {part2}");
        }
    }
}