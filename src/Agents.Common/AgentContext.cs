using Agents.Common.Interfaces;
using Agents.Common.Storage;

namespace Agents.Common;

public record AgentContext
{
    public IAgentGui AgentGui { get; } = new AgentGui();
    public IMemoryStore MemoryStore { get; init; } = MemoryFactory.CreateDefaultMemory();
    
    public static AgentContext Default() => new ();
}