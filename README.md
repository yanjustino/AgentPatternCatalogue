# Agent Patterns Project

This project is an implementation of the design patterns proposed in the paper [Agent Design Pattern Catalogue: A Collection of Architectural Patterns for Foundation Model based Agents](https://arxiv.org/abs/2405.10467v3). The goal is to explore and implement these patterns to build robust and reusable agent-based systems.

## Implemented Patterns

### PassiveGoalCreator
The `PassiveGoalCreator` pattern has been implemented as the first step in this project. Its purpose is to generate goals based on user input and contextual memory. For more details, refer to the documentation available in the file [PassiveGoalCreator](doc/PassiveGoalCreator.md).

## Dependencies

### Ollama
This project relies on the [Ollama](https://ollama.com/) platform for running and interacting with foundation models. Follow the steps below to install and configure Ollama:

1. Download and install Ollama from [https://ollama.com/download](https://ollama.com/download).
2. Ensure the Ollama server is running locally. By default, it listens on `http://localhost:11434/`.
3. Verify the installation by running:
   ```bash
   ollama list
   ```
   This command should display the available models.

4. Optionally, download the required model (e.g., `llama3`) using:
   ```bash
   ollama pull llama3
   ```

## How to Run

To execute the `PassiveGoalCreator` agent, use the following command:

```bash
dotnet run --project agent-patterns/src/PassiveGoalCreator/PassiveGoalCreator.csproj
```

Ensure that the Ollama server is running before starting the application.

## Future Work

This README will be updated as more patterns from the paper are implemented. Stay tuned for further developments!