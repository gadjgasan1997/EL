using System.Reflection;
using System.Reflection.Emit;
using EL.Domain.Frontend.Parser.Ast;

namespace EL.Application.CodeGeneration.Services.CodeGenerator;

/// <inheritdoc cref="ICodeGenerator" />
internal class CodeGenerator : ICodeGenerator
{
    /// <inheritdoc cref="ICodeGenerator.Generate" />
    public void Generate(IAbstractSyntaxTree ast, MetadataLoadContext context, ModuleBuilder module)
    {
        
    }
}