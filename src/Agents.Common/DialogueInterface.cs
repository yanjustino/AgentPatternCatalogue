using Agents.Common.Interfaces;

namespace Agents.Common;

/// <summary>
/// Represents a simple console-based dialogue interface.
/// </summary>
public class DialogueInterface : IDialogueInterface
{
    /// <summary>
    /// Prompts the user for input and generates a corresponding goal based on the input.
    /// </summary>
    /// <returns>
    /// A tuple containing a boolean indicating whether the user wishes to exit,
    /// and a Goal object encapsulating the user's input and an optional context.
    /// </returns>
    public (bool exit, Goal goal) GetUserPrompt()
    {
        Console.Write("\n> Enter your prompt (or type 'exit'): ");
        var input = Console.ReadLine();
        return IsInvalidValidInput(input) ? (true, new Goal(input, null)) : (false, new Goal(input, null));
    }

    /// <summary>
    /// Determines whether the provided input is invalid or considered valid input.
    /// </summary>
    /// <param name="input">The input string to validate.</param>
    /// <returns>
    /// True if the input is either null, whitespace, or equals "exit" (case insensitive).
    /// Otherwise, false.
    /// </returns>
    private static bool IsInvalidValidInput(string? input)
        => string.IsNullOrWhiteSpace(input) || input.Equals("exit", StringComparison.CurrentCultureIgnoreCase);

    /// <summary>
    /// Notifies the user with a message by displaying it in the console.
    /// </summary>
    /// <param name="message">The message to be displayed to the user.</param>
    public void Notify(string message)
        => Console.WriteLine($"\n[Agent] {message}");
}