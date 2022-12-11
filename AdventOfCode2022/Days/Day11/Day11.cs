using AdventOfCode2022.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Xml.Linq;

namespace AdventOfCode2022.Days
{
    internal class Day11
    {
        static int year = 2022;
        static int day = 11;
        //static string inputPath = $@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\Input.txt";
        static string inputPath = $@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\SampleInput.txt";
        //static string inputPath = $@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\SampleInput2.txt";

        static string cookiePath = @"D:\Development\AdventOfCode\cookie.txt";

        static int part1 = 0;
        static int part2 = -1;

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
            lines = lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

            //part1 = RunRounds(20, lines, 3);
            part2 = RunRounds(1000, lines, 1);

            Console.WriteLine($"part1: {part1}");
            Console.WriteLine($"part2: {part2}");
        }

        private int RunRounds(int rounds, string[] lines, decimal worryLevelDivider)
        {
            var monkeys = new List<List<string>>();

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                if (line.StartsWith("Monkey"))
                {
                    monkeys.Add(new List<string>());
                    var monkey = line.Substring(line.IndexOf(" ") + 1, 1).Trim();
                    monkeys.Last().Add(monkey);
                    //Console.WriteLine($"add monkey: {monkey}");
                }
                else if (line.Trim().StartsWith("Starting"))
                {
                    var step = line.Substring(line.IndexOf(":") + 1, line.Length - line.IndexOf(":") - 1).Trim();
                    monkeys.Last().Add(step);
                    //Console.WriteLine($"starting items: {step}");
                }
                else if (line.Trim().StartsWith("Operation"))
                {
                    var step = line.Substring(line.IndexOf("=") + 1, line.Length - line.IndexOf("=") - 1).Trim();
                    monkeys.Last().Add(step);
                    //Console.WriteLine($"operation: {step}");
                }
                else if (line.Trim().StartsWith("Test"))
                {
                    var step = line.Substring(line.LastIndexOf(" "), line.Length - line.LastIndexOf(" ")).Trim();
                    monkeys.Last().Add(step);
                    //Console.WriteLine($"test: {step}");
                }
                else if (line.Trim().Contains("true"))
                {
                    var step = line.Substring(line.LastIndexOf(" "), line.Length - line.LastIndexOf(" ")).Trim();
                    monkeys.Last().Add(step);
                    //Console.WriteLine($"true: {step}");
                }
                else if (line.Trim().Contains("false"))
                {
                    var step = line.Substring(line.LastIndexOf(" "), line.Length - line.LastIndexOf(" ")).Trim();
                    monkeys.Last().Add(step);
                    //Console.WriteLine($"false: {step}");

                    // Add inspection indicator
                    monkeys.Last().Add("0");
                }
            }

            var sw = new StreamWriter($@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\Part2Output.txt");
            var increment = 1;

            for (int i = 1; i <= rounds; i++)
            {
                // Attempt to reduce level each round to avoid using large numbers.
                var existsUnderMin = monkeys.Any(m => m[1].Split(",").Any(t => !string.IsNullOrWhiteSpace(t) && Int32.Parse(t) < rounds + increment - 1));

                if (!existsUnderMin)
                {
                    for (int j = 0; j < monkeys.Count; j++)
                    {
                        var monkey = monkeys[j];
                        monkeys[j][1] = string.Join(",", monkey[1].Split(",").Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => (Int32.Parse(t) % (rounds + increment)).ToString()));
                    }

                    increment++;
                }

                for (int j = 0; j < monkeys.Count; j++)
                {
                    var monkey = monkeys[j];
                    var items = monkey[1].Split(",").ToList();
                    var operationCalc = monkey[2];
                    var operationP1 = operationCalc.Substring(0, operationCalc.IndexOf(" ")).Trim();
                    var operationP2 = operationCalc.Substring(operationCalc.LastIndexOf(" "), operationCalc.Length - operationCalc.LastIndexOf(" ")).Trim();

                    for (int k = 0; k < items.Count; k++)
                    {
                        if (string.IsNullOrWhiteSpace(items[k].Trim()))
                        {
                            break;
                        }

                        monkeys[j][6] = (Int32.Parse(monkeys[j][6]) + 1).ToString();

                        int currentLevel = Int32.Parse(items[k].Trim());

                        //currentLevel = currentLevel > 1000 ? currentLevel / 100 : currentLevel;
                        int worryLevel = 0;
                        //Console.WriteLine($"currentLevel: {currentLevel}");

                        if (operationCalc.Contains("*"))
                        {
                            worryLevel = GetValue(operationP1, currentLevel) * GetValue(operationP2, currentLevel);
                        }
                        else if (operationCalc.Contains("+"))
                        {
                            worryLevel = GetValue(operationP1, currentLevel) + GetValue(operationP2, currentLevel);
                        }

                        var levelToPass = Math.Floor(Math.Abs(worryLevel) / worryLevelDivider);

                        //Console.WriteLine($"round: {i}, monkey: {monkey[0]}, item: {currentLevel}, levelToPass: {levelToPass}, worryLevel: {worryLevel}");

                        if (levelToPass % Int64.Parse(monkey[3]) == 0)
                        {
                            //Console.WriteLine($"pass to monkey: {monkey[4]}, levelToPass: {levelToPass}");
                            monkeys[Int32.Parse(monkey[4])][1] += (string.IsNullOrWhiteSpace(monkeys[Int32.Parse(monkey[4])][1]) ? string.Empty : ",") + levelToPass;
                        }
                        else
                        {
                            //Console.WriteLine($"pass to monkey: {monkey[5]}, levelToPass: {levelToPass}");
                            monkeys[Int32.Parse(monkey[5])][1] += (string.IsNullOrWhiteSpace(monkeys[Int32.Parse(monkey[5])][1]) ? string.Empty : ",") + levelToPass;
                        }

                        //Console.WriteLine($"monkey: {monkey[0]}, item length: {items[k].Length}");
                    }

                    monkeys[j][1] = string.Empty;

                    //Console.WriteLine($"items: {string.Join(',', items)}");
                }

                foreach (var monkey in monkeys)
                {
                    var response = $"round: {i}, monkey: {monkey[0]}, test: {monkey[3]}, items: {string.Join(',', monkey[1])}, inspected: {monkey[6]}";
                    sw.WriteLine(response);
                    Console.WriteLine(response);
                }
            }

            sw.Close();

            return monkeys.Select(m => Int32.Parse(m[6])).OrderByDescending(m => m).Take(2).Aggregate((a, x) => a * x);
        }

        private int GetValue(string operationValue, int currentValue)
        {
            return Math.Abs(Int32.Parse(operationValue.Replace("old", currentValue.ToString())));
        }
    }
}