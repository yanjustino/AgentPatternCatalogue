using Agents.Common.Interfaces;

namespace Agents.Common;

/// <summary>
/// Represents a memory implementation that stores and retrieves contextual data.
/// Implements the <see cref="IMemoryStore"/> interface utilizing an internal list of contexts.
/// </summary>
public class MemoryStory : IMemoryStore
{
    /// <summary>
    /// A private field that represents the internal list of contextual data stored in memory.
    /// Used for managing, storing, and retrieving data relevant to context processing.
    /// </summary>
    private readonly List<AgentContextData> _contexts = new();

    /// <summary>
    /// Retrieves the merged contextual data stored in memory as a single <see cref="AgentContextData"/> instance.
    /// Combines all individual context entries into one overarching context object.
    /// </summary>
    /// <returns>A <see cref="AgentContextData"/> instance containing the combined data from all stored contexts.</returns>
    public AgentContextData RetrieveContext() => AgentContextData.MergeAll(_contexts.ToArray());

    /// <summary>
    /// Stores the provided contextual data into the internal memory store.
    /// Adds the given <see cref="AgentContextData"/> instance to the existing collection of contexts.
    /// </summary>
    /// <param name="agentContext">The <see cref="AgentContextData"/> object containing contextual information to be stored.</param>
    public void StoreContext(AgentContextData agentContext) => _contexts.Add(agentContext);
}