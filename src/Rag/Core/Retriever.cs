using Agents.Common.Storage;
using Qdrant.Client;
using Rag.Core.Interfaces;

namespace Rag.Core;

/// <summary>
/// The Retriever class provides functionality to retrieve context data
/// from an external source based on a given task. It utilizes an
/// embedding mechanism to process queries and retrieve relevant results
/// from a specified collection.
/// </summary>
/// <remarks>
/// This class implements the IRetriever interface and works in conjunction
/// with an embedding generator for creating vector-based representations
/// to perform similarity searches in a collection.
/// </remarks>
/// <param name="url">The URL of the target retrieval service or database.</param>
/// <param name="collection">The name of the data collection to query.</param>
/// <param name="embedder">The embedding generator to create vector representations of tasks.</param>
public partial class Retriever(string url, string collection, IEmbedding embedder) : IRetriever
{
    private readonly QdrantClient _client = new (url, 6334);

    /// <summary>
    /// Retrieves relevant context data based on the provided task.
    /// Uses the embedding generator to create a vector representation
    /// of the task and performs a similarity search within the specified collection.
    /// </summary>
    /// <param name="task">The task or query input for which context data is to be retrieved.</param>
    /// <returns>
    /// A <see cref="ContextData"/> object containing the merged results of the retrieved data
    /// from the specified collection.
    /// </returns>
    public async Task<ContextData> Retrieve(string task)
    {
        var searchResult = await _client.QueryAsync(
            collectionName: collection,
            query: embedder.Generate(task),
            limit: 3
        );

        var result = string.Join("\n",searchResult.Select(p => p.Payload.ToString()));
        
        return ContextData.MergeAll(("retrieved", result));
    }
}