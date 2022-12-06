using System.Net.Http.Headers;

namespace AdventOfCode2022.Days
{
    internal class TestingDay
    {
        private static void GetInput()
        {
            var inputPath = @"D:\Development\AdventOfCode\AdventOfCode2022\AdventOfCode2022\Testing\Input.txt";

            if (!File.Exists(inputPath))
            {
                using HttpClient client = new();
                PopulateClientHeaders(client);

                var json = client.GetStringAsync("https://adventofcode.com/2021/day/1/input").Result;
                var sw = new StreamWriter(inputPath);
                sw.WriteLine(json);
                sw.Close();
            }
        }

        private static void SubmitAnswer()
        {
            using HttpClient client = new();
            PopulateClientHeaders(client);
            var content = new StringContent("1234");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var response = client.PostAsync("https://adventofcode.com/2021/day/1/answer", content).Result;
        }

        private static void PopulateClientHeaders(HttpClient client)
        {
            var cookie = File.ReadAllText(@"D:\Development\AdventOfCode\cookie.txt");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            client.DefaultRequestHeaders.Add("cookie", cookie);
        }

        public static void Run(int submitPartNumber = -1)
        {
            //GetInput();
            SubmitAnswer();
        }
    }
}
