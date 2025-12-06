using Agents.Common.Interfaces;

namespace Agents.Common.Storage;

/// <summary>
/// Provides a factory for creating instances of the Memory class with initialized context data.
/// </summary>
public static class MemoryFactory
{
    private static readonly (string key, string value) RetrieveContext = ("tools",
        """
        {
            items: [
                { label: "GerarRelatorioVendasMensal", value: "Gerar um relatório de vendas resumindo receita, unidades vendidas e principais produtos para um mês específico." },
                { label: "AnalisarFeedbackClientes", value: "Realizar análise de sentimento nos feedbacks de clientes coletados para identificar preocupações principais e tendências de satisfação." },
                { label: "LimparDadosTransacao", value: "Remover duplicatas, corrigir problemas de formatação e validar campos nos dados brutos de transações." },
                { label: "PreverReceitaTrimestral", value: "Estimar a receita futura para o próximo trimestre usando dados históricos e tendências." },
                { label: "IdentificarTransacoesFraudulentas", value: "Identificar padrões suspeitos em transações financeiras que possam indicar fraude." },
                { label: "ResumiTranscricaoReuniao", value: "Extrair itens de ação e pontos principais de uma transcrição de reunião." },
                { label: "GerarGraficoDesempenhoProduto", value: "Visualizar o desempenho de vendas de produtos por região e período." },
                { label: "ClassificarTicketsSuporte", value: "Categorizar automaticamente tickets de suporte por assunto e urgência." },
                { label: "OtimizarNiveisEstoque", value: "Sugerir ajustes de estoque para evitar excesso ou falta com base nas vendas recentes." },
                { label: "ConstruirModeloRotatividadeClientes", value: "Criar um modelo de aprendizado de máquina para prever quais clientes têm probabilidade de sair." },
                { label: "TraduzirRelatorioEspanhol", value: "Traduzir um relatório empresarial em inglês para espanhol para stakeholders internacionais." },
                { label: "GerarDocumentacaoCodigo", value: "Produzir documentação a partir de arquivos fonte incluindo descrições de funções e exemplos de uso." },
                { label: "CompararCampanhasMarketing", value: "Analisar e comparar o ROI das campanhas de marketing recentes." },
                { label: "ExtrairEntidadesContratos", value: "Identificar nomes, datas e obrigações em contratos legais usando PLN." },
                { label: "ResumiDemonstracaoFinanceira", value: "Criar um resumo executivo da demonstração de resultados ou do balanço patrimonial de uma empresa." },
                { label: "GerarResumoAtividadeSemanal", value: "Compilar um resumo das principais atividades do sistema ou dos usuários na última semana." },
                { label: "AvaliarPrecisaoModelo", value: "Avaliar o desempenho de um modelo preditivo com base em métricas do conjunto de teste." },
                { label: "CriarChecklistIntegracao", value: "Gerar uma lista de verificação passo a passo para integração de novos colaboradores." },
                { label: "AgregarRespostasPesquisa", value: "Consolidar e resumir respostas abertas de uma pesquisa." },
                { label: "IdentificarVulnerabilidadesSeguranca", value: "Analisar logs ou repositórios de código para encontrar possíveis vulnerabilidades de segurança." }
            ]
        }
        """);

    public static IMemoryStore CreateEmptyMemoryStore() => new MemoryStory();

    /// <summary>
    /// Creates and initializes a new instance of the Memory class.
    /// The method retrieves context data, merges the retrieved data,
    /// and stores the merged context within the created Memory instance.
    /// </summary>
    /// <returns>A newly created instance of the Memory class with stored context data.</returns>
    public static IMemoryStore CreateDefaultMemory()
    {
        var memory = new MemoryStory();
        memory.StoreContext(ContextData.MergeAll(RetrieveContext));
        return memory;
    }
}