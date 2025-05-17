using Agents.Common.Results;

namespace Agents.Common.Interfaces;

public interface IPlanGeneration
{
    public Task<Plan?> Generate(string input, int maxSteps = 10);
}