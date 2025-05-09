using Agents.Common;

namespace PassiveGoalCreator;

/// <summary>
/// Provides factory methods to create and initialize instances of the <see cref="Memory"/> class.
/// </summary>
public static class MemoryFactory
{
    private static readonly (string key, string value) RetrieveContext = ("tools",
        """
            {
                items: [
                    { label: "GenerateMonthlySalesReport", value: "Generate a sales report summarizing revenue, units sold, and top products for a given month." },
                    { label: "AnalyzeCustomerFeedback", value: "Perform sentiment analysis on collected customer feedback to identify key concerns and satisfaction trends." },
                    { label: "CleanseTransactionData", value: "Remove duplicates, fix formatting issues, and validate fields in the raw transaction data." },
                    { label: "ForecastQuarterlyRevenue", value: "Estimate future revenue for the upcoming quarter using historical data and trends." },
                    { label: "DetectFraudulentTransactions", value: "Identify suspicious patterns in financial transactions that may indicate fraud." },
                    { label: "SummarizeMeetingTranscript", value: "Extract action items and key points from a meeting transcript." },
                    { label: "GenerateProductPerformanceChart", value: "Visualize product sales performance across regions and time periods." },
                    { label: "ClassifySupportTickets", value: "Automatically categorize incoming support tickets by topic and urgency." },
                    { label: "OptimizeInventoryLevels", value: "Suggest stock adjustments to prevent overstock or stockouts based on recent sales data." },
                    { label: "BuildCustomerChurnModel", value: "Create a machine learning model to predict which customers are likely to leave." },
                    { label: "TranslateReportToSpanish", value: "Translate an English business report into Spanish for international stakeholders." },
                    { label: "GenerateCodeDocumentation", value: "Produce documentation from source code files including function descriptions and usage examples." },
                    { label: "CompareMarketingCampaigns", value: "Analyze and compare the ROI of recent marketing campaigns." },
                    { label: "ExtractEntitiesFromContracts", value: "Identify names, dates, and obligations from legal contracts using NLP." },
                    { label: "SummarizeFinancialStatement", value: "Create an executive summary of a company's income statement or balance sheet." },
                    { label: "GenerateWeeklyActivityDigest", value: "Compile a summary of key system or user activities during the past week." },
                    { label: "EvaluateModelAccuracy", value: "Assess the performance of a predictive model based on test dataset metrics." },
                    { label: "CreateOnboardingChecklist", value: "Generate a step-by-step checklist for onboarding new employees." },
                    { label: "AggregateSurveyResponses", value: "Consolidate and summarize open-ended responses from a survey." },
                    { label: "IdentifySecurityVulnerabilities", value: "Scan logs or code repositories to find potential security weaknesses." }
                ]
            }
        """);

    /// <summary>
    /// Creates a new instance of the Memory object and initializes it with merged context data.
    /// </summary>
    /// <returns>A new Memory object containing merged context data.</returns>
    public static Memory CreateMemory()
    {
        var memory = new Memory();
        memory.StoreContext(ContextData.MergeAll(RetrieveContext));
        return memory;
    }
}