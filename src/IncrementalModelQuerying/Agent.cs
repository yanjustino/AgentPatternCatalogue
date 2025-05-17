using Agents.Common.Interfaces;

namespace IncrementalModelQuerying;

/// <summary>
/// Represents an agent that processes user prompts and generates goals asynchronously.
/// </summary>
public class Agent(GoalCreator goalCreator, IFoundationModel llm)
{
    /// <summary>
    /// Executes the agent's main processing loop, handling user prompts and generating goals asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation of the agent's execution loop.</returns>
    public async Task RunAsync()
    {
        var gui = goalCreator.Context.UserInterface;
        
        while (true)
        {
            var goal = await goalCreator.GenerateGoalAsync();
            if (goal is null) break;
            
            gui.Notify($"[steps] {goal.Context}");
        }
    }
}