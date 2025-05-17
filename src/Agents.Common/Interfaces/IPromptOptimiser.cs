using Agents.Common.Results;

namespace Agents.Common.Interfaces;

public interface IPromptOptimiser
{
    public string OptimisePrompt(Goal goal);
    public string OptimiseResponse(string response);
}