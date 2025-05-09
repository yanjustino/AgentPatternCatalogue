using Agents.Common;
using Agents.Common.Interfaces;

namespace ProactiveGoalCreator;

public class ScreenContextDetector: IContextDetector
{
    public ContextData Capture()
    {
        var context = new ContextData();

        // Simulação de leitura da tela
        var activeWindow = GetActiveWindowTitle();
        var visibleElements = GetVisibleUiElements();

        context.Data["active_window"] = activeWindow;
        context.Data["visible_ui_elements"] = string.Join(", ", visibleElements);
        context.Data["screenshot_timestamp"] = DateTime.UtcNow.ToString("o");

        return context;
    }

    private static string GetActiveWindowTitle() => "Code Editor - Visual Studio";

    private static string[] GetVisibleUiElements() => ["RunButton", "SearchBar", "FileMenu"];
}