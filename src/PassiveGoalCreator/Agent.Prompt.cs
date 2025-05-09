using Agents.Common;

namespace PassiveGoalCreator;

public partial class Agent
{
    private readonly LLm _llm = new("phi3:mini");

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

    private async Task ExecutePrompt(string prompt)
    {
        Console.WriteLine("[PassiveGoalCreator] Querying local LLaMA...");

        var result = await _llm.SendMessage(prompt);
        var text = result?.Trim() ?? "[no response]";

        Console.WriteLine($"[Identified Goal] {text}");
    }   
}