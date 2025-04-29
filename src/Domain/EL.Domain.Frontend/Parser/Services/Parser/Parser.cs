using EL.Domain.Frontend.Lexer;
using EL.Domain.Frontend.Parser.Ast;
using EL.Domain.Frontend.Parser.Ast.Implementation;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;

namespace EL.Domain.Frontend.Parser.Services.Parser;

/// <inheritdoc cref="IParser" />
internal class Parser : IParser
{
    private TokensStream _tokens = new List<Token>();
    
    /// <inheritdoc cref="IParser.Parse" />
    public IAbstractSyntaxTree Parse(IReadOnlyCollection<Token> tokens)
    {
        _tokens = tokens.ToList();
        
        var root = new ScriptBody([]);
        return new AbstractSyntaxTree(root);
    }
}