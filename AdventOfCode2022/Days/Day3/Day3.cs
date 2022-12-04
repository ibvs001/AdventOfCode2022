namespace AdventOfCode2022.Days
{
    internal class Day3
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines(@"D:\Development\AdventOfCode\AdventOvCode2022\AdventOfCode2022\Days\Day3\Input.txt");

            var prioritySum = 0;
            var packPrioritySum = 0;

            for (int j = 0; j < lines.Length; j++)
            {
                var comp1 = lines[j].Substring(0, lines[j].Length / 2).Distinct().ToList();
                var comp2 = lines[j].Substring(lines[j].Length / 2, lines[j].Length / 2).Distinct().ToList();

                var dups = new List<char>();

                for (int i = 0; i < comp1.Count; i++)
                {
                    if (comp2.Any(c2 => c2 == comp1[i]))
                    {
                        dups.Add(comp1[i]);
                    }
                }

                //Console.WriteLine($"comp1: {comp1}");
                //Console.WriteLine($"comp2: {comp2}");
                //Console.WriteLine($"pack: {lines[0]}");

                foreach (var dup in dups)
                {
                    var enumValue = Enum.Parse(typeof(ItemPriorities), dup.ToString());
                    prioritySum += enumValue.GetHashCode() + 1;
                    //Console.WriteLine($"dup: {dup}");
                }

                //Console.WriteLine($"prioritySum: {prioritySum}");

                if (j % 3 == 0 || j == 0)
                {
                    var pack1 = lines[j].Distinct().ToList();
                    var pack2 = lines[j + 1].Distinct().ToList();
                    var pack3 = lines[j + 2].Distinct().ToList();
                    var packDups = new List<char>();

                    //Console.WriteLine($"pack1: {pack1}");
                    //Console.WriteLine($"pack2: {pack2}");
                    //Console.WriteLine($"pack3: {pack3}");

                    for (int i = 0; i < pack1.Count; i++)
                    {
                        if (pack2.Any(p2 => p2 == pack1[i]) && pack3.Any(p3 => p3 == pack1[i]))
                        {
                            packDups.Add(pack1[i]);
                        }
                    }

                    foreach (var dup in packDups)
                    {
                        var enumValue = Enum.Parse(typeof(ItemPriorities), dup.ToString());
                        packPrioritySum += enumValue.GetHashCode() + 1;
                        //Console.WriteLine($"dup: {dup}");
                    }
                }
            }

            Console.WriteLine($"part 1: {prioritySum}");
            Console.WriteLine($"part 2: {packPrioritySum}");
        }
    }
}
