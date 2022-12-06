using AdventOfCode2022.Helpers;

namespace AdventOfCode2022.Days
{
    internal class Day6
    {
        static int year = 2022;
        static int day = 6;
        static string inputPath = $@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\Input.txt";
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
            catch(Exception)
            {
                Console.WriteLine("Unable to submit!");
                throw;
            }
        }

        private void SolveProblem(string[] lines)
        {
            var signal = lines[0];
            part1 = GetSignal(4, signal);
            part2 = GetSignal(14, signal);

            Console.WriteLine($"part1: {part1}");
            Console.WriteLine($"part2: {part2}");
        }

        private int GetSignal(int count, string signal)
        {
            var part = -1;
            for (int i = count - 1; i < signal.Length; i++)
            {
                var result = string.Empty;
                var dups = 0;

                for (int j = 0; j < count; j++)
                {
                    var rPart = signal[i - j].ToString();
                    result += rPart;

                    //Console.WriteLine($"rPartCount: {result.Count(r => r.ToString() == rPart)}");
                    //Console.WriteLine($"dups: {dups}");
                    //Console.WriteLine($"rPart: {rPart}");

                    if (result.Count(r => r.ToString() == rPart) > 1)
                    {
                        dups++;
                        break;
                    }
                }

                //Console.WriteLine($"signal: {result}");

                if (dups == 0)
                {
                    part = i + 1;
                    break;
                }
            }

            return part;
        }
    }
}
