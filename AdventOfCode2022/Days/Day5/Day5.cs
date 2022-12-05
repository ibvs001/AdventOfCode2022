namespace AdventOfCode2022.Days
{
    internal class Day5
    {
        public static void Run()
        {
            string[] stacks = File.ReadAllLines(@"D:\Development\AdventOfCode\AdventOvCode2022\AdventOfCode2022\Days\Day5\Stacks.txt");
            string[] orderedStacks = File.ReadAllLines(@"D:\Development\AdventOfCode\AdventOvCode2022\AdventOfCode2022\Days\Day5\Stacks.txt");
            string[] lines = File.ReadAllLines(@"D:\Development\AdventOfCode\AdventOvCode2022\AdventOfCode2022\Days\Day5\Input.txt");

            var stackOutput1 = string.Empty;
            var stackOutput2 = string.Empty;

            for (int i = 0; i < lines.Length; i++)
            {
                var actions = lines[i].Replace("move ", string.Empty).Replace(" from ", ":").Replace(" to ", "-") + ".";
                var count = Int32.Parse(actions.Substring(0, actions.IndexOf(":")));
                var from = Int32.Parse(actions.Substring(actions.IndexOf(":") + 1, actions.Length - actions.IndexOf("-") - actions.IndexOf(":") - 2 + count.ToString().Length));
                var to = Int32.Parse(actions.Substring(actions.IndexOf("-") + 1, actions.Length - actions.IndexOf(".")));

                Console.WriteLine($"start stack from {from}: {stacks[from - 1]}");
                Console.WriteLine($"start stack to {to}: {stacks[to - 1]}");

                for (int j = 0; j < count; j++)
                {
                    var itemToMove = stacks[from - 1].Last();
                    stacks[from - 1] = stacks[from - 1].Remove(stacks[from - 1].Length - 1, 1);
                    stacks[to - 1] += itemToMove;
                }

                var itemsToMove = orderedStacks[from - 1].Substring(orderedStacks[from - 1].Length - count, count);
                orderedStacks[from - 1] = orderedStacks[from - 1].Remove(orderedStacks[from - 1].Length - count, count);
                orderedStacks[to - 1] += itemsToMove;

                Console.WriteLine(string.Empty);

                Console.WriteLine($"actions: {actions}");
                Console.WriteLine($"count: {count}");
                Console.WriteLine($"from: {from}");
                Console.WriteLine($"to: {to}");
                Console.WriteLine($"stack from: {stacks[from - 1]}");
                Console.WriteLine($"stack to: {stacks[to - 1]}");
                Console.WriteLine("-------------------------");
            }

            for (int i = 0; i < stacks.Length; i++)
            {
                Console.WriteLine($"stack: {stacks[i]}");
                stackOutput1 += stacks[i].Length > 0 ? stacks[i].Last() : " ";
                stackOutput2 += orderedStacks[i].Length > 0 ? orderedStacks[i].Last() : " ";
            }

            Console.WriteLine($"part 1: {stackOutput1}");
            Console.WriteLine($"part 2: {stackOutput2}");
        }
    }
}
