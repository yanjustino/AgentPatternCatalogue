using System.Text.Json;
using Agents.Common;
using Agents.Common.Interfaces;
using Agents.Common.Results;

namespace PassiveGoalCreator;

/// <summary>
/// Represents a prompt optimization class that generates a structured prompt
/// based on a provided goal using specific formatting and instructions.
/// Implements the IPromptOptimiser interface.
/// </summary>
public class Optimiser : IPromptOptimiser
{
    /// <summary>
    /// Generates a structured and optimized prompt based on the provided agent goal.
    /// The method organizes the goal data into an input, context, instructions,
    /// and output format to create a clearly defined prompt suitable for processing.
    /// </summary>
    /// <param name="goal">An instance of <see cref="Goal"/> that contains the intent
    /// and contextual information related to the specific goal.</param>
    /// <returns>A formatted string representing the optimized prompt.</returns>
    public string OptimisePrompt(Goal goal) =>
        $"""
         <input>
            {goal.Intent}
         </input>
         <context>
            {goal.Context}
         </context>
         <instructions>
             - Select the 'label' from the <context>.
             - The 'label' should be the most relevant to the <input>.
             - The 'label' should be the same as the 'label' in the <context>.
             - Do not include any other text just the 'label'.
             - Pay attention to <output> format.
         </instructions>
         <output>
             - [label]
         </output>
         """;

    /// <summary>
    /// Serializes the response into a JSON-formatted string, structuring the response
    /// for further processing or output.
    /// </summary>
    /// <param name="response">A string containing the response to be serialized into JSON format.</param>
    /// <returns>A JSON-formatted string representing the serialized version of the response.</returns>
    public string OptimiseResponse(string response) => JsonSerializer.Serialize(new { action = response });
}