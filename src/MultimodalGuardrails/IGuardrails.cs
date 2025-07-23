using MultimodalGuardrails.Guardrails;

namespace MultimodalGuardrails;

public interface IGuardrails
{
    Task<GuardrailsResult> ApplyAsync(string input);
}