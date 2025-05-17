using Agents.Common.Interfaces;
using Agents.Common.Results;

namespace OneShotModelQueryng;

public class PlanGeneration(IFoundationModel llm) : IPlanGeneration
{
    /// <summary>
    /// Generates a detailed plan consisting of a main task and subtasks based on a given input.
    /// </summary>
    /// <param name="input">The main request or description that needs to be broken down into a task and subtasks.</param>
    /// <param name="maxSteps">The maximum number of subtasks to include in the plan. Defaults to 10 if not provided.</param>
    /// <returns>
    /// A <see cref="Plan"/> object containing the task description and subtasks, or null if the input could not be
    /// processed into a valid plan.
    /// </returns>
    public async Task<Plan?> Generate(string input, int maxSteps = 10)
    {
        var prompt =
            $"""
             <context>
               You are a planning assistant. Break the following request into a task and subtasks.
             </context>  
             <request>
               "{input}"
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

        var result = await llm.SendMessage(prompt);
        var plan = new Plan();

        foreach (var line in result?.Split('\n') ?? [])
        {
            if (line.Trim().StartsWith("Task:"))
                plan.TaskDescription = line["Task:".Length..].Trim();
            else if (line.Trim().StartsWith("- "))
                plan.Subtasks.Add(line["- ".Length..].Trim());
        }

        return string.IsNullOrWhiteSpace(plan.TaskDescription) ? null : plan;
    }
}