using Agents.Common;
using Agents.Common.Interfaces;

namespace PassiveGoalCreator;

/// <summary>
/// Represents an agent that facilitates interaction between a user and the goal creation system.
/// The agent uses a dialogue interface for user input and output, and a goal creation mechanism
/// to process and generate goals. The agent runs in an asynchronous loop to continuously process
/// user inputs until the agent is signaled to stop.
/// </summary>
/// <remarks>
/// This class is responsible for handling the main operational logic of the agent, including:
/// - Retrieving user prompts through the provided dialogue interface.
/// - Creating prompts based on user inputs.
/// - Executing tasks associated with the generated prompts.
/// - Shutting down gracefully upon user request.
/// </remarks>
public class Agent(Creator creator, ILLmClient llm, IPromptOptimiser optimiser)
{
    /// <summary>
    /// Runs the main execution loop for the agent asynchronously.
    /// The loop continuously retrieves user inputs, processes them into prompts,
    /// and executes tasks associated with those prompts until the agent is signaled to stop.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation of the agent's execution loop.
    /// </returns>
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
    /// Executes the given prompt by querying the local language model and processing the response.
    /// </summary>
    /// <param name="prompt">The input string containing the query to be processed by the language model.</param>
    /// <returns>A task representing the asynchronous operation of executing the prompt and outputting the result.</returns>
    private async Task ExecutePrompt(string? prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
        {
            creator.Context.AgentGui.Notify("[ProactiveGoalCreator] No prompt provided.");
            return;
        }
        
        var gui = creator.Context.AgentGui;

        gui.Notify("[PassiveGoalCreator] Querying local LLaMA...");

        var result = await llm.SendMessage(prompt);
        var text = optimiser.OptimiseResponse(result?.Trim() ?? "[no response]");

        gui.Notify($"[Identified Goal] {text}");
    }
}