using Agents.Common.Models;
using VotingBasedCooperation;
using VotingBasedCooperation.Agents;

Console.WriteLine("=== Voting-based Cooperation ===");

var gemini = Gemini.Create();
var msphi4 = Ollama.Create(model: "phi4-mini");

var agentG = new Worker("gemini", gemini);
var agentP = new Worker("msphi4", msphi4);
var person = new User("human");

var coordinator = new Coordinator(gemini, person, [agentG, agentP]);
var result = await coordinator.ExecuteAsync("US001", UserStories.U001);

Console.WriteLine(result);
Console.Read();