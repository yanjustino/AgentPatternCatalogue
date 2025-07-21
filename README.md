# 🧠 Agent Patterns in C#

Welcome to the **Agent Patterns Project** — a hands-on, developer-friendly exploration of agent design patterns for foundation model-based systems.

This repository brings to life selected architectural patterns from the paper  
**[Agent Design Pattern Catalogue: A Collection of Architectural Patterns for Foundation Model Based Agents](https://arxiv.org/abs/2405.10467v3)** by Yue Liu et al. (CSIRO Data61).

🚀 **Goal**: Implement and experiment with agent behavior, memory handling, and goal inference using C# and local LLMs like LLaMA.  
🧪 **Why?** This is a research-driven initiative to learn and explore how these patterns work in practice — not just theory.

---

## ✅ Implemented Patterns

- 📄 [Passive Goal Creator](doc/PassiveGoalCreator.md) is the first pattern implemented in this project. It extracts user goals from natural language input by using contextual memory and a local LLaMA model.
- 📄 [Proactive Goal Creator](doc/ProactiveGoalCreator.md) anticipates users’ goals by analysing human interactions and proactively capturing multimodal context through appropriate detectors, thereby enriching goal descriptions and improving accessibility.
- 📄 [Prompt Response Optimiser](doc/PromptResponseOptimiser.md) is a pattern designed to enhance the interaction between agents and large language models (LLMs). It refines goals and contextual information into optimized prompts, ensuring that the LLM produces accurate, relevant, and goal-aligned responses.
- 📄 [Retrieval Augmented Generation (RAG)](doc/RetrievalAugmentedGeneration.md) is a pattern that combines retrieval and generation techniques to enhance the performance of large language models (LLMs). It retrieves relevant information from a knowledge base and uses it to generate more accurate and contextually relevant responses.
- 📄 [One-Shot Model Querying](doc/OneShotModelQueryng.md) is a pattern that describes a direct interaction in which the agent queries a foundation model (LLM) only once to generate a complete plan based on a user’s input. This approach favors simplicity and efficiency, making it suitable for straightforward tasks that can be handled in a single reasoning step.
- 📄 [Incremental Model Querying](doc/IncrementalModelQuerying.md) is a pattern that describes an iterative process where the agent interacts with the foundation model multiple times throughout plan generation. At each step, new prompts and partial context are used to refine the reasoning and build a more complete, explainable plan.
- 📄 [Voting Based Cooperation](doc/VotingBasedCooperation.md) is a pattern that enables multiple agents to independently estimate the complexity of user stories by voting. A coordinator agent collects, validates, and aggregates these votes, presenting the results in a clear and structured format without performing consensus or statistical analysis.
---

## ⚙️ Dependencies

### Ollama
We use [Ollama](https://ollama.com/) to run foundation models locally.

1. [Download Ollama](https://ollama.com/download)
2. Ensure the server is running at `http://localhost:11434/`
3. Verify with:
   ```bash
   ollama list
   ```
4. Pull the phi3:mini model:
   ```bash
   ollama pull phi4-mini:latest
   ```

---

## ⚡ Quick Start

Clone the repo, restore dependencies, and run the agent:

```bash
# Run the PassiveGoalCreator agent
dotnet run --project agent-patterns/src/<<patter>>/<<pattern>>.csproj
```

✅ Make sure the Ollama server is running before launching the agent.

---

## 🔭 Future Work

New patterns from the paper will be added iteratively.  
Stay tuned — this project is evolving with each experiment and pull request!