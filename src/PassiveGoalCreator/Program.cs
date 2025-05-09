using Agents.Common;
using PassiveGoalCreator;

Console.WriteLine("=== Passive Goal Creator Agent (LLaMA + CLI) ===");

var dialog = new DialogueInterface();
var memory = MemoryFactory.CreateMemory();
var creator = new GoalCreator(memory);

var agent = new Agent(dialog, creator);
await agent.RunAsync();