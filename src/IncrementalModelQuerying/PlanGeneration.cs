using Agents.Common.Interfaces;
using Agents.Common.Results;

namespace IncrementalModelQuerying;

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
               You are a planning assistant. Break the following request into a task and subtask.
             </context>  
             <request>
               "{input}"
             </request>
             <instruction>
                - The task should have a single subtask;
                - The length of subtask step should be less than 50 words;
                - The subtask should be a new content, based on the [previous task] in the context;
                - Don't use the same content as the [previous task];
             </instruction>
             <output>
               Task: [main task]
               Subtasks:
                 - [subtask]
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