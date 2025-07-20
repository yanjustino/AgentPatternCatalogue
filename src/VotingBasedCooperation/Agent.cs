using Agents.Common.Interfaces;
using Agents.Common.Storage;

namespace VotingBasedCooperation;

public interface IVoter
{
    string AgentId { get; }

    /// <summary>
    /// Runs the voting process for a given user story.
    /// </summary>
    /// <param name="storeId">The key identifying the user story.</param>
    /// <param name="stories">The user story data.</param>
    /// <returns>A task that represents the asynchronous operation, containing the voting result.</returns>
    Task<string> VoteAsync(string storeId, ContextData stories);
}    

public class Agent(string agentId, IFoundationModel llm): IVoter
{
    public string AgentId { get; } = agentId;
    
    public async Task<string> VoteAsync(string storeId, ContextData stories)
    {
        var prompt = SystemPrompt
            .Replace("{{agentName}}", AgentId)
            .Replace("{{user_story}}", stories.Data[storeId]);
        return await ExecutePrompt(prompt) ?? "Agent did not return a valid response.";
    }

    private async Task<string?> ExecutePrompt(string? prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
            throw new ArgumentException("Prompt cannot be null or whitespace.", nameof(prompt));

        return await llm.SendMessage(prompt);
    }

    private string SystemPrompt =>
        """
        You are a specialist agent responsible for analyzing a <user_story> and estimating its technical complexity using the Fibonacci scale (1, 2, 3, 5, 8, 13).
        Upon receiving a <user_story> from the coordinator agent, you must:

        <user_story>
        {{user_story}}
        </user_story>

        <instructions>
        1. Read and fully understand the description of the user story.
        2. Estimate the implementation complexity based on the following criteria:
           – Scope and size of the functionality  
           – Technical or requirement uncertainties  
           – External integrations or dependencies  
           – Impact on the architecture and required testing  
        3. Assign a story point value (1, 2, 3, 5, 8, or 13) according to the Fibonacci scale.
        4. Provide a technical justification for your chosen score.
        5. Return your vote in the standardized format below.
        </instructions>

        <template>
        {
          "agent_id": "{{agentName}}",
          "user_story_id": "US001",
          "story_point": 5,
          "justification": "The implementation involves a basic REST endpoint with simple validations and standard persistence using Entity Framework. The level of uncertainty is low, and the complexity lies in authentication handling and data validation."
        }
        </template>

        <restrictions>
        - Only use values from the Fibonacci sequence up to 13: [1, 2, 3, 5, 8, 13].
        - Justifications must be clear, technical, and concise.
        - Do not include any text other than the specified JSON in <template>.
        - Be consistent with your technical expertise and evaluate based on software engineering best practices.
        </restrictions>
        """;
}