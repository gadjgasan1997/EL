using EL.Domain.Share.Dictionaries;
using EL.Domain.Frontend.Lexer;
using EL.Domain.Frontend.Parser.Exceptions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.LeftHandSideExpressions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;
using ElType = EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.ElType;
using static EL.Domain.Share.Dictionaries.TokenTypes;
using static EL.Domain.Share.Dictionaries.TokenType;
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
        
        if (_tokens.CurrentIsAny(ElAllTypesTokens))
        {
            return _tokens.ForCurrent(
                (VoidType, _ => ParseVoidType()),
                (VarType, _ => ParseVarType()),
                (DoubleType, ParseForType),
                (IntType, ParseForType),
                (LongType, ParseForType),
                (BooleanType, ParseForType),
                (StringType, ParseForType));
        }
        
        throw new UnexpectedTokenException(_tokens.Current);
    }
    
    private NamespaceDeclaration ParseNamespaceDeclaration()
    {
        _tokens.ExpectKeyword(Namespace);
        
        var identifierToken = _tokens.Expect(Identifier);
        _tokens.Expect(Semicolon);
        
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
        
        var identifierToken = _tokens.Expect(Identifier);
        var body = ParseStatement();
        return new ClassDeclaration(
            new IdentifierExpression(identifierToken.Value),
            new StatementsBlock([body]));
    }
    
    private FunctionDeclaration ParseVoidType()
    {
        _tokens.Expect(VoidType);
        
        var identifierToken = _tokens.Expect(Identifier);
        return ParseFunctionDeclaration(VoidType, identifierToken.Value);
    }
    
    private VariableDeclaration ParseVarType()
    {
        _tokens.Expect(VarType);
        
        var identifierToken = _tokens.Expect(Identifier);
        return ParseVariableDeclaration(VarType, identifierToken);
    }
    
    private Declaration ParseForType(TokenType type)
    {
        _tokens.Expect(type);
        
        var identifierToken = _tokens.Expect(Identifier);
        if (_tokens.CurrentIs(Semicolon))
        {
            return new VariableDeclaration(
                new ElType(new IdentifierExpression(type.Value)),
                new IdentifierExpression(identifierToken.Value));
        }
        
        if (_tokens.CurrentIs(LeftParen))
            return ParseFunctionDeclaration(type, identifierToken.Value);
        
        return ParseVariableDeclaration(type, identifierToken);
    }
    
    private VariableDeclaration ParseVariableDeclaration(TokenType type, Token token)
    {
        _tokens.Expect(Assign);
        
        var expression = ParseExpression();
        return new VariableDeclaration(
            new ElType(new IdentifierExpression(type.Value)),
            new IdentifierExpression(token.Value),
            new AssignmentExpression(new IdentifierExpression(token.Value), expression));
    }
    
    private FunctionDeclaration ParseFunctionDeclaration(
        TokenType returnType,
        string functionName)
    {
        var parameters = ParseFunctionParameters().ToList();
        var body = ParseStatement();
        
        return new FunctionDeclaration(
            new ElType(new IdentifierExpression(returnType.Value)),
            new IdentifierExpression(functionName),
            parameters,
            new StatementsBlock([body]));
    }
    
    private IEnumerable<FunctionParameterDeclaration> ParseFunctionParameters()
    {
        _tokens.Expect(LeftParen);
        
        if (_tokens.CurrentIs(RightParen))
        {
            _tokens.Expect(RightParen);
            yield break;
        }
        
        while (true)
        {
            var typeToken = _tokens.ExpectAny(ElConcreteTypesTokens);
            var nameToken = _tokens.Expect(Identifier);
            
            yield return new FunctionParameterDeclaration(
                new ElType(new IdentifierExpression(typeToken.Value)),
                new IdentifierExpression(nameToken.Value));
            
            if (_tokens.CurrentIs(RightParen))
            {
                _tokens.Expect(RightParen);
                yield break;
            }
            
            _tokens.Expect(Comma);
        }
    }
}