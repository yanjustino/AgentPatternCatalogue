namespace AgentCore;

public interface ILocalChat
{
    Task<string?> SendMessage(string prompt);
}