using System.Text.Json;
using Agents.Common.Interfaces;
using Agents.Common.Storage;

namespace VotingBasedCooperation;

public class Coordinator(IFoundationModel llm, IVoter human, params IVoter[] voters)
{
    public async Task<string> ValidateVotesAsync(string storeId, ContextData stories)
    {
        // Initialize the agent context
        var humanResult = await human.VoteAsync(storeId, stories);
        
        var tasks = voters.Select(voter => voter.VoteAsync(storeId, stories)).ToList();

        var results = await Task.WhenAll(tasks);
        
        var votes = new List<string> { humanResult };

        foreach (var result in results)
        {
            if (!string.IsNullOrEmpty(result))
            {
                votes.Add(result);
            }
        }
        
        var prompt = SystemPrompt.Replace("{{votes}}", JsonSerializer.Serialize(votes));
        return await ExecutePrompt(prompt) ?? "Agent did not return a valid response.";
        
    }
    
    private async Task<string?> ExecutePrompt(string? prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
        {
            throw new ArgumentException("Prompt cannot be null or whitespace.", nameof(prompt));
        }

        return await llm.SendMessage(prompt);
    }    

    private string SystemPrompt =>
        """
        You are a coordinator agent responsible for aggregating and analyzing complexity estimation votes from multiple voting agents.
        
        <context>
        You will receive a list of votes, each in JSON format, containing:
        - agent_id: identifier of the voting agent
        - user_story_id: the ID of the estimated user story
        - story_point: the Fibonacci score given by the agent
        - justification: the technical reasoning for the score
        </context>
        
        <instructions>
        1. Parse and validate all votes (ensure JSON structure is correct and story_point is within [1, 2, 3, 5, 8, 13]).
        2. Construct a summary containing the following columns:
        - Agent ID
        - User Story ID
        - Story Point
        - Justification
        3. Ensure the result is clear, consistent, and readable in Markdown format.
        4. Do not suggest a final estimation or perform statistical analysis. Only report the individual votes.
        </instructions>
        
        <input>
        {{votes}}
        </input>
        
        <output>
        ## Agent ID: {{agentName}}
        ### User Story ID: {{userStoryId}}
        - **Story Point**: {{storyPoint}}
        - **Justification**: {{justification}}
        ---
        [next vote]
        </output>
        
        <restrictions>
        - Do not perform story point averaging or consensus computation.
        - Do not modify agent responses.
        - Do not add your own interpretation.
        - Output only the table after validation
        </restrictions>
        """;
}