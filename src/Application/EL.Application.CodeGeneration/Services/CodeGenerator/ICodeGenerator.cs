using System.Reflection;
using System.Reflection.Emit;
using EL.Domain.Frontend.Parser.Ast;

namespace EL.Application.CodeGeneration.Services.CodeGenerator;

/// <summary>
/// Генератор кода
/// </summary>
public interface ICodeGenerator
{
    /// <summary>
    /// Генерирует код
    /// </summary>
    /// <param name="ast">Дерево</param>
    /// <param name="context">Контекст</param>
    /// <param name="module">Модуль</param>
    void Generate(
        IAbstractSyntaxTree ast,
        MetadataLoadContext context,
        ModuleBuilder module);
}