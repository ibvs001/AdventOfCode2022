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
            var crtRowPixels = string.Empty;
            var pixelMultiplier = 0;

            /*
            1100110011001100110011001100110011001100
            1110001110001110001110001110001110001110
            1111000011110000111100001111000011110000
            1111100000111110000011111000001111100000
            1111110000001111110000001111110000001111

            ##..##..##..##..##..##..##..##..##..##..
            ###...###...###...###...###...###...###.
            ####....####....####....####....####....
            #####.....#####.....#####.....#####.....
            ######......######......######......####

            ##..##..##..##..##..##..##..##..##..##..
            ###...###...###...###...###...###...###.
            ####....####....####....####....####....
            #####.....#####.....#####.....#####.....
            ######......######......######......####
            #######.......#######.......#######.....
            */

            for (int i = 0; i <= cycles; i++)
            {
                var line = i < newLines.Count ? newLines[i] : string.Empty;
                if (!string.IsNullOrEmpty(line) && line != "noop" && line.Substring(0, line.IndexOf(" ")) == "addx")
                {
                    var action = (i + 1, Int32.Parse(line.Substring(line.IndexOf(" "), line.Length - line.IndexOf(" "))));
                    actions.Add(action);
                    newLines.Insert(i + 1, String.Empty);
                    //Console.WriteLine($"action - current index: {i}, complete index: {action.Item1}, value: {action.Item2}");
                }

                if (i >= 40 && i % 40 == 0)
                {
                    pixelMultiplier += 40;
                    //Console.WriteLine($"row: {crtRowPixels}");
                    crtPixels.Add(crtRowPixels);
                    crtRowPixels = string.Empty;
                }

                var pixelIndex = i - pixelMultiplier;// + (i == 39 ? 1 : 0);
                crtRowPixels += (x >= pixelIndex - 1 && x <= pixelIndex + 1 ? "#" : ".");

                if (i == cycleCounter - 1)
                {
                    part1 += (i + 1) * x;
                    //Console.WriteLine($"signal: {i + 1}, strength: {part1}");
                    cycleCounter += 40;
                }

                for (int j = 0; j < actions.Where(a => a.Item1 == i).Count(); j++)
                {
                    x += actions[j].Item2;
                    actions.Remove(actions[j]);
                }

                Console.WriteLine($"cycle: {i + 1}, x: {x}");
            }

            foreach (var row in crtPixels)
            {
                Console.WriteLine($"row: {row}");
            }

            Console.WriteLine($"part1: {part1}");
            Console.WriteLine($"part2: {part2}");
        }
    }
}