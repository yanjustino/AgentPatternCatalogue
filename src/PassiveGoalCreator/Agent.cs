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
            Console.Write("\n> Enter your prompt (or type 'exit'): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) || input.Equals("exit", StringComparison.CurrentCultureIgnoreCase)) break;

            var memoryContext = Memory.RecentTools;

            Console.WriteLine("[PassiveGoalCreator] Querying local LLaMA...");
            var goal = await GoalCreator.GenerateGoalAsync(input, memoryContext);
            Console.WriteLine($"[Identified Goal] {goal}");
            
            //TODO: use goal to create a task
        }

        Console.WriteLine("\n[Agent] Shutting down.");
    }
}