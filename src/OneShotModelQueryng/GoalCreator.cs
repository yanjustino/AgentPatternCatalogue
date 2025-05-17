using Agents.Common;
using Agents.Common.Interfaces;
using Agents.Common.Results;
using Agents.Common.Storage;

namespace OneShotModelQueryng;

/// <summary>
/// The GoalCreator class is responsible for creating and modifying a Goal object
/// by merging its contextual data with additional data from an IMemoryStore implementation.
/// </summary>
public class GoalCreator(AgentContext context, IPlanGeneration planner): IGoalCreatorAsync
{
    public AgentContext Context { get; } = context;

    /// <summary>
    /// Generates a new Goal object by retrieving user input through the DialogueInterface
    /// and combining its contextual data with additional data retrieved from the memory store.
    /// </summary>
    /// <returns>A newly created Goal object with merged contextual information, or null if no user input is provided.</returns>
    public async Task<Goal?> GenerateGoalAsync()
    {
        var prompt = Context.UserInterface.GetUserPrompt();
        if (prompt is null) return null;

        var plan = await planner.Generate(prompt.Intent!, 5);
        var data = ContextData.MergeAll(("[Task]", plan?.ToString() ?? ""));
        
        return prompt with { Context = data };
    }
}