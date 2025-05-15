using Agents.Common.Interfaces;

namespace Agents.Common.Storage;

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
    private readonly List<ContextData> _contexts = new();

    /// <summary>
    /// Retrieves the merged contextual data stored in memory as a single <see cref="ContextData"/> instance.
    /// Combines all individual context entries into one overarching context object.
    /// </summary>
    /// <returns>A <see cref="ContextData"/> instance containing the combined data from all stored contexts.</returns>
    public ContextData RetrieveContext() => ContextData.MergeAll(_contexts.ToArray());

    /// <summary>
    /// Stores the provided contextual data into the internal memory store.
    /// Adds the given <see cref="ContextData"/> instance to the existing collection of contexts.
    /// </summary>
    /// <param name="context">The <see cref="ContextData"/> object containing contextual information to be stored.</param>
    public void StoreContext(ContextData context) => _contexts.Add(context);
}