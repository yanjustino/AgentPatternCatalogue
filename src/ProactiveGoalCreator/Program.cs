using Agents.Common;
using Agents.Common.Models;
using ProactiveGoalCreator;

Console.WriteLine("=== Proactive Goal Creator Agent (LLaMA + CLI) ===");

var context = AgentContext.Default();
var creator = new GoalCreator(context, [ new Detector() ]);
var prompts = new Optimiser();
var clients = Ollama.Create("http://localhost:11434", "phi4-mini");


var agentAi = new Agent(creator, clients, prompts);
await agentAi.RunAsync();

Console.WriteLine("\n[Agent] Shutting down.");