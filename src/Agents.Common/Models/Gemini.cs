using System.Text.Json;
using Agents.Common.Interfaces;

namespace Agents.Common.Models;

/// <summary>
/// Create your API key at https://aistudio.google.com/apikey
/// </summary>
public partial class Gemini : IFoundationModel
{
    private readonly string _apiKey;

    private Gemini(string apiKey)
    {
        _apiKey = apiKey;
    }
    
    public async Task<string?> SendMessage(string prompt)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://generativelanguage.googleapis.com");
        
        var payload = new { contents = new { parts = new { text = prompt } } };
        var json = JsonSerializer.Serialize(payload);

        var response = await client.PostAsync($"/v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}", 
            new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
        
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine(body);

        var responseBody = JsonDocument.Parse(body).RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();
        
        return responseBody;
    }
}

public partial class Gemini
{
    public static Gemini Create(string apiKey)
    {
        return new Gemini(apiKey);
    }
}