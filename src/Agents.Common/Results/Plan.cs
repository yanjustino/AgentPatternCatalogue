using System.Text;

namespace Agents.Common.Results;

public record Plan
{
    public string TaskDescription { get; set; } = "";
    public List<string> Subtasks { get; set; } = new();

    public override string ToString()
    {
        var result = new StringBuilder();
        result.AppendLine(TaskDescription);
        result.AppendLine("[Subtasks]:");
        result.AppendJoin('\n', Subtasks.Select(s => $"  {s}"));
        return result.ToString();
    }
}