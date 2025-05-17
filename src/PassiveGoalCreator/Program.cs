using Agents.Common;
using Agents.Common.Models;
using PassiveGoalCreator;

Console.WriteLine("=== Passive Goal Creator Agent (LLaMA + CLI) ===");

var context = AgentContext.Default();
var creator = new GoalCreator(context);
var prompts = new Optimiser();
var clients = Ollama.Create("http://localhost:11434", "phi4-mini");

var agentAi = new Agent(creator, clients, prompts);
await agentAi.RunAsync();

Console.WriteLine("\n[Agent] Shutting down.");