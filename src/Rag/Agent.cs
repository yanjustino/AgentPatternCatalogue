using Agents.Common.Interfaces;

namespace Rag;

/// <summary>
/// Represents an agent that processes user prompts and generates goals asynchronously.
/// </summary>
public class Agent(Creator creator, IFoundationModel llm, IPromptOptimiser optimiser)
{
    /// <summary>
    /// Executes the agent's main processing loop, handling user prompts and generating goals asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation of the agent's execution loop.</returns>
    public async Task RunAsync()
    {
        while (true)
        {
            var goal = await creator.GenerateGoalAsync();
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
            creator.Context.UserInterface.Notify("[Rag] No prompt provided.");
            return;
        }

        var gui = creator.Context.UserInterface;
        
        gui.Notify("[Rag] Querying local LLaMA...");

        var result = await llm.SendMessage(prompt);
        var text = optimiser.OptimiseResponse(result?.Trim() ?? "[no response]");

        gui.Notify($"[result] {text}");
    }
}