// See https://aka.ms/new-console-template for more information

using Agents.Common;
using Agents.Common.Models;
using Rag;
using Rag.Core;

Console.WriteLine("=== Retrieval Augmented Generator Agent (LLaMA + ONNX) ===");

// EMBEDDING
// https://huggingface.co/intfloat/e5-small-v2/blob/main/model.onnx
var modelPath = Path.Combine(Environment.CurrentDirectory, "resource", "model.onnx");
// https://huggingface.co/intfloat/e5-small-v2/blob/main/vocab.txt
var vocabPath = Path.Combine(Environment.CurrentDirectory, "resource", "vocab.txt");
var samplPath = Path.Combine(Environment.CurrentDirectory, "resource", "sample.txt");
var embedding = new Embedding(modelPath, new Tokenizer(vocabPath));

// CREATE RETRIEVE
var retriever = new Retriever("localhost", "docs", embedding);
await retriever.Seed(samplPath);

// CREATE AGENT
var llm = Ollama.Create("http://localhost:11434", "phi4-mini", false);
var planner = new PlanGeneration(llm);
var context = AgentContext.Default();
var creator = new GoalCreator(context, retriever, planner);

// AGENT
var agentAi = new Agent(creator, llm, new Optimiser());
await agentAi.RunAsync();

Console.WriteLine("\n[Agent] Shutting down.");