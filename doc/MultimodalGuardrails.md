# MultimodalGuardrails Pattern

## Resumo Curto

**Multimodal Guardrails** controlam as entradas e saídas de modelos de fundação para atender a requisitos específicos, como necessidades dos usuários, padrões éticos e regulamentações legais.

## Visão Geral

O padrão **MultimodalGuardrails** define um framework para impor restrições e regras de segurança em múltiplas modalidades de dados (texto, imagens, áudio, vídeo). Ele garante que sistemas multimodais operem dentro de limites definidos, melhorando confiabilidade, segurança e conformidade.

![img.png](MultimodalGuardrails.png)

## Contexto de Uso

Este padrão é relevante em sistemas onde um agente consiste em um modelo de fundação e outros componentes. Quando os usuários fornecem objetivos específicos, o modelo de fundação subjacente é consultado para alcançá-los. Multimodal Guardrails atuam como um intermediário para garantir interações seguras e conformes.

## Declaração do Problema

Como podemos evitar que um modelo de fundação seja influenciado por entradas adversariais ou que gere saídas prejudiciais ou indesejáveis para usuários e outros componentes do sistema?

## Forças

- **Robustez**: Informações adversariais podem ser enviadas ao modelo de fundação, afetando sua memória, raciocínio e resultados subsequentes.
- **Segurança**: Modelos de fundação podem gerar respostas inadequadas devido a alucinações, o que pode ofender usuários ou prejudicar outros componentes.
- **Alinhamento a Padrões**: Agentes e modelos de fundação devem cumprir padrões da indústria, organizacionais e requisitos legais.

## Solução Proposta

Aplicar **guardrails** como uma camada intermediária entre o modelo de fundação e todos os outros componentes do sistema:

- Quando usuários ou componentes enviam prompts/mensagens ao modelo de fundação, os guardrails verificam primeiro se a informação atende aos requisitos predefinidos. Apenas informações válidas são entregues ao modelo (por exemplo, PII é tratada ou removida para proteger a privacidade).
- Guardrails podem avaliar conteúdo com base em exemplos predefinidos ou de maneira independente de referência.
- Quando o modelo de fundação gera saídas, os guardrails garantem que as respostas não incluam informações tendenciosas ou desrespeitosas e atendam aos requisitos específicos.
- Vários guardrails podem ser implementados, cada um responsável por interações especializadas (por exemplo, recuperação de dados, validação de entrada do usuário, invocação de API externa).
- Guardrails são capazes de processar dados multimodais (texto, áudio, vídeo) para monitoramento e controle abrangentes.

## Componentes

- **GuardrailManager**: Orquestra a aplicação de guardrails para cada modalidade.
- **TextGuardrail**: Lida com regras e restrições para dados de texto.
- **ImageGuardrail**: Lida com regras e restrições para dados de imagem.
- **AudioGuardrail**: Lida com regras e restrições para dados de áudio.
- **PolicyStore**: Armazena e gerencia políticas e configurações de guardrails.
- **ViolationReporter**: Coleta e reporta violações detectadas pelos guardrails.

## Fluxo de Trabalho

1. Dados de entrada (texto, imagem, áudio, vídeo) são recebidos pelo sistema.
2. O `GuardrailManager` encaminha os dados para os componentes de guardrail apropriados.
3. Cada guardrail aplica suas regras e verifica por violações.
4. Se uma violação for detectada, o `ViolationReporter` registra ou atua sobre o evento.
5. Apenas dados que passam por todos os guardrails relevantes seguem para processamento downstream.

## Consequências

### Benefícios

- **Robustez**: Filtra informações contextuais inadequadas, preservando a confiabilidade do modelo.
- **Segurança**: Valida as saídas do modelo, garantindo a segurança do usuário.
- **Alinhamento a Padrões**: Configurável para políticas organizacionais, padrões éticos e requisitos legais.
- **Adaptabilidade**: Pode ser implementado em diversos modelos e agentes, com requisitos personalizáveis.

### Desvantagens

- **Sobrecarga**: Coletar um corpus diverso e de alta qualidade para guardrails multimodais e realizar processamento em tempo real pode aumentar requisitos computacionais e custos.
- **Falta de Explicabilidade**: A complexidade dos guardrails multimodais pode dificultar a explicação dos resultados finais.

## Exemplos do Mundo Real

- **NeMo Guardrails (NVIDIA)**: Garante coerência de diálogo e previne impactos negativos de desinformação e tópicos sensíveis.
- **Llama Guard (Meta)**: Mecanismo de proteção baseado em modelo de fundação, treinado em uma taxonomia de risco para identificar conteúdo potencialmente arriscado ou violador em prompts e saídas.
- **Guardrails AI**: Fornece um hub listando vários validadores para lidar com diferentes riscos nas entradas e saídas de modelos de fundação.

## Relação com Outros Padrões

- **Proactive Goal Creator**: Multimodal Guardrails pode processar dados multimodais capturados por este padrão.
- **One-shot e Incremental Model Querying**: Multimodal Guardrails serve como camada intermediária, gerenciando entradas e saídas para consultas ao modelo.

## Casos de Uso

- Prevenir conteúdo inseguro ou não conforme em sistemas de IA multimodais.
- Impor diretrizes de direitos autorais, privacidade ou ética em diferentes tipos de dados.
- Extensão modular para suportar novas modalidades ou políticas atualizadas.

## Integração

- Implementar interfaces de guardrail para cada modalidade conforme necessário.
- Configurar o `PolicyStore` com regras específicas da organização.
- Integrar o `GuardrailManager` ao pipeline de dados multimodais.

## Trabalho Futuro

- Definir políticas de guardrail detalhadas para cada modalidade.
- Fornecer exemplos de implementação e trechos de código.
- Adicionar diagramas de classes e de sequência para ilustrar as interações entre componentes.



---