using System.Text;
using Qdrant.Client;
using Qdrant.Client.Grpc;

namespace Rag.Core;

public partial class Retriever
{
    public const int EMBEDDING_DIM = 384; // ajuste ao modelo que você usa

    /// <summary>
    /// Reads the content of a specified file, processes the text into manageable chunks,
    /// generates embeddings for each chunk, and stores the results in the configured collection.
    /// </summary>
    /// <param name="filePath">The file path of the text file to be processed and seeded into the collection.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Seed(string filePath)
    {
        var fullText = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
        var chunks = ChunkByLength(fullText, maxChars: 2000).ToArray();
        var client = await TryCreateCollectionAsync();

        var points = new List<PointStruct>();
        for (var idx = 0; idx < chunks.Length; idx++)
        {
            var chunk = chunks[idx];

            var point = new PointStruct
            {
                Id = new PointId { Num = Convert.ToUInt64(idx) },
                Vectors = embedder.Generate(chunk)
            };
            point.Payload.Add("text", chunk);
            point.Payload.Add("source", Path.GetFileName(filePath));
            point.Payload.Add("chunk", idx);

            points.Add(point);
        }

        await client.UpsertAsync(collection, points);
        Console.WriteLine($"Ingerido {points.Count} chunks em '{collection}'.");
    }

    private async Task<QdrantClient> TryCreateCollectionAsync()
    {
        var client = new QdrantClient(url, 6334);

        if (await client.CollectionExistsAsync(collection)) return client;
        var config = new VectorParams { Size = EMBEDDING_DIM, Distance = Distance.Dot };
        await client.CreateCollectionAsync(collection, config);
        return client;
    }

    private static IEnumerable<string> ChunkByLength(string text, int maxChars)
    {
        for (var i = 0; i < text.Length; i += maxChars)
            yield return text.Substring(i, Math.Min(maxChars, text.Length - i));
    }
}