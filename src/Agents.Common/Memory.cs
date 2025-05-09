using Agents.Common.Interfaces;

namespace Agents.Common;

public class Memory : IMemoryStore
{
    private readonly List<ContextData> _contexts = new();

    public ContextData RetrieveContext() => ContextData.MergeAll(_contexts.ToArray());

    public void StoreContext(ContextData context) => _contexts.Add(context);
}