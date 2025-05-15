namespace Rag.Core.Interfaces;

/// <summary>
/// Defines the contract for encoding a given text into a sequence of token IDs.
/// </summary>
public interface ITokenizer
{
    long[] Encode(string text);
}