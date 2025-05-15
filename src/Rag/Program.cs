// See https://aka.ms/new-console-template for more information

using Agents.Common;
using Rag;
using Rag.Core;

Console.WriteLine("=== Retrieval Augmented Generator Agent (LLaMA + ONNX) ===");

// EMBEDDING
var modelPath = Path.Combine(Environment.CurrentDirectory, "resource", "model.onnx");
var vocabPath = Path.Combine(Environment.CurrentDirectory, "resource", "vocab.txt");
var samplPath = Path.Combine(Environment.CurrentDirectory, "resource", "sample.txt");
var embedding = new Embedding(modelPath, new Tokenizer(vocabPath));

// CREATE RETRIEVE
var retriever = new Retriever("localhost", "docs", embedding);
await retriever.Seed(samplPath);

// CREATE AGENT
var llm = LLmClient.Create("http://localhost:11434", "phi4-mini", false);
var context = AgentContext.Default();
var creator = new Creator(context, retriever, llm);

// AGENT
var agentAi = new Agent(creator, llm, new Optimiser());
await agentAi.RunAsync();

Console.WriteLine("\n[Agent] Shutting down.");