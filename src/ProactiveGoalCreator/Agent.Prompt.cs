using Agents.Common;
using Agents.Common.Interfaces;

namespace ProactiveGoalCreator;

public partial class Agent
{
    private readonly LLm _llm = new("phi3:mini");

    /// <summary>
    /// Generates a structured prompt based on the provided goal details.
    /// </summary>
    /// <param name="prompt">The goal object containing the intent and context needed to form the prompt.</param>
    /// <returns>A formatted string prompt containing input, context, instructions, and output sections.</returns>
    private string CreatePrompt(Goal prompt)
    {
        var goal = creator.GenerateGoal(prompt, detectors);

        return $"""
                <input>
                {prompt.Intent}
                </input>
                <context>
                {goal.Context}
                </context>
                <instructions>
                    - Select the 'label' from the <context> in 'tools' section.
                    - The 'label' should be the most relevant to the <input>.
                    - The 'label' should be the same as the 'label' in the <context>.
                    - Do not include any other text just the 'label'.
                    - Pay attention to <output> format.
                </instructions>
                <output>
                    - [label]
                </output>
                """;
    }

    /// <summary>
    /// Sends the specified prompt to the LLm model for processing and outputs the result.
    /// </summary>
    /// <param name="prompt">The input string containing instructions or queries to be processed by the LLm model.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task ExecutePrompt(string prompt)
    {
        Console.WriteLine("[PassiveGoalCreator] Querying local LLaMA...");

        var result = await _llm.SendMessage(prompt);
        var text = result?.Trim() ?? "[no response]";

        Console.WriteLine($"[Identified Goal] {text}");
    }   
}