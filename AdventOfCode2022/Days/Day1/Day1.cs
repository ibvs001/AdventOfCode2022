using System.Linq;

namespace AdventOfCode2022.Days
{
    internal class Day1
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines(@"D:\Development\AdventOfCode\AdventOvCode2022\AdventOfCode2022\Days\Day1\Input.txt");

            var elfCalories = new List<int>();
            var currentCalories = 0;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    elfCalories.Add(currentCalories);
                    currentCalories = 0;
                }
                else
                {
                    currentCalories += Int32.Parse(line);
                }
            }

            Console.WriteLine($"top 1 calories = {elfCalories.OrderByDescending(e => e).First()}");
            Console.WriteLine($"top 3 calories = {elfCalories.OrderByDescending(e => e).Take(3).Sum()}");
        }
    }
}
