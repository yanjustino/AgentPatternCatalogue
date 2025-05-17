using Agents.Common.Results;

namespace Agents.Common.Interfaces;

/// <summary>
/// Represents a dialogue interface between a user and the system, allowing for user input and system notifications.
/// </summary>
public interface IUserInterface
{
    public Goal? GetUserPrompt(string? text = null);
    public void Notify(string message);
}