using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ScoreClient
{
    private static readonly HttpClient client = new HttpClient();

    public async Task SubmitScoreAsync(string scoreValue)
    {
        var score = new Score { Value = scoreValue };
        var json = JsonSerializer.Serialize(score);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await client.PostAsync("http://localhost:5000/submit", content);
    }

    public async Task<List<Score>> GetHighScoresAsync()
    {
        var response = await client.GetAsync("http://localhost:5000/highscores");
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Score>>(json);
    }
}

public class Score
{
    public string Value { get; set; }
}
