using Agents.Common.Interfaces;

namespace MultimodalGuardrails;

public class Agent(IFoundationModel? llm = null)
{
    public async Task ExecuteAsync(string input, IGuardrails[] guardrails)
    {
        foreach (var guardrail in guardrails)
        {
            var result = await guardrail.ApplyAsync(input);
            
            var icon = result.IsUnsafe ? "🚫" : "✅ ";

            Console.WriteLine($"{icon} >> {input}");
            Console.ForegroundColor = result.IsUnsafe ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine("Guardrail.: {0}", result.Label);
            Console.WriteLine("Result....: {0}", result.Result.ToUpper());
            Console.WriteLine("Score.....: {0}", result.Score);
            if (!string.IsNullOrEmpty(result.Category))
            {
                Console.WriteLine("Category..: {0}", result.Category);
            }
            else
            {
                Console.WriteLine("Category..: No category provided");
            }
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}