using Agents.Common;
using Agents.Common.Interfaces;

namespace ProactiveGoalCreator;

public class GoalCreator(IDialogueInterface gui, IMemoryStore memory)
{
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