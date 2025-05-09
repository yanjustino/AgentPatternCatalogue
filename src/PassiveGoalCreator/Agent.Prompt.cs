using Agents.Common;

namespace PassiveGoalCreator;

public partial class Agent
{
    private readonly LLm _llm = new("phi3:mini");

    /// <summary>
    /// Creates a structured prompt based on the provided goal, including intent, context, and instructions.
    /// </summary>
    /// <param name="prompt">The input goal containing intent and associated context data.</param>
    /// <returns>A formatted string representing the generated prompt including input, context, and output structure.</returns>
    private string CreatePrompt(Goal prompt)
    {
        var goal = goalCreator.GenerateGoal(prompt);

        return $"""
                <input>
                    {prompt.Intent}
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
    }

    /// <summary>
    /// Executes the given prompt by querying the local language model and processing the response.
    /// </summary>
    /// <param name="prompt">The input string containing the query to be processed by the language model.</param>
    /// <returns>A task representing the asynchronous operation of executing the prompt and outputting the result.</returns>
    private async Task ExecutePrompt(string prompt)
    {
        Console.WriteLine("[PassiveGoalCreator] Querying local LLaMA...");

        var result = await _llm.SendMessage(prompt);
        var text = result?.Trim() ?? "[no response]";

        Console.WriteLine($"[Identified Goal] {text}");
    }   
}