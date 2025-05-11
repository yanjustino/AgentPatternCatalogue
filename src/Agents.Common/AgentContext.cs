using Agents.Common.Interfaces;

namespace Agents.Common;

public record AgentContext
{
    public IAgentGui AgentGui { get; init; } = new AgentGui();
    public IMemoryStore MemoryStore { get; init; } = MemoryFactory.CreateDefaultMemory();
    
    public static AgentContext Default() => new ();
}