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
public partial class Agent(IDialogueInterface gui, GoalCreator goalCreator)
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
            var (stopAgent, goal) = gui.GetUserPrompt();
            if (stopAgent) break;
            
            var prompt = CreatePrompt(goal);
            Console.WriteLine(prompt);
            await ExecutePrompt(prompt);
        }

        Console.WriteLine("\n[Agent] Shutting down.");
    }
}