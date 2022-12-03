using System.Linq;

namespace AdventOfCode2022.Days
{
    internal class Day2
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines(@"D:\Development\AdventOfCode\AdventOvCode2022\AdventOfCode2022\Days\Day2\Input.txt");

            var totalScoreP1 = 0;
            var totalScoreP2 = 0;

            foreach (var line in lines)
            {
                /*
                A = rock = 1
                B = paper = 2
                C = scissors = 3

                X = rock
                Y = paper
                Z = scissors

                X = lose = 0
                Y = draw = 3
                Z = win = 6
                */

                var opponentMove = line.Split(" ")[0];
                var myMove = line.Split(" ")[1];
                var myResult = myMove;
                var score = 0;

                if (myMove == "X")
                {
                    score += 1;
                }
                if (myMove == "Y")
                {
                    score += 2;
                }
                if (myMove == "Z")
                {
                    score += 3;
                }

                if (myMove == "X" && opponentMove == "C" || myMove == "Y" && opponentMove == "A" || myMove == "Z" && opponentMove == "B")
                {
                    score += 6;
                }
                if (myMove == "X" && opponentMove == "A" || myMove == "Y" && opponentMove == "B" || myMove == "Z" && opponentMove == "C")
                {
                    score += 3;
                }

                totalScoreP1 += score;

                score = 0;

                if (myResult == "X") // lose
                {
                    if (opponentMove == "A")
                    {
                        score += 3;
                    }
                    if (opponentMove == "B")
                    {
                        score += 1;
                    }
                    if (opponentMove == "C")
                    {
                        score += 2;
                    }
                }

                if (myResult == "Y") // draw
                {
                    score += 3;

                    if (opponentMove == "A")
                    {
                        score += 1;
                    }
                    if (opponentMove == "B")
                    {
                        score += 2;
                    }
                    if (opponentMove == "C")
                    {
                        score += 3;
                    }
                }

                if (myResult == "Z") // draw
                {
                    score += 6;

                    if (opponentMove == "A")
                    {
                        score += 2;
                    }
                    if (opponentMove == "B")
                    {
                        score += 3;
                    }
                    if (opponentMove == "C")
                    {
                        score += 1;
                    }
                }

                totalScoreP2 += score;
            }

            Console.WriteLine($"part 1: {totalScoreP1}");
            Console.WriteLine($"part 2: {totalScoreP2}");
        }
    }
}
