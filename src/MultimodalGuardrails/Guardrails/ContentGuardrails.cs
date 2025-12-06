using Agents.Common.Models;

namespace MultimodalGuardrails.Guardrails;

/// <summary>
/// Faz uso de guardrails para filtrar conteúdo sensível ou inadequado.
/// llama-guard3
/// <see href="https://ollama.com/library/llama-guard3"/>
/// </summary>
public class ContentGuardrails: IGuardrails
{
    private const string GUARDRAILS_NAME = nameof(ContentGuardrails);

    public async Task<GuardrailsResult> ApplyAsync(string input)
    {
        var response = await CallGuardrails(input);

        return string.IsNullOrEmpty(response) 
            ? GuardrailsResult.GetDafault(GUARDRAILS_NAME) 
            : GetGuardrailsResult(response);
    }

    private static async Task<string?> CallGuardrails(string input)
    {
        var client = NewOllama.Create(model:"llama-guard3:1b");
        return await client.SendMessage(input);
    }
    
    private GuardrailsResult GetGuardrailsResult(string response)
    {
        var result = response.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        
        var statement = result.Length > 0 ? result[0].Trim() : "none";
        var category = result.Length > 1 ? result[1].Trim() : "none";
        
        return new GuardrailsResult(GUARDRAILS_NAME, statement, 0, category);
    }
}