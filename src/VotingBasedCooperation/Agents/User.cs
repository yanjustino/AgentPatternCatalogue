using System.Text.Json;
using Agents.Common.Storage;

namespace VotingBasedCooperation.Agents;

public class User(string id): Agent(id)
{
    public override async Task<string> VoteAsync(string storeId, ContextData stories)
    {
        Console.WriteLine("\n[User] Voteing ");
        var userStory =  stories.Data[storeId];
        
        Console.WriteLine("[User] User Story");
        Console.WriteLine(userStory);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine();
        Console.WriteLine("[User] Please enter your vote (1, 2, 3, 5, 8, 13): ");
        Console.ResetColor();
        var vote = Console.ReadLine()?.Trim().ToLowerInvariant();
        
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[User] Justification for your vote: ");
        Console.ResetColor();
        var justification = Console.ReadLine()?.Trim();

        var result = new
        {
            agent_id = id,
            user_story_id = storeId,
            story_point = vote ?? "0",
            justification = justification ?? "No justification provided."
        };

        await Task.Yield();
        
        return JsonSerializer.Serialize(result);
    }
}