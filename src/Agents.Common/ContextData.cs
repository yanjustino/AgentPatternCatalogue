using System.Text;

namespace Agents.Common;

/// <summary>
/// Repre
/// </summary>
public class ContextData
{
    public Dictionary<string, string> Data { get; } = new();

    public void Merge(ContextData? other)
    {
        if (other == null) return;
        foreach (var kv in other.Data)
            Data[kv.Key] = kv.Value;
    }

    public static ContextData MergeAll(params ContextData[] contexts)
    {
        var merged = new ContextData();
        foreach (var ctx in contexts) merged.Merge(ctx);
        return merged;
    }

    public static ContextData MergeAll(params (string key, string value)[] contexts)
    {
        var merged = new ContextData();
        foreach (var ctx in contexts)
            merged.Data[ctx.key] = ctx.value;
        return merged;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var kv in Data)
            sb.AppendLine($"{kv.Key}:{kv.Value};");
        return sb.ToString();
    }
}