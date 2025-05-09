using Agents.Common;
using Agents.Common.Interfaces;

namespace PassiveGoalCreator;

/// <summary>
/// The GoalCreator class is responsible for creating and modifying a Goal object
/// by merging its contextual data with additional data from an IMemoryStore implementation.
/// </summary>
public class GoalCreator(IMemoryStore memoryStore)
{
    /// <summary>
    /// Generates a new Goal object by merging the contextual data of the provided Goal with additional
    /// context retrieved from an IMemoryStore implementation.
    /// </summary>
    /// <param name="prompt">The initial Goal object containing the intent and context to be merged.</param>
    /// <returns>A new Goal object with the original intent and updated merged context.</returns>
    public Goal GenerateGoal(Goal prompt)
    {
        var mergedContext = ContextData.MergeAll(prompt.Context!, memoryStore.RetrieveContext());
        return prompt with { Context = mergedContext };
    }
}