using System.Net.Http.Headers;

namespace AdventOfCode2022.Helpers
{
    internal class InputHelper
    {
        public static void GetInput(string inputPath, int year, int day, string cookie)
        {
            using HttpClient client = new();
            PopulateClientHeaders(client, cookie);

            if (!File.Exists(inputPath))
            {
                var json = client.GetStringAsync($"https://adventofcode.com/{year}/day/{day}/input").Result;
                var sw = new StreamWriter(inputPath);
                sw.WriteLine(json);
                sw.Close();
            }
        }

        public static void SubmitAnswer(string answer, int year, int day, string cookie)
        {
            using HttpClient client = new();
            PopulateClientHeaders(client, cookie);
            var content = new StringContent(answer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var response = client.PostAsync($"https://adventofcode.com/{year}/day/{day}/answer", content).Result;
        }

        private static void PopulateClientHeaders(HttpClient client, string cookie)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            client.DefaultRequestHeaders.Add("cookie", cookie);
        }
    }
}
