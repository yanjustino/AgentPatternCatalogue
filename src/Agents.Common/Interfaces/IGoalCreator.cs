namespace Agents.Common.Interfaces;

public interface IGoalCreator
{
    public AgentContext Context { get; }

    public AgentGoal? GenerateGoal();
}