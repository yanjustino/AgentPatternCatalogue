using Agents.Common;
using Agents.Common.Interfaces;

namespace ProactiveGoalCreator;

/// <summary>
/// Represents the primary Agent of the Proactive Goal Creator system.
/// The Agent interacts with a dialogue interface to process user inputs, utilizes a goal creator for goal generation,
/// and leverages context detectors to enhance context understanding for better goal processing.
/// </summary>
/// <remarks>
/// The Agent encapsulates a loop that continuously prompts the user for inputs, processes the input to generate a goal,
/// and operates until the user explicitly decides to stop the execution. The runtime behavior relies on the interplay of the provided dialogue interface, goal creator, and context detectors.
/// </remarks>
/// <param name="gui">An implementation of IDialogueInterface, responsible for handling user input and notifications.</param>
/// <param name="creator">An instance of GoalCreator, used to generate goals based on user input and additional context.</param>
/// <param name="detectors">A collection of IContextDetector instances, used to capture and provide contextual data for goal enhancement.</param>
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