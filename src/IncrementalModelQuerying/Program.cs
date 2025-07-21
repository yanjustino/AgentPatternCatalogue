

using Agents.Common;
using Agents.Common.Models;
using IncrementalModelQuerying;

Console.WriteLine("=== Incremental Model Queryng Agent (LLaMA) ===");

// CREATE AGENT
//var llm = Ollama.Create("http://localhost:11434", "phi4-mini", false);
var llm = Gemini.Create();
var planner = new PlanGeneration(llm);
var context = AgentContext.Default();
var creator = new GoalCreator(context, planner);

// AGENT
var agentAi = new Agent(creator, llm);
await agentAi.RunAsync();

Console.WriteLine("\n[Agent] Shutting down.");