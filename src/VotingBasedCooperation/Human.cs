using System.Text.Json;
using Agents.Common.Storage;

namespace VotingBasedCooperation;

public class Human(string agentId): IVoter
{
    public string AgentId { get; } = agentId;
    
    public async Task<string> VoteAsync(string storeId, ContextData stories)
    {
        Console.WriteLine("\n[Human] Voteing ");
        var userStory =  stories.Data[storeId];
        
        Console.WriteLine("[Human] User Story");
        Console.WriteLine(userStory);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine();
        Console.WriteLine("[Human] Please enter your vote (yes/no): ");
        Console.ResetColor();
        var vote = Console.ReadLine()?.Trim().ToLowerInvariant();
        
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[Human] Justification for your vote: ");
        Console.ResetColor();
        var justification = Console.ReadLine()?.Trim();

        var result = new
        {
            agent_id = AgentId,
            user_story_id = storeId,
            story_point = vote ?? "0",
            justification = justification ?? "No justification provided."
        };

        await Task.Yield();
        
        return JsonSerializer.Serialize(result);
    }
}