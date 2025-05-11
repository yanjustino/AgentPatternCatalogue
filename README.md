# 🧠 Agent Patterns in C#

Welcome to the **Agent Patterns Project** — a hands-on, developer-friendly exploration of agent design patterns for foundation model-based systems.

This repository brings to life selected architectural patterns from the paper  
**[Agent Design Pattern Catalogue: A Collection of Architectural Patterns for Foundation Model Based Agents](https://arxiv.org/abs/2405.10467v3)** by Yue Liu et al. (CSIRO Data61).

🚀 **Goal**: Implement and experiment with agent behavior, memory handling, and goal inference using C# and local LLMs like LLaMA.  
🧪 **Why?** This is a research-driven initiative to learn and explore how these patterns work in practice — not just theory.

---

## ✅ Implemented Patterns

- 📄 [PassiveGoalCreator](doc/PassiveGoalCreator.md) is the first pattern implemented in this project. It extracts user goals from natural language input by using contextual memory and a local LLaMA model.
- 📄 [ProactiveGoalCreator](doc/ProactiveGoalCreator.md) anticipates users’ goals by analysing human interactions and proactively capturing multimodal context through appropriate detectors, thereby enriching goal descriptions and improving accessibility.
- 📄 [PromptResponseOptimiser](doc/PromptResponseOptimiser.md) is a pattern designed to enhance the interaction between agents and large language models (LLMs). It refines goals and contextual information into optimized prompts, ensuring that the LLM produces accurate, relevant, and goal-aligned responses. 
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
   ollama pull phi3:mini
   ```

---

## ⚡ Quick Start

Clone the repo, restore dependencies, and run the agent:

```bash
# Run the PassiveGoalCreator agent
dotnet run --project agent-patterns/src/PassiveGoalCreator/PassiveGoalCreator.csproj
```

✅ Make sure the Ollama server is running before launching the agent.

---

## 🔭 Future Work

New patterns from the paper will be added iteratively.  
Stay tuned — this project is evolving with each experiment and pull request!