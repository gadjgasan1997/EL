using EL.Domain.Frontend.Parser.Ast;

namespace EL.Application.StaticAnalysis.Services.StaticAnalyzer;

/// <summary>
/// Статический анализатор кода
/// </summary>
public interface IStaticAnalyzer
{
    /// <summary>
    /// Анализирует АСТ
    /// </summary>
    /// <param name="ast">АСТ</param>
    void Analyze(IAbstractSyntaxTree ast);
}