using System.Text.Json;
using Agents.Common.Models;
using System.Collections.Generic;

namespace MultimodalGuardrails.Guardrails;

/// <summary>
/// Faz uso de guardrails para filtrar conteúdo sensível ou inadequado.
/// meta-llama/Llama-Prompt-Guard-2-86M
/// meta-llama/Prompt-Guard-86M
/// <see href="https://huggingface.co/meta-llama/Llama-Prompt-Guard-2-86M"/>
/// <see href="https://huggingface.co/meta-llama/Prompt-Guard-86M"/>
/// </summary>
public class PromptGuardrails: IGuardrails
{
    private const string GUARDRAILS_NAME = nameof(PromptGuardrails);
    
    private readonly Dictionary<string, string> _mapping = new()
    {
        { "LABEL_1", "unsafe" },
        { "LABEL_0", "safe" }
    };
    
    public async Task<GuardrailsResult> ApplyAsync(string input)
    {
        var json = await CallGuardrail(input);

        if (string.IsNullOrEmpty(json))
            return GuardrailsResult.GetDafault(GUARDRAILS_NAME);
        
        var result = PrepareResult(json);

        return result.MaxBy(x => x.Score) ?? GuardrailsResult.GetDafault(GUARDRAILS_NAME);
    }

    private static async Task<string?> CallGuardrail(string input)
    {
        var client = HuggingFace.Create("meta-llama/Llama-Prompt-Guard-2-86M");
        var json = await client.SendMessage(input);
        return json;
    }
    
    private IEnumerable<GuardrailsResult> PrepareResult(string json)
    {
        var doc = JsonDocument.Parse(json);
        var arr = doc.RootElement[0];
        
        foreach (var item in arr.EnumerateArray())
        {
            yield return GetGuardrailsResult(item);
        }
    }

    private GuardrailsResult GetGuardrailsResult(JsonElement item)
    {
        var raw = item.GetProperty("label").GetString() ?? "none";
        var label = _mapping[raw];
        var score = item.GetProperty("score").GetDouble();
        return new GuardrailsResult(GUARDRAILS_NAME, label, score);
    }
}