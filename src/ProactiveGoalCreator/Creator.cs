using Agents.Common;
using Agents.Common.Interfaces;
using Agents.Common.Results;
using Agents.Common.Storage;

namespace ProactiveGoalCreator;

/// <summary>
/// The GoalCreator class is responsible for synthesizing and enhancing goals based on user inputs, memory contexts,
/// and multimodal contextual information captured from various context detectors.
/// </summary>
public class Creator(AgentContext context, IEnumerable<IContextDetector> detectors): IGoalCreator
{
    public AgentContext Context { get; } = context;

    /// <summary>
    /// Generates a goal by synthesizing user-provided input, memory context, and multimodal contextual data.
    /// The method combines information from user dialogue, retrieved memory, and context detectors to create
    /// a comprehensive goal representation.
    /// </summary>
    /// <returns>A synthesized <see cref="Goal"/> object if the inputs are valid; otherwise, null.</returns>
    public Goal? GenerateGoal()
    {
        var prompt = Context.UserInterface.GetUserPrompt();
        if (prompt is null) return null;
        
        var memoryContext = Context.MemoryStore.RetrieveContext();
        var multimodalContext = new ContextData();
        
        foreach (var detector in detectors)
        {
            var data = detector.Capture();
            multimodalContext.Merge(data);
        }

        if (multimodalContext.Data.Count != 0)
            Context.UserInterface.Notify("Multimodal context has been captured to improve goal understanding.");
        
        var merged = ContextData.MergeAll(prompt.Context!, memoryContext, multimodalContext);
        return prompt with { Context = merged };
    }
}