namespace Agents.Common.Models;

public record Plan
{
    public string? TaskDescription { get; set; }
    public List<string> Subtasks { get; set; } = new();
}