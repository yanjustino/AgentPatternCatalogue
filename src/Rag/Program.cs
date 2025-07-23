// See https://aka.ms/new-console-template for more information

using Agents.Common;
using Agents.Common.Models;
using Rag;
using Rag.Core;

Console.WriteLine("=== Retrieval Augmented Generator Agent (LLaMA + ONNX) ===");

// EMBEDDING
// The ONNX model for the E5-small-v2 embedding model
// This model is used to convert text into embeddings for the Retriever
// https://huggingface.co/intfloat/e5-small-v2/blob/main/model.onnx
var model = Path.Combine(Environment.CurrentDirectory, "resource", "model.onnx");

// The tokenizer vocabulary file for the E5-small-v2 model
// This file contains the vocabulary used by the tokenizer to convert text into tokens
// https://huggingface.co/intfloat/e5-small-v2/blob/main/vocab.txt
var vocab = Path.Combine(Environment.CurrentDirectory, "resource", "vocab.txt");

// The sample text file to be used for seeding the Retriever
var sample = Path.Combine(Environment.CurrentDirectory, "resource", "sample.txt");

// CREATE EMBEDDING
// The Embedding class is used to create embeddings from text using the ONNX model and tokenizer
var embedding = new Embedding(model, new Tokenizer(vocab));

// CREATE RETRIEVE
var retriever = new Retriever("localhost", "docs", embedding);
await retriever.Seed(sample);

// CREATE AGENT
var llm = Ollama.Create("http://localhost:11434", "phi4-mini", false);
var planner = new PlanGeneration(llm);
var context = AgentContext.Default();
var creator = new GoalCreator(context, retriever, planner);

// AGENT
var agentAi = new Agent(creator, llm, new Optimiser());
await agentAi.RunAsync();

Console.WriteLine("\n[Agent] Shutting down.");