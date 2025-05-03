using EL.Domain.Frontend.Lexer;
using EL.Domain.Frontend.Parser.Exceptions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.LeftHandSideExpressions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;
using ElType = EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.ElType;
using static EL.Domain.Share.Dictionaries.TokenTypes;
using static EL.Domain.Share.Dictionaries.Keyword;
// ReSharper disable InvertIf

namespace EL.Domain.Frontend.Parser.Services.Parser.Implementations;

/// <inheritdoc cref="IParser" />
internal partial class TopDownParser
{
    private Declaration ParseDeclaration()
    {
        if (_tokens.CurrentIsKeyword(Class))
            return ParseClassDeclaration();
        
        if (_tokens.CurrentIsAny(ElAllTypesTokensTags))
        {
            return _tokens.ForCurrent(
                (VoidTypeTag, _ => ParseVoidType()),
                (VarTypeTag, _ => ParseVarType()),
                (DoubleTypeTag, ParseForType),
                (IntTypeTag, ParseForType),
                (LongTypeTag, ParseForType),
                (BooleanTypeTag, ParseForType),
                (StringTypeTag, ParseForType));
        }
        
        throw new UnexpectedTokenException(_tokens.Current);
    }
    
    private NamespaceDeclaration ParseNamespaceDeclaration()
    {
        _tokens.ExpectKeyword(Namespace);
        
        var identifierToken = _tokens.Expect(IdentifierTag);
        _tokens.Expect(SemicolonTag);
        
        var statements = ParseStatementList().ToList();
        if (statements.Any(item => item is not ClassDeclaration))
            throw new ParserException("Root statements must be classes");
        
        return new NamespaceDeclaration(
            new IdentifierExpression(identifierToken.Value),
            new StatementsBlock(statements));
    }
    
    private ClassDeclaration ParseClassDeclaration()
    {
        _tokens.ExpectKeyword(Class);
        
        var identifierToken = _tokens.Expect(IdentifierTag);
        var body = ParseStatement();
        return new ClassDeclaration(
            new IdentifierExpression(identifierToken.Value),
            new StatementsBlock([body]));
    }
    
    private FunctionDeclaration ParseVoidType()
    {
        _tokens.Expect(VoidTypeTag);
        
        var identifierToken = _tokens.Expect(IdentifierTag);
        return ParseFunctionDeclaration(VoidTypeTag, identifierToken.Value);
    }
    
    private VariableDeclaration ParseVarType()
    {
        _tokens.Expect(VarTypeTag);
        
        var identifierToken = _tokens.Expect(IdentifierTag);
        return ParseVariableDeclaration(VarTypeTag, identifierToken);
    }
    
    private Declaration ParseForType(string typeTag)
    {
        _tokens.Expect(typeTag);
        
        var identifierToken = _tokens.Expect(IdentifierTag);
        if (_tokens.CurrentIs(SemicolonTag))
        {
            return new VariableDeclaration(
                new ElType(new IdentifierExpression(typeTag)),
                new IdentifierExpression(identifierToken.Value));
        }
        
        if (_tokens.CurrentIs(LeftParenTag))
            return ParseFunctionDeclaration(typeTag, identifierToken.Value);
        
        return ParseVariableDeclaration(typeTag, identifierToken);
    }
    
    private VariableDeclaration ParseVariableDeclaration(string type, Token token)
    {
        _tokens.Expect(AssignTag);
        
        var expression = ParseExpression();
        return new VariableDeclaration(
            new ElType(new IdentifierExpression(type)),
            new IdentifierExpression(token.Value),
            new AssignmentExpression(new IdentifierExpression(token.Value), expression));
    }
    
    private FunctionDeclaration ParseFunctionDeclaration(
        string returnType,
        string functionName)
    {
        var parameters = ParseFunctionParameters().ToList();
        var body = ParseStatement();
        
        return new FunctionDeclaration(
            new ElType(new IdentifierExpression(returnType)),
            new IdentifierExpression(functionName),
            parameters,
            new StatementsBlock([body]));
    }
    
    private IEnumerable<FunctionParameterDeclaration> ParseFunctionParameters()
    {
        _tokens.Expect(LeftParenTag);
        
        if (_tokens.CurrentIs(RightParenTag))
        {
            _tokens.Expect(RightParenTag);
            yield break;
        }
        
        while (true)
        {
            var typeToken = _tokens.ExpectAny(ElConcreteTypesTokensTags);
            var nameToken = _tokens.Expect(IdentifierTag);
            
            yield return new FunctionParameterDeclaration(
                new ElType(new IdentifierExpression(typeToken.Value)),
                new IdentifierExpression(nameToken.Value));
            
            if (_tokens.CurrentIs(RightParenTag))
            {
                _tokens.Expect(RightParenTag);
                yield break;
            }
            
            _tokens.Expect(CommaTag);
        }
    }
}