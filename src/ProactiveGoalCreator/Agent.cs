using Agents.Common;
using Agents.Common.Interfaces;

namespace ProactiveGoalCreator;

public partial class Agent(IDialogueInterface gui, GoalCreator creator, IEnumerable<IContextDetector> detectors)
{
    public async Task RunAsync()
    {
        while (true)
        {
            var (stopAgent, goal) = gui.GetUserPrompt();
            if (stopAgent) break;
            
            var prompt = CreatePrompt(goal);
            await ExecutePrompt(prompt);
        }

        Console.WriteLine("\n[Agent] Shutting down.");
    }
}