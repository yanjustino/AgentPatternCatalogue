namespace Agents.Common.Interfaces;

public interface IDialogueInterface
{
    public (bool exit, Goal goal) GetUserPrompt();
    public void Notify(string message);
}