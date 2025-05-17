using Agents.Common.Storage;

namespace Agents.Common.Results;

/// <summary>
/// Represents a goal with an associated intent and an optional context.
/// </summary>
/// <param name="Intent">The main intent or objective of the goal.</param>
/// <param name="Context">The contextual data associated with the goal, if any.</param>
public record Goal(string? Intent, ContextData? Context);
