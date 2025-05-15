using Agents.Common.Models;

namespace Agents.Common.Interfaces;

/// <summary>
/// Defines the contract for interacting with a language model interface.
/// Provides functionality to send a prompt to the language model and receive a response.
/// </summary>
public interface ILLmClient
{
    Task<string?> SendMessage(string prompt);
    Task<Plan?> Generate(string input);
}