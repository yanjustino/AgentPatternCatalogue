using Agents.Common;
using Agents.Common.Interfaces;

namespace ProactiveGoalCreator;

public class Agent(Creator creator, IAgentLLmClient llm, IPromptOptimiser optimiser)
{
    /// <summary>
    /// Executes the agent's main processing loop, handling user prompts and generating goals asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation of the agent's execution loop.</returns>
    public async Task RunAsync()
    {
        while (true)
        {
            var goal = creator.GenerateGoal();
            if (goal is null) break;

            var promptString = optimiser.OptimisePrompt(goal);
            await ExecutePrompt(promptString);
        }
    }

    /// <summary>
    /// Sends the specified prompt to the LLm model for processing and outputs the result.
    /// </summary>
    /// <param name="prompt">The input string containing instructions or queries to be processed by the LLm model.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task ExecutePrompt(string? prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
        {
            creator.Context.AgentGui.Notify("[ProactiveGoalCreator] No prompt provided.");
            return;
        }

        var gui = creator.Context.AgentGui;
        
        gui.Notify("[ProactiveGoalCreator] Querying local LLaMA...");

        var result = await llm.SendMessage(prompt);
        var text = optimiser.OptimiseResponse(result?.Trim() ?? "[no response]");

        gui.Notify($"[Identified Goal] {text}");
    }
}