namespace MultimodalGuardrails;

public record GuardrailsResult(string Label, string Result, double Score, string? Category = null)
{
    public bool IsSafe => Result.Equals("safe", StringComparison.OrdinalIgnoreCase);
    public bool IsUnsafe => !IsSafe;
    
    public static GuardrailsResult GetDafault(string label) => new(label, "none", 0, "none");
}
