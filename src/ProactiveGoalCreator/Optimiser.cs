using Agents.Common;
using Agents.Common.Interfaces;
using Agents.Common.Results;

namespace ProactiveGoalCreator;

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
         Act as a text classification model. Given the <input> and <memory>, select the most relevant 'label' from the 
         <memory> that matches the <input>. Follow the instructions carefully to ensure accurate selection. Format your 
         response as specified in the <output> section.

         ## input
            {goal.Intent}

         ## memory
            {goal.Context}

         ## instructions
            1. Select the 'label' and 'value' from the <memory>.
               - Ensure that the 'label' you select is directly related to the <input>.
               - Avoid selecting 'labels' that are unrelated or only tangentially connected to the <input>.
            2. If multiple 'labels' seem relevant, choose the one that best encapsulates the
            3. If no relevant 'label' is found, respond with 'Unknown' as the 'label' and 'N/A' as the 'value'.
            4. Pay attention to <output> format.
            
         ## Attention   
            - Do not include any additional text or explanations in your response.
            - Strictly adhere to the specified <output> format.

         # output
         The output should be a concatenation of the selected 'label' and 'value' in the following format:
         <label> - <value>
         """;

    /// <summary>
    /// Optimizes the provided response by encapsulating it within a specified
    /// structured format for further processing or output generation.
    /// </summary>
    /// <param name="response">A string containing the response text to be optimized
    /// and formatted according to the defined structure.</param>
    /// <returns>A formatted string representing the optimized and encapsulated response.</returns>
    public string OptimiseResponse(string response) => $"<action>{response}</action>";
}