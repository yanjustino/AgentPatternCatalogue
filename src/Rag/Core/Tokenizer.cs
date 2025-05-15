using System.Text.RegularExpressions;
using Rag.Core.Interfaces;

namespace Rag.Core;

/// <summary>
/// The <c>Tokenizer</c> class is responsible for tokenizing input text
/// into a sequence of token IDs based on a predefined vocabulary.
/// </summary>
/// <remarks>
/// This class implements the <see cref="ITokenizer"/> interface, providing
/// functionality to encode text into token IDs. It utilizes a vocabulary loaded
/// from a specified path to map text tokens to unique integer identifiers.
/// Words that are not found in the vocabulary are tokenized into subwords,
/// and if no match is found, a default unknown token is used.
/// </remarks>
public class Tokenizer : ITokenizer
{
    private readonly Dictionary<string, int> _vocab;
    private const string UNK = "[UNK]";

    public Tokenizer(string vocabPath)
    {
        _vocab = File.ReadAllLines(vocabPath)
            .Select((tok, idx) => new { tok, idx })
            .ToDictionary(x => x.tok, x => x.idx);
    }

    /// <summary>
    /// Encodes the input text into a sequence of token IDs by tokenizing the text
    /// based on the vocabulary of the tokenizer.
    /// </summary>
    /// <param name="text">The input text to be tokenized and encoded.</param>
    /// <returns>An array of token IDs corresponding to the input text. If a word or subword
    /// is not found in the vocabulary, it is represented by the default unknown token ID.</returns>
    public long[] Encode(string text)
    {
        var tokens = new List<string>();
        foreach (var word in Regex.Split(text.ToLower(), @"\s+"))
        {
            if (_vocab.ContainsKey(word))
            {
                tokens.Add(word);
                continue;
            }

            var sub = new List<string>();
            var start = 0;
            while (start < word.Length)
            {
                var end = word.Length;
                string? piece = null;
                while (start < end)
                {
                    var part = word.Substring(start, end - start);
                    var candidate = (start > 0 ? "##" : "") + part;
                    if (_vocab.ContainsKey(candidate)) { piece = candidate; break; }
                    end--;
                }

                if (piece == null) { tokens.Add(UNK); break; }
                sub.Add(piece);
                start = end;
            }

            tokens.AddRange(sub);
        }

        return tokens.Select(t => (long)(_vocab.TryGetValue(t, out var value) ? value : _vocab[UNK])).ToArray();
    }
}