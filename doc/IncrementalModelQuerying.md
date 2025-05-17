# Incremental Model Querying

**Summary**  
The **Incremental Model Querying** pattern describes an iterative process where the agent interacts with the foundation model multiple times throughout plan generation. At each step, new prompts and partial context are used to refine the reasoning and build a more complete, explainable plan.

## Context
When users provide a goal to the agent, the foundation model may struggle to return a correct or complete plan in a single query. The reasoning process may require multiple intermediate steps, context updates, or refinements.

## Problem
How can the agent perform accurate and explainable reasoning when a one-shot query is insufficient for generating a coherent plan?

## Forces
- **Limited context window** – Token constraints make it difficult to include all required information in one prompt.
- **Oversimplification** – Single-shot queries may miss nuance and interdependencies.
- **Explainability** – Transparent, step-by-step plans build user trust and facilitate debugging.

## Solution
The agent uses a multi-step process, querying the foundation model at each stage of plan generation. Intermediate results can be validated, adjusted, or expanded using user feedback, memory, or tool outputs. The number of queries may be predefined or dynamic, and the process may follow reusable templates or workflow repositories.

## Consequences

### Benefits
- **Supplementary context** – Tasks can be split across multiple prompts, solving the context window problem.
- **Improved reasoning certainty** – Iterative refinement increases the accuracy of results.
- **Explainability** – Each step can include justifications, making the plan easier to understand.

### Drawbacks
- **Overhead** – Multiple queries increase latency and computational cost.
- **Cost** – High interaction volume can become expensive when using commercial models.

## Known Uses
- **HuggingGPT** – Decomposes user requests into sub-tasks via multiple model queries.
- **EcoAssistant** – Iteratively refines code using LLM-driven feedback loops.
- **ReWOO** – Plans and executes interdependent tasks using tool-assisted observations.

## Related Patterns
- **One-Shot Model Querying** – Direct alternative for simple tasks with one-time querying.
- **Multi-Path Plan Generator** – Iteratively generates branched plans with user input.
- **Self-Reflection** – Queries the model multiple times to review and refine outputs.
- **Human-Reflection** – Enables collaborative iteration between user and agent.
- **Multimodal Guardrails** – Acts as an intermediary for safe and structured model interactions.

## References
[37] Shen et al., “HuggingGPT,” 2023.  
[38] Li et al., “EcoAssistant,” 2023.  
[39] Xu et al., “ReWOO: Reasoning with Workflow and Observation,” 2023.