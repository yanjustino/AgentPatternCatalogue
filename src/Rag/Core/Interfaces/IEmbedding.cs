namespace Rag.Core.Interfaces;

/// <summary>
/// Defines the contract for generating embeddings from text input.
/// </summary>
public interface IEmbedding
{
    float[] Generate(string text);
}