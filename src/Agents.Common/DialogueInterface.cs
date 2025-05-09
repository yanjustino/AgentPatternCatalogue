using Agents.Common.Interfaces;

namespace Agents.Common;

public class DialogueInterface : IDialogueInterface
{
    public (bool exit, Goal goal) GetUserPrompt()
    {
        Console.Write("\n> Enter your prompt (or type 'exit'): ");
        var input = Console.ReadLine();
        return IsInvalidValidInput(input) ? (true, new Goal(input, null)) : (false, new Goal(input, null));
    }

    private static bool IsInvalidValidInput(string? input)
        => string.IsNullOrWhiteSpace(input) || input.Equals("exit", StringComparison.CurrentCultureIgnoreCase);
    
    public void Notify(string message)
    {
        Console.WriteLine($"\n[Agent] {message}");
    }
}