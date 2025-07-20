// See https://aka.ms/new-console-template for more information

using Agents.Common;
using Agents.Common.Models;
using VotingBasedCooperation;

Console.WriteLine("=== Voting-based Cooperation ===");

var key = Environment.GetEnvironmentVariable("API_KEY") ?? "not-found";

var gemini = Gemini.Create(key);
var phi4 = Ollama.Create("http://localhost:11434", "phi4-mini", false);

var agentG = new Agent("gemini_agent", gemini);
var agentP = new Agent("phi4_agent", phi4);
var person = new Human("Iam_a_human");

var coordinator = new Coordinator(gemini, person, agentG, agentP);
var voting = await coordinator.ValidateVotesAsync("US001", UserStoryFactory.CreateDefaultUserStory());

Console.WriteLine(voting);

Console.Read();