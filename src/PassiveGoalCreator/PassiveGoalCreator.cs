using AgentCore;

namespace PassiveGoalCreator;

using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class PassiveGoalCreator
{
    private readonly ILocalChat _localChat = new LocalChat("phi3:mini");
    public async Task<string> GenerateGoalAsync(string? userInput, string memoryContext)
    {
        var prompt = $"""
                      Input: 
                        - {userInput}
                      Context:
                        {memoryContext}
                      Instructions:
                        - Select the 'label' from the 'Context'.
                        - The 'label' should be the most relevant to the 'Input'.
                        - The 'label' should be the same as the 'label' in the 'Context'.
                        - Do not include any other text just the 'label'.
                        - Pay attention to Output format.
                      Output:
                        - <label>label</label>
                      """;

        var result = await _localChat.SendMessage(prompt);
        return result?.Trim() ?? "[no response]";
    }
}