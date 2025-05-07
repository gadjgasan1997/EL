using EL.Domain.Share.Dictionaries;
using EL.Domain.Frontend.Parser.Ast;
using Microsoft.Extensions.DependencyInjection;
using static EL.Application.StaticAnalysis.Dictionaries.KnownVisitors;

namespace EL.Application.StaticAnalysis.Services.StaticAnalyzer;

/// <inheritdoc cref="IStaticAnalyzer" />
internal class StaticAnalyzer(
    [FromKeyedServices(PRE_ANALYZER)] IEnumerable<IVisitor<IAbstractSyntaxTreeNode>> preAnalyzers,
    [FromKeyedServices(ANALYZER)] IVisitor<IAbstractSyntaxTreeNode, ElType> analyzer)
    : IStaticAnalyzer
{
    /// <inheritdoc cref="IStaticAnalyzer.Analyze" />
    public void Analyze(IAbstractSyntaxTree ast)
    {
        var root = ast.Root;
        foreach (var preAnalyzer in preAnalyzers)
            root.Accept(preAnalyzer);
        
        root.Accept(analyzer);
    }
}