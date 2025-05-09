using Agents.Common;
using Agents.Common.Interfaces;

namespace PassiveGoalCreator;

public class GoalCreator(IMemoryStore memoryStore)
{
    public Goal GenerateGoal(Goal prompt)
    {
        var mergedContext = ContextData.MergeAll(prompt.Context!, memoryStore.RetrieveContext());
        return prompt with { Context = mergedContext };
    }
}