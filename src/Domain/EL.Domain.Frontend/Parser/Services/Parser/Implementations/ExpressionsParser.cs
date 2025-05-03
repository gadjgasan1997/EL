using System.Text.RegularExpressions;
using EL.Domain.Frontend.Parser.Exceptions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.LeftHandSideExpressions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;
using static EL.Domain.Share.Dictionaries.TokenTypes;
using static EL.Domain.Share.Dictionaries.Operator;
using static System.Globalization.CultureInfo;
// ReSharper disable InvertIf

namespace EL.Domain.Frontend.Parser.Services.Parser.Implementations;

/// <inheritdoc cref="IParser" />
internal partial class TopDownParser
{
    private Expression ParseExpression()
    {
        var left = ParseTernary();
        if (left is LeftHandSideExpression lhs && _tokens.CurrentIs(AssignTag))
        {
            _tokens.Expect(AssignTag);
            
            var right = ParseExpression();
            return new AssignmentExpression(lhs, right);
        }
        
        return left;
    }
    
    private Expression ParseTernary()
    {
        var left = ParseNullCoalescing();
        while (_tokens.CurrentIs(QuestionTag))
        {
            _tokens.Expect(QuestionTag);
            
            var then = ParseExpression();
            _tokens.Expect(ColonTag);
            
            var @else = ParseExpression();
            return new TernaryOperatorExpression(left, then, @else);
        }
        
        return left;
    }
    
    private Expression ParseNullCoalescing()
    {
        var left = ParseLogicalOr();
        while (_tokens.CurrentIsOperator(NullCoalescing))
        {
            var token = _tokens.ExpectOperator(NullCoalescing);
            var right = ParseNullCoalescing();
            left = new BinaryExpression(left, token.Value, right);
        }
        
        return left;
    }
    
    private Expression ParseLogicalOr()
    {
        var left = ParseLogicalAnd();
        while (_tokens.CurrentIsOperator(Or))
        {
            var token = _tokens.ExpectOperator();
            var right = ParseLogicalAnd();
            left = new BinaryExpression(left, token.Value, right);
        }
        
        return left;
    }
    
    private Expression ParseLogicalAnd()
    {
        var left = ParseEquality();
        while (_tokens.CurrentIsOperator(And))
        {
            var token = _tokens.ExpectOperator();
            var right = ParseEquality();
            left = new BinaryExpression(left, token.Value, right);
        }
        
        return left;
    }
    
    private Expression ParseEquality()
    {
        var left = ParseRelational();
        while (_tokens.CurrentIsOperator(Equal) || _tokens.CurrentIsOperator(NotEqual))
        {
            var token = _tokens.ExpectOperator();
            var right = ParseRelational();
            left = new BinaryExpression(left, token.Value, right);
        }
        
        return left;
    }
    
    private Expression ParseRelational()
    {
        var left = ParseAdditive();
        while (_tokens.CurrentIsOperator(GreaterThan) ||
               _tokens.CurrentIsOperator(LessThan) ||
               _tokens.CurrentIsOperator(GreaterThanOrEqual) ||
               _tokens.CurrentIsOperator(LessThanOrEqual))
        {
            var token = _tokens.ExpectOperator();
            var right = ParseAdditive();
            left = new BinaryExpression(left, token.Value, right);
        }
        
        return left;
    }
    
    private Expression ParseAdditive()
    {
        var left = ParseMultiplicative();
        while (_tokens.CurrentIsOperator(Plus) || _tokens.CurrentIsOperator(Minus))
        {
            var token = _tokens.ExpectOperator();
            var right = ParseMultiplicative();
            left = new BinaryExpression(left, token.Value, right);
        }
        
        return left;
    }
    
    private Expression ParseMultiplicative()
    {
        var left = ParseUnary();
        while (
            _tokens.CurrentIsOperator(Multiply) ||
            _tokens.CurrentIsOperator(Divide) ||
            _tokens.CurrentIsOperator(Remainder))
        {
            var token = _tokens.ExpectOperator();
            var right = ParseUnary();
            left = new BinaryExpression(left, token.Value, right);
        }
        
        return left;
    }
    
    private Expression ParseUnary()
    {
        if (_tokens.CurrentIsOperator(Not) || _tokens.CurrentIsOperator(Minus))
        {
            var token = _tokens.ExpectOperator();
            var expression = ParseUnary();
            return new UnaryExpression(token.Value, expression);
        }
        
        return ParsePostfix();
    }
    
    private Expression ParsePostfix()
    {
        var left = ParseCallExpression();
        while (_tokens.CurrentIsOperator(Increment) || _tokens.CurrentIsOperator(Decrement))
        {
            var token = _tokens.ExpectOperator();
            return new UnaryExpression(token.Value, left);
        }
        
        return left;
    }
    
    private Expression ParseCallExpression()
    {
        var expression = ParsePrimaryExpression();
        if (_tokens.CurrentIs(LeftParenTag))
        {
            var id = (IdentifierExpression) expression;
            var parameters = ParseCallParameters().ToList();
            return new CallExpression(id, parameters);
        }
        
        return expression;
    }
    
    private IEnumerable<CallParameterExpression> ParseCallParameters()
    {
        _tokens.Expect(LeftParenTag);
        
        if (_tokens.CurrentIs(RightParenTag))
        {
            _tokens.Expect(RightParenTag);
            yield break;
        }
        
        while (true)
        {
            var expression = ParseExpression();
            yield return new CallParameterExpression(expression);
            
            if (_tokens.CurrentIs(RightParenTag))
            {
                _tokens.Expect(RightParenTag);
                break;
            }
            
            _tokens.Expect(CommaTag);
        }
    }
    
    private Expression ParsePrimaryExpression()
    {
        if (_tokens.CurrentIs(LeftParenTag))
        {
            _tokens.Expect(LeftParenTag);
            
            var expression = ParseExpression();
            _tokens.Expect(RightParenTag);
            
            return expression;
        }
        
        if (_tokens.CurrentIs(IdentifierTag))
        {
            var token = _tokens.Expect(IdentifierTag);
            if (_tokens.CurrentIs(AssignTag))
                return new VariableAssignmentExpression(new IdentifierExpression(token.Value));
            
            return new IdentifierExpression(token.Value);
        }
        
        if (_tokens.CurrentIsAny(ElLiteralsTokensTags))
            return ParseLiteralExpression();
        
        throw new UnexpectedTokenException(_tokens.Current);
    }
    
    private LiteralExpression ParseLiteralExpression()
    {
        return _tokens.ForCurrent(
            (DoubleLiteralTag, _ => ParseLiteralCore(
                DoubleLiteralTag,
                value => double.Parse(value, InvariantCulture))),
            (IntLiteralTag, _ => ParseLiteralCore(
                IntLiteralTag,
                value => int.Parse(value, InvariantCulture))),
            (BooleanLiteralTag, _ => ParseLiteralCore(
                BooleanLiteralTag,
                value => bool.Parse(value))),
            (NullLiteralTag, _ => ParseLiteralCore(
                NullLiteralTag,
                _ => null)),
            (StringLiteralTag, _ => ParseLiteralCore(
                StringLiteralTag,
                value => Regex.Unescape(value.Trim('"')))));
        
        LiteralExpression ParseLiteralCore(string typeTag, Func<string, object?> func)
        {
            var token = _tokens.Expect(typeTag);
            return new LiteralExpression(
                new ElType(new IdentifierExpression(typeTag)),
                func(token.Value));
        }
    }
}