using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ooadepazar.Controllers;

public class OpenAIController
{
    private static readonly HttpClient _httpClient = new HttpClient();

    public async Task<string> SendMessageAsync()
    {
        string apiKey = "your_openai_api_key";

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);

        var requestBody = new
        {
            model = "gpt-4o", // or "gpt-3.5-turbo"
            messages = new[]
            {
                new { role = "system", content = "Odgovaraj samo na bosanskom jeziku. Budi kratak i jasan pomoćnik." },
                new { role = "user", content = "Kako mogu poboljšati svoje vještine programiranja?" }
            }
        };

        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
        response.EnsureSuccessStatusCode();

        using var responseStream = await response.Content.ReadAsStreamAsync();
        var json = await JsonSerializer.DeserializeAsync<JsonElement>(responseStream);

        var reply = json
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        return reply;
    }
}