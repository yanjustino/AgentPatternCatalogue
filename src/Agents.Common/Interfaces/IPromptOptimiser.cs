namespace Agents.Common.Interfaces;

public interface IPromptOptimiser
{
    public string OptimisePrompt(AgentGoal goal);
    public string OptimiseResponse(string response);
}