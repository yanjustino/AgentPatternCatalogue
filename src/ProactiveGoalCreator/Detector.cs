using Agents.Common;
using Agents.Common.Interfaces;

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
    /// A <see cref="AgentContextData"/> object containing captured screen context data like active window title, visible UI elements,
    /// and screenshot timestamp.
    /// </returns>
    public AgentContextData Capture()
    {
        var context = new AgentContextData();

        // Simulação de leitura da tela
        var activeWindow = GetActiveWindowTitle();
        var visibleElements = GetVisibleUiElements();

        context.Data["active_window"] = activeWindow;
        context.Data["visible_ui_elements"] = string.Join(", ", visibleElements);
        context.Data["screenshot_timestamp"] = DateTime.UtcNow.ToString("o");

        return context;
    }

    /// <summary>
    /// Retrieves the title of the currently active window on the screen.
    /// </summary>
    /// <returns>
    /// A string representing the title of the active window.
    /// </returns>
    private static string GetActiveWindowTitle() => "Code Editor - Visual Studio";

    /// <summary>
    /// Retrieves the identifiers of currently visible user interface elements on the screen.
    /// </summary>
    /// <returns>
    /// An array of strings, where each string represents the identifier of a visible UI element.
    /// </returns>
    private static string[] GetVisibleUiElements() => ["RunButton", "SearchBar", "FileMenu"];
}