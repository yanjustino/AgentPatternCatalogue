namespace Agents.Common.Interfaces;

/// <summary>
/// Defines the contract for interacting with a language model interface.
/// Provides functionality to send a prompt to the language model and receive a response.
/// </summary>
public interface ILLm
{
    Task<string?> SendMessage(string prompt);
}