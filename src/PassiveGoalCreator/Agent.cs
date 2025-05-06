namespace PassiveGoalCreator;

using System;
using System.Threading.Tasks;

public class Agent
{
    public PassiveGoalCreator GoalCreator { get; } = new();

    public async Task RunAsync()
    {
        while (true)
        {
            var input = GetPrompt();
            if (input.exit) break;
            await QueryLlm(input.prompt!);
            //TODO: use goal to create a task
        }

        Console.WriteLine("\n[Agent] Shutting down.");
    }

    private static (bool exit, string? prompt) GetPrompt()
    {
        Console.Write("\n> Enter your prompt (or type 'exit'): ");
        var input = Console.ReadLine();
        return string.IsNullOrWhiteSpace(input) || input.Equals("exit", StringComparison.CurrentCultureIgnoreCase) 
            ? (true, input) : (false, input);
    }

    private async Task<string> QueryLlm(string prompt)
    {
        var memory = Memory.RecentTools;
        Console.WriteLine("[PassiveGoalCreator] Querying local LLaMA...");
        var goal = await GoalCreator.GenerateGoalAsync(prompt, memory);
        Console.WriteLine($"[Identified Goal] {goal}");
        
        return goal;
    }
}