using Agents.Common;
using Agents.Common.Interfaces;
using Agents.Common.Results;
using Agents.Common.Storage;

namespace IncrementalModelQuerying;

/// <summary>
/// The GoalCreator class is responsible for creating and modifying a Goal object
/// by merging its contextual data with additional data from an IMemoryStore implementation.
/// </summary>
public class Creator(AgentContext context, IPlanGeneration planner): IGoalCreatorAsync
{
    public AgentContext Context { get; } = context;
    private IUserInterface Gui => Context.UserInterface;

    /// <summary>
    /// Generates a new Goal object by retrieving user input through the DialogueInterface
    /// and combining its contextual data with additional data retrieved from the memory store.
    /// </summary>
    /// <returns>A newly created Goal object with merged contextual information, or null if no user input is provided.</returns>
    public async Task<Goal?> GenerateGoalAsync()
    {
        var prompt = Gui.GetUserPrompt();
        if (prompt is null) return null;

        var before = "";

        while (true)
        {
            var intent =
                $"""
                {prompt.Intent};
                Previous task:
                {before}
                """;
            
            var plan = await planner.Generate(intent);
            
            if (plan is null)
            {
                Gui.Notify("No plan generated.");
                break;
            }

            Gui.Notify($"[plan]: {plan}");
            
            var newAction = Context.UserInterface.GetUserPrompt("Do you want to continue? (yes/no)");
            if (newAction?.Intent?.Equals("no", StringComparison.InvariantCultureIgnoreCase) ?? true) break;

            before += $"\n - {plan.Subtasks.FirstOrDefault()}";
        }

        return null;
    }
}