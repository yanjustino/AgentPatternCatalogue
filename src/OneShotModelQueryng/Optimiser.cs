using Agents.Common.Interfaces;
using Agents.Common.Results;

namespace OneShotModelQueryng;

/// <summary>
/// Represents a prompt optimization class that generates a structured prompt
/// based on a provided goal using specific formatting and instructions.
/// Implements the IPromptOptimiser interface.
/// </summary>
public class Optimiser(int maxSteps) : IPromptOptimiser
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
         <context>
           You are a planning assistant. Break the following request into a task and subtasks.
         </context>  
         <request>
           "{goal.Context}"
         </request>
         <instruction>
           - The subtasks should contains no more than {maxSteps} steps;
           - The length of each step should be less than 30 words;
           - Each step should have a sequence number starting from 1;
           - Each step should be formatted as "- [sequence] [description]";
         </instruction>
         <output>
           Task: [main task]
           Subtasks:
             - [sequence] [description]
         </output>
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