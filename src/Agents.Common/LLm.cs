using Microsoft.Extensions.AI;

namespace Agents.Common;

public interface ILLm
{
    Task<string?> SendMessage(string prompt);
}

public class LLm(string model = "llama3") : ILLm
{
    private OllamaChatClient Client => new ("http://localhost:11434/", model);
    private readonly List<ChatMessage> _chatHistory = [];
    
    public async Task<string?> SendMessage(string prompt)
    {
        _chatHistory.Add(new ChatMessage(ChatRole.User, prompt));
        var response = "";
        await foreach(var item in Client.GetStreamingResponseAsync(_chatHistory))
        {
            response += item;
        }
        _chatHistory.Add(new ChatMessage(ChatRole.Assistant, response));
        return response;
    }
}