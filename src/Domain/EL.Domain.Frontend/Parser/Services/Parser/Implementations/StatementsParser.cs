using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;
using static EL.Domain.Share.Dictionaries.TokenTypes;
using static EL.Domain.Share.Dictionaries.TokenType;
using static EL.Domain.Share.Dictionaries.Keyword;
// ReSharper disable InvertIf

namespace EL.Domain.Frontend.Parser.Services.Parser.Implementations;

/// <inheritdoc cref="IParser" />
internal partial class TopDownParser
{
    private IEnumerable<StatementListItem> ParseStatementList()
    {
        while (
            _tokens.CurrentIsAny(ElAllTypesTokens) ||
            _tokens.CurrentIs(Identifier) ||
            _tokens.CurrentIsAny(ElLiteralsTokens) ||
            _tokens.CurrentIs(LeftParen) ||
            _tokens.CurrentIs(LeftCurl) ||
            _tokens.CurrentIsKeyword())
        {
            var listItem = ParseStatementListItem();
            if (listItem.NeedSemicolon)
                _tokens.Expect(Semicolon);
            
            yield return listItem;
            
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (_tokens.Current is null)
                yield break;
        }
    }
    
    private StatementListItem ParseStatementListItem() =>
        _tokens.CurrentIsKeyword(Class) ||
        _tokens.CurrentIsAny(ElAllTypesTokens)
            ? ParseDeclaration()
            : ParseStatement();
    
    private Statement ParseStatement()
    {
        if (_tokens.CurrentIs(Identifier))
        {
            var expression = ParseExpression();
            return new ExpressionStatement(expression);
        }
        
        if (_tokens.CurrentIs(LeftCurl))
            return ParseStatementsBlock();
        
        return _tokens.ForCurrentKeyword<Statement>(
            (If, ParseIfStatement),
            (While, ParseWhileStatement),
            (For, ParseForStatement),
            (Break, ParseBreakStatement),
            (Continue, ParseContinueStatement),
            (Return, ParseReturnStatement));
    }
    
    private StatementsBlock ParseStatementsBlock()
    {
        _tokens.Expect(LeftCurl);
        
        var statementList = ParseStatementList().ToList();
        _tokens.Expect(RightCurl);
        
        return new StatementsBlock(statementList);
    }
    
    private IfStatement ParseIfStatement()
    {
        _tokens.ExpectKeyword(If);
        _tokens.Expect(LeftParen);
        
        var expression = ParseExpression();
        _tokens.Expect(RightParen);
        
        var statement = ParseStatement();
        if (_tokens.CurrentIsKeyword(Else))
        {
            _tokens.ExpectKeyword(Else);
            
            var @else = ParseStatement();
            return new IfStatement(expression, statement, @else);
        }
        
        return new IfStatement(expression, statement);
    }
    
    private WhileStatement ParseWhileStatement()
    {
        _tokens.ExpectKeyword(While);
        _tokens.Expect(LeftParen);
        
        var expression = ParseExpression();
        _tokens.Expect(RightParen);
        
        var statement = ParseStatement();
        return new WhileStatement(expression, statement);
    }
    
    private ForStatement ParseForStatement()
    {
        _tokens.ExpectKeyword(For);
        _tokens.Expect(LeftParen);
        
        var declaration = _tokens.CurrentIs(Semicolon) ? null : (VariableDeclaration) ParseDeclaration();
        _tokens.Expect(Semicolon);
        
        var condition = ParseExpression();
        _tokens.Expect(Semicolon);
        
        var iteration = _tokens.CurrentIs(RightParen) ? null : ParseExpression();
        _tokens.Expect(RightParen);
        
        var body = ParseStatement();
        return new ForStatement(declaration, condition, iteration, body);
    }
    
    private BreakStatement ParseBreakStatement()
    {
        _tokens.ExpectKeyword(Break);
        return new BreakStatement();
    }
    
    private ContinueStatement ParseContinueStatement()
    {
        _tokens.ExpectKeyword(Continue);
        return new ContinueStatement();
    }
    
    private ReturnStatement ParseReturnStatement()
    {
        _tokens.ExpectKeyword(Return);
        
        var expression = _tokens.CurrentIs(Semicolon) ? null : ParseExpression();
        return new ReturnStatement(expression);
    }
}