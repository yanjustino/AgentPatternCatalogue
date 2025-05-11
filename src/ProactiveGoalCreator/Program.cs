using Agents.Common;
using ProactiveGoalCreator;

Console.WriteLine("=== Proactive Goal Creator Agent (LLaMA + CLI) ===");

var model = Environment.GetEnvironmentVariable("LLM_MODEL") ?? "phi3:mini";
var url = Environment.GetEnvironmentVariable("LLM_URL") ?? "http://localhost:11434";

var context = AgentContext.Default();
var creator = new Creator(context, [ new Detector() ]);
var prompts = new Prompt();
var clients = AgentLLmClient.Create(url, model);

var agentAi = new Agent(creator, clients, prompts);
await agentAi.RunAsync();

Console.WriteLine("\n[Agent] Shutting down.");