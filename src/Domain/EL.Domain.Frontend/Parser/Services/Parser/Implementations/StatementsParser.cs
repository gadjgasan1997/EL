using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;
using static EL.Domain.Share.Dictionaries.TokenTypes;
using static EL.Domain.Share.Dictionaries.Keyword;
// ReSharper disable InvertIf

namespace EL.Domain.Frontend.Parser.Services.Parser.Implementations;

/// <inheritdoc cref="IParser" />
internal partial class TopDownParser
{
    private IEnumerable<StatementListItem> ParseStatementList()
    {
        while (
            _tokens.CurrentIsAny(ElAllTypesTokensTags) ||
            _tokens.CurrentIs(IdentifierTag) ||
            _tokens.CurrentIsAny(ElLiteralsTokensTags) ||
            _tokens.CurrentIs(LeftParenTag) ||
            _tokens.CurrentIs(LeftCurlTag) ||
            _tokens.CurrentIsKeyword())
        {
            var listItem = ParseStatementListItem();
            if (listItem.NeedSemicolon)
                _tokens.Expect(SemicolonTag);
            
            yield return listItem;
            
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (_tokens.Current is null)
                yield break;
        }
    }
    
    private StatementListItem ParseStatementListItem() =>
        _tokens.CurrentIsKeyword(Class) ||
        _tokens.CurrentIsAny(ElAllTypesTokensTags)
            ? ParseDeclaration()
            : ParseStatement();
    
    private Statement ParseStatement()
    {
        if (_tokens.CurrentIs(IdentifierTag))
        {
            var expression = ParseExpression();
            return new ExpressionStatement(expression);
        }
        
        if (_tokens.CurrentIs(LeftCurlTag))
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
        _tokens.Expect(LeftCurlTag);
        
        var statementList = ParseStatementList().ToList();
        _tokens.Expect(RightCurlTag);
        
        return new StatementsBlock(statementList);
    }
    
    private IfStatement ParseIfStatement()
    {
        _tokens.ExpectKeyword(If);
        _tokens.Expect(LeftParenTag);
        
        var expression = ParseExpression();
        _tokens.Expect(RightParenTag);
        
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
        _tokens.Expect(LeftParenTag);
        
        var expression = ParseExpression();
        _tokens.Expect(RightParenTag);
        
        var statement = ParseStatement();
        return new WhileStatement(expression, statement);
    }
    
    private ForStatement ParseForStatement()
    {
        _tokens.ExpectKeyword(For);
        _tokens.Expect(LeftParenTag);
        
        var declaration = _tokens.CurrentIs(SemicolonTag) ? null : (VariableDeclaration) ParseDeclaration();
        _tokens.Expect(SemicolonTag);
        
        var condition = ParseExpression();
        _tokens.Expect(SemicolonTag);
        
        var iteration = _tokens.CurrentIs(RightParenTag) ? null : ParseExpression();
        _tokens.Expect(RightParenTag);
        
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
        
        var expression = _tokens.CurrentIs(SemicolonTag) ? null : ParseExpression();
        return new ReturnStatement(expression);
    }
}