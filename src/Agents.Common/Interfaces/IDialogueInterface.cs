namespace Agents.Common.Interfaces;

/// <summary>
/// Represents a dialogue interface between a user and the system, allowing for user input and system notifications.
/// </summary>
public interface IDialogueInterface
{
    public (bool exit, Goal goal) GetUserPrompt();
    public void Notify(string message);
}