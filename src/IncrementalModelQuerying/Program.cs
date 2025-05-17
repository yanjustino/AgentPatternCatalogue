

using Agents.Common;
using Agents.Common.Models;
using IncrementalModelQuerying;

Console.WriteLine("=== Incremental Model Queryng Agent (LLaMA) ===");

// CREATE AGENT
//var llm = Ollama.Create("http://localhost:11434", "phi4-mini", false);
var key = Environment.GetEnvironmentVariable("API_KEY") ?? "not-found";
var llm = Gemini.Create(key);
var planner = new PlanGeneration(llm);
var context = AgentContext.Default();
var creator = new Creator(context, planner);

// AGENT
var agentAi = new Agent(creator, llm);
await agentAi.RunAsync();

Console.WriteLine("\n[Agent] Shutting down.");