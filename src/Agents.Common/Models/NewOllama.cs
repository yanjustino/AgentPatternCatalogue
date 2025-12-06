using Agents.Common.Interfaces;
using Microsoft.Extensions.AI;
using OllamaSharp;

namespace Agents.Common.Models;

public partial class NewOllama: IFoundationModel
{
    
    private Chat Client { get; init; }
    private readonly List<ChatMessage> _chatHistory = [];
    private readonly bool _preserveHistory;    
    
    
    private NewOllama(Chat client, bool preserveHistory = true)
    {
        Client = client;
        _preserveHistory = preserveHistory;
    }
    
    public async Task<string?> SendMessage(string prompt)
    {
        if (!_preserveHistory) _chatHistory.Clear();
        
        var response = "";
        
        await foreach (var item in Client.SendAsync(prompt))
        {
            response += item;
        }
        
        _chatHistory.Add(new (ChatRole.Assistant, response));
        
        return response;
    }
}

/// <summary>
/// Provides a client implementation for interacting with a language model.
/// This class includes support for creating clients with specific configurations, such as custom endpoints or default models.
/// </summary>
public partial class NewOllama
{
    public static IFoundationModel Create(string? endpoint = null, string? model = null, bool preserveHistory = true)
    {
        var client = new Chat(new OllamaApiClient(endpoint ?? "http://localhost:11434", model ?? "llama3"));
        return new NewOllama(client, preserveHistory);
    }
}