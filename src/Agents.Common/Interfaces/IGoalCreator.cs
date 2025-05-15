using Agents.Common.Models;

namespace Agents.Common.Interfaces;

public interface IGoalCreator
{
    public AgentContext Context { get; }

    public Goal? GenerateGoal();
}

public interface IGoalCreatorAsync
{
    public AgentContext Context { get; }

    public Task<Goal?> GenerateGoalAsync();
}