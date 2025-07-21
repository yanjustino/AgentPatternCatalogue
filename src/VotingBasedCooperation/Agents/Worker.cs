using Agents.Common.Interfaces;
using Agents.Common.Storage;

namespace VotingBasedCooperation.Agents;

public class Worker(string id, IFoundationModel llm): Agent(id, llm)
{
    public override async Task<string> VoteAsync(string storeId, ContextData stories)
    {
        var prompt = SYSTEM_PROMPT
            .Replace("{{agentName}}", id)
            .Replace("{{user_story}}", stories.Data[storeId]);
        
        return await ExecutePrompt(prompt) ?? "Agent did not return a valid response.";
    }

    private const string SYSTEM_PROMPT =
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