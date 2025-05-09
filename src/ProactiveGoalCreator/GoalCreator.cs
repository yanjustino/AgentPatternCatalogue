using Agents.Common;
using Agents.Common.Interfaces;

namespace ProactiveGoalCreator;

/// <summary>
/// The GoalCreator class is responsible for synthesizing and enhancing goals based on user inputs, memory contexts,
/// and multimodal contextual information captured from various context detectors.
/// </summary>
public class GoalCreator(IDialogueInterface gui, IMemoryStore memory)
{
    /// <summary>
    /// Generates a new Goal by synthesizing provided goal context, memory context,
    /// and multimodal contextual information captured from context detectors.
    /// </summary>
    /// <param name="prompt">The initial Goal object provided as input containing an intent and optional context.</param>
    /// <param name="detectors">A collection of context detectors used to capture multimodal contextual information.</param>
    /// <returns>A new Goal object with an enriched and merged context constructed using the provided goal, memory, and multimodal contexts.</returns>
    public Goal GenerateGoal(Goal prompt, IEnumerable<IContextDetector> detectors)
    {
        var memoryContext = memory.RetrieveContext();

        var multimodalContext = new ContextData();
        foreach (var detector in detectors)
        {
            var data = detector.Capture();
            multimodalContext.Merge(data);
        }

        if (multimodalContext.Data.Count != 0)
            gui.Notify("Multimodal context has been captured to improve goal understanding.");
        
        var merged = ContextData.MergeAll(prompt.Context!, memoryContext, multimodalContext);
        return prompt with { Context = merged };
    }
}