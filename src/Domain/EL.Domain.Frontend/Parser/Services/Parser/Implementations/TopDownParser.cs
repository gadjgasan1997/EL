using EL.Domain.Frontend.Parser.Ast;
using EL.Domain.Frontend.Parser.Ast.Implementation;
using EL.Domain.Frontend.Lexer.TokensEnumerator;

namespace EL.Domain.Frontend.Parser.Services.Parser.Implementations;

/// <inheritdoc cref="IParser" />
internal partial class TopDownParser : IParser
{
    private ITokensEnumerator _tokens = null!;
    
    /// <inheritdoc cref="IParser.Parse" />
    public IAbstractSyntaxTree Parse(ITokensEnumerator tokens)
    {
        _tokens = tokens;
        
        var namespaceDeclaration = ParseNamespaceDeclaration();
        return new AbstractSyntaxTree(namespaceDeclaration);
    }
}