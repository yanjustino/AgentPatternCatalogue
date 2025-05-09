// See https://aka.ms/new-console-template for more information

using Agents.Common;
using Agents.Common.Interfaces;
using ProactiveGoalCreator;

Console.WriteLine("=== Proactive Goal Creator Agent (LLaMA + CLI) ===");

var dialog = new DialogueInterface();
var memory = MemoryFactory.CreateMemory();
var creator = new GoalCreator(dialog, memory);
IEnumerable<IContextDetector> detectors = new List<IContextDetector>() { new ScreenContextDetector() };

var agent = new Agent(dialog, creator, detectors);
await agent.RunAsync();