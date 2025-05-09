namespace Agents.Common.Interfaces;

/// <summary>
/// Provides functionality to detect and capture context data.
/// </summary>
public interface IContextDetector
{
        ContextData Capture();
}