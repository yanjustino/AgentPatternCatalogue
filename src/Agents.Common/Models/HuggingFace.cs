using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Agents.Common.Interfaces;

namespace Agents.Common.Models;

public class HuggingFace : IFoundationModel
{
    private readonly string _apiKey;
    private readonly string _modelName;

    private HuggingFace(string apiKey, string modelName)
    {
        _modelName = modelName; 
        _apiKey = apiKey;
    }    
    
    public static HuggingFace Create(string modelName)
    {
        var key = Environment.GetEnvironmentVariable("HF_API_KEY") ?? "not-found";
        return new HuggingFace(key, modelName);
    }    

    public async Task<string?> SendMessage(string prompt)
    {
        var inputJson = $"{{ \"inputs\": \"{prompt}\" }}";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"https://router.huggingface.co/hf-inference/models/{_modelName}", content);

        return await response.Content.ReadAsStringAsync();
    }
}