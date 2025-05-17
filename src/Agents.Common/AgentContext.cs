using Agents.Common.Interfaces;
using Agents.Common.Storage;

namespace Agents.Common;

public record AgentContext
{
    public IUserInterface UserInterface { get; } = new UserInterface();
    public IMemoryStore MemoryStore { get; init; } = MemoryFactory.CreateDefaultMemory();
    
    public static AgentContext Default() => new ();
}