using System.Text.RegularExpressions;
using EL.Domain.Share.Dictionaries;
using EL.Domain.Frontend.Parser.Exceptions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.LeftHandSideExpressions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;
using static EL.Domain.Share.Dictionaries.TokenType;
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
        if (left is LeftHandSideExpression lhs && _tokens.CurrentIs(Assign))
        {
            _tokens.Expect(Assign);
            
            var right = ParseExpression();
            return new AssignmentExpression(lhs, right);
        }
        
        return left;
    }
    
    private Expression ParseTernary()
    {
        var left = ParseNullCoalescing();
        while (_tokens.CurrentIs(Question))
        {
            _tokens.Expect(Question);
            
            var then = ParseExpression();
            _tokens.Expect(Colon);
            
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
        if (_tokens.CurrentIs(LeftParen))
        {
            var id = (IdentifierExpression) expression;
            var parameters = ParseCallParameters().ToList();
            return new CallExpression(id, parameters);
        }
        
        return expression;
    }
    
    private IEnumerable<CallParameterExpression> ParseCallParameters()
    {
        _tokens.Expect(LeftParen);
        
        if (_tokens.CurrentIs(RightParen))
        {
            _tokens.Expect(RightParen);
            yield break;
        }
        
        while (true)
        {
            var expression = ParseExpression();
            yield return new CallParameterExpression(expression);
            
            if (_tokens.CurrentIs(RightParen))
            {
                _tokens.Expect(RightParen);
                break;
            }
            
            _tokens.Expect(Comma);
        }
    }
    
    private Expression ParsePrimaryExpression()
    {
        if (_tokens.CurrentIs(LeftParen))
        {
            _tokens.Expect(LeftParen);
            
            var expression = ParseExpression();
            _tokens.Expect(RightParen);
            
            return expression;
        }
        
        if (_tokens.CurrentIs(Identifier))
        {
            var token = _tokens.Expect(Identifier);
            if (_tokens.CurrentIs(Assign))
                return new VariableAssignmentExpression(new IdentifierExpression(token.Value));
            
            return new IdentifierExpression(token.Value);
        }
        
        if (_tokens.CurrentIsAny(ElLiteralsTokens))
            return ParseLiteralExpression();
        
        throw new UnexpectedTokenException(_tokens.Current);
    }
    
    private LiteralExpression ParseLiteralExpression()
    {
        return _tokens.ForCurrent(
            (DoubleLiteral, _ => ParseLiteralCore(
                DoubleLiteral,
                ElType.DoubleType,
                value => double.Parse(value, InvariantCulture))),
            (IntLiteral, _ => ParseLiteralCore(
                IntLiteral,
                ElType.IntType,
                value => int.Parse(value, InvariantCulture))),
            (BooleanLiteral, _ => ParseLiteralCore(
                BooleanLiteral,
                ElType.BooleanType,
                value => bool.Parse(value))),
            (NullLiteral, _ => ParseLiteralCore(
                NullLiteral,
                ElType.NullType,
                _ => null)),
            (StringLiteral, _ => ParseLiteralCore(
                StringLiteral,
                ElType.StringType,
                value => Regex.Unescape(value.Trim('"')))));
        
        LiteralExpression ParseLiteralCore(
            TokenType tokenType,
            ElType elType,
            Func<string, object?> func)
        {
            var token = _tokens.Expect(tokenType);
            return new LiteralExpression(
                new ElTypeNode(new IdentifierExpression(elType.Value)),
                func(token.Value));
        }
    }
}