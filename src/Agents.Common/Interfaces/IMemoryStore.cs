namespace Agents.Common.Interfaces;

public interface IMemoryStore
{
    /// <summary>
    ///     Retrieves the context data from the memory store.
    /// </summary>
    /// <returns>The context data.</returns>
    ContextData RetrieveContext();

    /// <summary>
    ///     Stores the context data in the memory store.
    /// </summary>
    /// <param name="context">The context data to store.</param>
    void StoreContext(ContextData context);
}