using AdventOfCode2022.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace AdventOfCode2022.Days
{
    internal class Directory
    {
        public int Index { get; set; }
        public string FullStructure { get; set; }
        public string Name { get; set; }
        public (int, string) Parent { get; set; }
        public List<string> Files { get; set; }
    }

    internal class Day7
    {
        static int year = 2022;
        static int day = 7;
        static string inputPath = $@"D:\Development\AdventOfCode\AdventOfCode{year}\AdventOfCode{year}\Days\Day{day}\Input.txt";
        static string cookiePath = @"D:\Development\AdventOfCode\cookie.txt";

        static int part1 = -1;
        static int part2 = -1;

        public void Run(int submitPartNumber = -1)
        {
            var cookie = File.ReadAllText(cookiePath);
            InputHelper.GetInput(inputPath, year, day, cookie);
            string[] lines = File.ReadAllLines(inputPath);

            //SolveProblem(lines);
            SolveProblem2(lines);

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

        private void SolveProblem2(string[] lines)
        {
            var finalSizes = new List<List<string>>();

            var directories = new List<Directory>();
            var currentDirectory = (0, string.Empty);
            var directoryIndex = 0;
            var currentStructure = string.Empty;

            lines = lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                if (line.Contains("$ cd") && !line.Contains(".."))
                {
                    directoryIndex++;
                    currentDirectory = (directoryIndex, line.Replace("$ cd ", string.Empty));

                    currentStructure += $"_{line.Replace("$ cd ", string.Empty)}";
                    //Console.WriteLine($"currentStructure: {currentStructure}");

                    if (!directories.Any(d => d.FullStructure == currentStructure && d.Name == line.Replace("$ cd ", string.Empty)))
                    {
                        directories.Add(new Directory
                        {
                            Index = currentDirectory.Item1,
                            FullStructure = currentStructure,
                            Name = line.Replace("$ cd ", string.Empty),
                            Parent = currentDirectory,
                            Files = new List<string>()
                        });
                    }
                }
                else if (line.Contains("dir "))
                {
                    if (!directories.Any(d => d.FullStructure == currentStructure && d.Name == line.Replace("dir ", string.Empty)))
                    {
                        directories.Add(new Directory
                        {
                            Index = currentDirectory.Item1,
                            FullStructure = currentStructure,
                            Name = line.Replace("dir ", string.Empty),
                            Parent = currentDirectory,
                            Files = new List<string>()
                        });
                    }
                }
                else if (!line.Contains("$"))
                {
                    var directory = directories.Single(d => d.FullStructure == currentStructure && d.Name == currentDirectory.Item2);
                    //Console.WriteLine($"file: {line}");
                    directory.Files.Add(line);
                }
                else if (line.Contains(".."))
                {
                    currentStructure = currentStructure.Substring(0, currentStructure.LastIndexOf("_"));
                    directoryIndex--;
                }
            }

            foreach (var directory in directories)
            {
                var fileSizes = directory.Files.Sum(f => Int32.Parse(f.Substring(0, f.IndexOf(" "))));

                if (!finalSizes.Any(f => f[0] == directory.FullStructure))
                {
                    finalSizes.Add(new List<string> { directory.FullStructure, fileSizes.ToString() });
                    //Console.WriteLine($"FullStructure: {directory.FullStructure}, fileSizes: {fileSizes}");
                }

                var newStructure = directory.FullStructure;

                while (!string.IsNullOrWhiteSpace(newStructure))
                {
                    newStructure = newStructure.Substring(0, newStructure.LastIndexOf("_"));
                    var parent = finalSizes.SingleOrDefault(f => f[0] == newStructure);

                    if (parent != null)
                    {
                        //Console.WriteLine($"parent: {parent[0]}");
                        //Console.WriteLine($"fileSize before: {parent[1]}");
                        parent[1] = (Int32.Parse(parent[1]) + fileSizes).ToString();
                        //Console.WriteLine($"fileSize after: {parent[1]}");
                    }
                }
            }

            foreach (var item in finalSizes.OrderByDescending(f => Int32.Parse(f[1])))
            {
                Console.WriteLine($"directory: {item[0]}, size: {item[1]}");
            }

            var rootSize = finalSizes.OrderByDescending(f => Int32.Parse(f[1])).Max(f => Int32.Parse(f[1]));
            var availableSpace = 70000000 - rootSize;
            var directoryToDelete = finalSizes.Where(f => Int32.Parse(f[1]) + availableSpace >= 30000000).OrderByDescending(f => Int32.Parse(f[1])).Min(f => Int32.Parse(f[1]));

            Console.WriteLine($"total space used: {finalSizes.Sum(f => Int32.Parse(f[1]))}");
            Console.WriteLine($"availableSpace: {availableSpace}");
            Console.WriteLine($"largest directory: {directoryToDelete}");

            part1 = finalSizes.Where(f => Int32.Parse(f[1]) < 100000).Sum(f => Int32.Parse(f[1]));
            part2 = directoryToDelete;

            Console.WriteLine($"part1: {part1}");
            Console.WriteLine($"part2: {part2}");
        }
    }
}