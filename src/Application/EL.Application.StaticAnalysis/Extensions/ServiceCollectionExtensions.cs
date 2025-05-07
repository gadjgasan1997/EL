using EL.Domain.Share.Dictionaries;
using EL.Domain.Frontend.Parser.Ast;
using EL.Application.StaticAnalysis.Visitors;
using EL.Application.StaticAnalysis.Services.ScopesResolver;
using EL.Application.StaticAnalysis.Services.StaticAnalyzer;
using EL.Application.StaticAnalysis.Services.SymbolTableStorage;
using Microsoft.Extensions.DependencyInjection;
using static EL.Application.StaticAnalysis.Dictionaries.KnownVisitors;

namespace EL.Application.StaticAnalysis.Extensions;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет сервисы слоя приложения для статического анализа
    /// </summary>
    /// <param name="services">Сервисы</param>
    public static IServiceCollection AddStaticAnalysisServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IScopesResolver, ScopesResolver>()
            .AddSingleton<ISymbolTablesStorage, SymbolTablesStorage>()
            .AddSingleton<IStaticAnalyzer, StaticAnalyzer>()
            .AddSingleton<IVisitor<IAbstractSyntaxTreeNode, bool>, StatementsReturnsValidator>()
            .AddKeyedSingleton<IVisitor<IAbstractSyntaxTreeNode>, TableStorageInitializer>(PRE_ANALYZER)
            .AddKeyedSingleton<IVisitor<IAbstractSyntaxTreeNode>, SymbolsTablesInitializer>(PRE_ANALYZER)
            .AddKeyedSingleton<IVisitor<IAbstractSyntaxTreeNode, ElType>, TypesBuilder>(TYPES_BUILDER)
            .AddKeyedSingleton<IVisitor<IAbstractSyntaxTreeNode, ElType>, SemanticChecker>(ANALYZER);
    }
}