using Agents.Common.Storage;

namespace Rag.Core.Interfaces;

/// <summary>
/// Defines the contract for a retriever that retrieves context data based on a given task.
/// </summary>
public interface IRetriever
{
    Task<ContextData> Retrieve(string task);
}