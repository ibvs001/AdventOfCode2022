namespace AdventOfCode2022.Days
{
    internal class Day4
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines(@"D:\Development\AdventOfCode\AdventOfCode2022\AdventOfCode2022\Days\Day4\Input.txt");

            var completeOverlapCount = 0;
            var overlapCount = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                var tempPairs = lines[i];
                var pair1 = tempPairs.Substring(0, tempPairs.IndexOf(','));
                var pair2 = tempPairs.Substring(tempPairs.IndexOf(',') + 1, tempPairs.Length - pair1.Length - 1);

                var pair1Start = Int32.Parse(pair1.Substring(0, pair1.IndexOf('-')));
                var pair1End = Int32.Parse(pair1.Substring(pair1.IndexOf('-') + 1, pair1.Length - pair1.Substring(0, pair1.IndexOf('-')).Length - 1));

                var pair2Start = Int32.Parse(pair2.Substring(0, pair2.IndexOf('-')));
                var pair2End = Int32.Parse(pair2.Substring(pair2.IndexOf('-') + 1, pair2.Length - pair2.Substring(0, pair2.IndexOf('-')).Length - 1));

                if (pair1Start >= pair2Start && pair1Start <= pair2End && pair1End >= pair2Start && pair1End <= pair2End ||
                    pair2Start >= pair1Start && pair2Start <= pair1End && pair2End >= pair1Start && pair2End <= pair1End)
                {
                    //Console.WriteLine($"complete overlap");
                    completeOverlapCount++;
                    //overlapCount++;
                }

                if (pair1Start >= pair2Start && pair1Start <= pair2End || pair1End >= pair2Start && pair1End <= pair2End ||
                    pair2Start >= pair1Start && pair2Start <= pair1End || pair2End >= pair1Start && pair2End <= pair1End)
                {
                    //Console.WriteLine($"overlap");
                    overlapCount++;
                }

                //Console.WriteLine($"pair1: {pair1}");
                //Console.WriteLine($"pair2: {pair2}");
            }

            Console.WriteLine($"part 1: {completeOverlapCount}");
            Console.WriteLine($"part 2: {overlapCount}");
        }
    }
}
