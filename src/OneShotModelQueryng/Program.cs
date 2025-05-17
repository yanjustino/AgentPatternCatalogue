// See https://aka.ms/new-console-template for more information

using Agents.Common;
using Agents.Common.Models;
using OneShotModelQueryng;

Console.WriteLine("=== One Shot Model Queryng Agent (GEMINI) ===");

// CREATE AGENT
//var llm = Ollama.Create("http://localhost:11434", "phi4-mini", false);
var key = Environment.GetEnvironmentVariable("API_KEY") ?? "not-found";
var llm = Gemini.Create(key);
var planner = new PlanGeneration(llm);
var context = AgentContext.Default();
var creator = new Creator(context, planner);

// AGENT
var agentAi = new Agent(creator);
await agentAi.RunAsync();

Console.WriteLine("\n[Agent] Shutting down.");