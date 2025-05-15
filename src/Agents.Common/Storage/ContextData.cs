using System.Text;

namespace Agents.Common.Storage;

/// <summary>
/// Represents a collection of context data.
/// </summary>
public class ContextData
{
    public Dictionary<string, string> Data { get; } = new();

    /// <summary>
    /// Merges the current context data with the provided context data.
    /// </summary>
    /// <param name="other">The other context data to be merged.</param>
    public void Merge(ContextData? other)
    {
        if (other == null) return;
        foreach (var kv in other.Data)
            Data[kv.Key] = kv.Value;
    }

    /// <summary>
    /// Merges multiple ContextData instances into a single ContextData instance.
    /// </summary>
    /// <param name="contexts">An array of ContextData instances to be merged.</param>
    /// <returns>A new ContextData instance containing the combined data from all provided contexts.</returns>
    public static ContextData MergeAll(params ContextData[] contexts)
    {
        var merged = new ContextData();
        foreach (var ctx in contexts) merged.Merge(ctx);
        return merged;
    }

    /// <summary>
    /// Merges multiple context data instances into a single context data instance.
    /// </summary>
    /// <param name="contexts">An array of key-value pairs representing context data to be merged.</param>
    /// <returns>A merged instance of <see cref="ContextData"/> containing all provided context data.</returns>
    public static ContextData MergeAll(params (string key, string value)[] contexts)
    {
        var merged = new ContextData();
        foreach (var ctx in contexts)
            merged.Data[ctx.key] = ctx.value;
        return merged;
    }

    /// <summary>
    /// Returns a string representation of the context data.
    /// </summary>
    /// <returns>A string containing key-value pairs of the context data, formatted as "Key:Value;" on separate lines.</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var kv in Data)
            sb.AppendLine($"{kv.Key}:{kv.Value};");
        return sb.ToString();
    }
}