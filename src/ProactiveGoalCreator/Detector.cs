using Agents.Common;
using Agents.Common.Interfaces;
using Agents.Common.Storage;

namespace ProactiveGoalCreator;

/// <summary>
/// Detects and captures the current screen context including active window information, visible user interface elements, and a timestamp.
/// Implements the <see cref="IContextDetector"/> interface.
/// </summary>
public class Detector: IContextDetector
{
    /// <summary>
    /// Captures the current screen context, including active window information, visible UI elements, and a timestamp.
    /// </summary>
    /// <returns>
    /// A <see cref="ContextData"/> object containing captured screen context data like active window title, visible UI elements,
    /// and screenshot timestamp.
    /// </returns>
    public ContextData Capture()
    {
        var context = new ContextData();

        // Simulação de leitura da tela
        var availability = GetSystemAvailability();
        var status = GetSystemStatus();

        context.Data["ListarSistemasDisponiveis"] = string.Join(", ", availability);
        context.Data["StatusDosSistemas"] = string.Join(", ", status);

        return context;
    }

    /// <summary>
    /// Retrieves the identifiers of currently visible user interface elements on the screen.
    /// </summary>
    /// <returns>
    /// An array of strings, where each string represents the identifier of a visible UI element.
    /// </returns>
    private static string[] GetSystemAvailability() => ["Os **sistemas** **disponíveis** são:", "C3PO", "R2D2", "BB8"];
    private static string[] GetSystemStatus() => ["**Status** de operação dos sistemas:", "C3PO - Online", "R2D2 - Offline", "BB8 - Online"];
}