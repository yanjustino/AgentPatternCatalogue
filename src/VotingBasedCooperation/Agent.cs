using Agents.Common.Interfaces;
using Agents.Common.Storage;

namespace VotingBasedCooperation;

public abstract class Agent(string id, IFoundationModel? llm = null)
{
    public string id { get; } = id;
    
    public abstract Task<string> VoteAsync(string storeId, ContextData stories);
    
    protected async Task<string?> ExecutePrompt(string? prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
            throw new ArgumentException("Prompt cannot be null or whitespace.", nameof(prompt));

        if (llm is null)
            throw new InvalidOperationException("Foundation model (llm) is not initialized.");
        
        return await llm.SendMessage(prompt);
    }
}