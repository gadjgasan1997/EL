using EL.Domain.IR.Symbols.Ids;
using EL.Domain.Share.Dictionaries;
using EL.Domain.Frontend.Parser.Ast;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.LeftHandSideExpressions;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Expressions.PrimaryExpressions;
using EL.Domain.Frontend.Parser.Extensions;
using EL.Application.StaticAnalysis.Exceptions;
using EL.Application.StaticAnalysis.Extensions;
using EL.Application.StaticAnalysis.Services.SymbolTableStorage;
using Microsoft.Extensions.DependencyInjection;
using static EL.Domain.Share.Dictionaries.Operator;
using static EL.Domain.Share.Dictionaries.ElType;
using static EL.Application.StaticAnalysis.Dictionaries.KnownVisitors;
// ReSharper disable ForCanBeConvertedToForeach

namespace EL.Application.StaticAnalysis.Visitors;

/// <summary>
/// Визитор выполнения семантического анализа кода
/// </summary>
internal class SemanticChecker : VisitorBase<IAbstractSyntaxTreeNode, ElType>,
    IVisitor<NamespaceDeclaration, ElType>,
    IVisitor<ClassDeclaration, ElType>,
    IVisitor<FunctionDeclaration, ElType>,
    IVisitor<StatementsBlock, ElType>,
    IVisitor<VariableDeclaration, ElType>,
    IVisitor<ExpressionStatement, ElType>,
    IVisitor<IfStatement, ElType>,
    IVisitor<WhileStatement, ElType>,
    IVisitor<ForStatement, ElType>,
    IVisitor<BreakStatement, ElType>,
    IVisitor<ContinueStatement, ElType>,
    IVisitor<ReturnStatement, ElType>,
    IVisitor<CallExpression, ElType>,
    IVisitor<CallParameterExpression, ElType>,
    IVisitor<AssignmentExpression, ElType>,
    IVisitor<TernaryOperatorExpression, ElType>,
    IVisitor<BinaryExpression, ElType>,
    IVisitor<UnaryExpression, ElType>,
    IVisitor<VariableAssignmentExpression, ElType>,
    IVisitor<LiteralExpression, ElType>,
    IVisitor<IdentifierExpression, ElType>
{
    private readonly ISymbolTablesStorage _tablesStorage;
    private readonly IVisitor<IAbstractSyntaxTreeNode, ElType> _typesBuilder;
    private readonly IVisitor<IAbstractSyntaxTreeNode, bool> _validator;
    
    public SemanticChecker(
        ISymbolTablesStorage tablesStorage,
        [FromKeyedServices(TYPES_BUILDER)] IVisitor<IAbstractSyntaxTreeNode, ElType> typesBuilder,
        IVisitor<IAbstractSyntaxTreeNode, bool> validator)
    {
        _tablesStorage = tablesStorage;
        _typesBuilder = typesBuilder;
        _validator = validator;
    }
    
    /// <inheritdoc cref="IVisitor{NamespaceDeclaration, Type}" />
    public ElType Visit(NamespaceDeclaration visitable)
    {
        for (var i = 0; i < visitable.Count; i++)
            visitable[i].Accept(This);
        
        return string.Empty;
    }
    
    /// <inheritdoc cref="IVisitor{ClassDeclaration, Type}" />
    public ElType Visit(ClassDeclaration visitable)
    {
        for (var i = 0; i < visitable.Count; i++)
            visitable[i].Accept(This);
        
        return string.Empty;
    }
    
    /// <inheritdoc cref="IVisitor{FunctionDeclaration, Type}" />
    public ElType Visit(FunctionDeclaration visitable)
    {
        ElType returnType = visitable.ReturnType.Id.Name;
        
        var statements = visitable.ReturnStatements
            .Select(statement => statement.Accept(This))
            .ToList();
        
        if (returnType == VoidType)
        {
            var nonVoidReturnType = statements.FirstOrDefault(type => type != VoidType);
            if (nonVoidReturnType is not null)
                throw new FunctionShouldNotReturnValue(visitable, nonVoidReturnType);
            
            return visitable.Body.Accept(This);
        }
        
        var unacceptableReturnType = statements.FirstOrDefault(type => type != returnType);
        if (unacceptableReturnType is not null)
        {
            throw new UnacceptableFunctionReturnType(
                visitable,
                returnType,
                unacceptableReturnType);
        }
        
        if (visitable.Accept(_validator))
            return visitable.Body.Accept(This);
        
        throw new NotAllCodeBranchesReturnsValue(visitable);
    }
    
    /// <inheritdoc cref="IVisitor{StatementsBlock, Type}" />
    public ElType Visit(StatementsBlock visitable)
    {
        for (var i = 0; i < visitable.Count; i++)
            visitable[i].Accept(This);
        
        return string.Empty;
    }
    
    /// <inheritdoc cref="IVisitor{VariableDeclaration, Type}" />
    public ElType Visit(VariableDeclaration visitable)
    {
        if (!visitable.IsChildOf<FunctionDeclaration>())
            throw new VariableOutsideOfFunction(visitable);
        
        return string.Empty;
    }
    
    /// <inheritdoc cref="IVisitor{ExpressionStatement, Type}" />
    public ElType Visit(ExpressionStatement visitable)
    {
        for (var i = 0; i < visitable.Count; i++)
            visitable[i].Accept(This);
        
        return string.Empty;
    }
    
    /// <inheritdoc cref="IVisitor{IfStatement, Type}" />
    public ElType Visit(IfStatement visitable)
    {
        var type = visitable.Condition.Accept(This);
        if (type != BooleanType)
            throw new NotBooleanCondition(visitable, type);
        
        visitable.Then.Accept(This);
        visitable.Else?.Accept(This);
        return string.Empty;
    }
    
    /// <inheritdoc cref="IVisitor{WhileStatement, Type}" />
    public ElType Visit(WhileStatement visitable)
    {
        var type = visitable.Condition.Accept(This);
        if (type != BooleanType)
            throw new NotBooleanCondition(visitable, type);
        
        visitable.Then.Accept(This);
        return string.Empty;
    }
    
    /// <inheritdoc cref="IVisitor{ForStatement, Type}" />
    public ElType Visit(ForStatement visitable)
    {
        visitable.Declaration?.Accept(This);
        
        var type = visitable.Condition?.Accept(This);
        if (type is not null && type != BooleanType)
            throw new NotBooleanCondition(visitable, type);
        
        visitable.Iteration?.Accept(This);
        visitable.Body.Accept(This);
        return string.Empty;
    }
    
    /// <inheritdoc cref="IVisitor{BreakStatement, Type}" />
    public ElType Visit(BreakStatement visitable)
    {
        if (!visitable.IsChildOf<ForStatement, WhileStatement>())
            throw new BreakOutsideOfCycle(visitable);
        
        return string.Empty;
    }
    
    /// <inheritdoc cref="IVisitor{ContinueStatement, Type}" />
    public ElType Visit(ContinueStatement visitable)
    {
        if (!visitable.IsChildOf<ForStatement, WhileStatement>())
            throw new ContinueOutsideOfCycle(visitable);
        
        return string.Empty;
    }
    
    /// <inheritdoc cref="IVisitor{ReturnStatement, Type}" />
    public ElType Visit(ReturnStatement visitable)
    {
        if (!visitable.IsChildOf<FunctionDeclaration>())
            throw new ReturnOutsideOfFunction(visitable);
        
        return visitable.Expression?.Accept(This) ?? VoidType;
    }
    
    /// <inheritdoc cref="IVisitor{CallExpression, Type}" />
    public ElType Visit(CallExpression visitable)
    {
        return string.Empty;
    }
    
    /// <inheritdoc cref="IVisitor{CallParameterExpression, Type}" />
    public ElType Visit(CallParameterExpression visitable)
    {
        return string.Empty;
    }
    
    /// <inheritdoc cref="IVisitor{AssignmentExpression, Type}" />
    public ElType Visit(AssignmentExpression visitable)
    {
        var destinationType = visitable.Destination.Accept(This);
        var sourceType = visitable.Source.Accept(This);
        if (destinationType != sourceType)
            throw new AssigmentIncomparableTypes(destinationType, sourceType);
        
        return destinationType;
    }
    
    /// <inheritdoc cref="IVisitor{TernaryOperatorExpression, Type}" />
    public ElType Visit(TernaryOperatorExpression visitable)
    {
        var type = visitable.Condition.Accept(This);
        if (type != BooleanType)
            throw new NotBooleanCondition(visitable, type);
        
        var thenType = visitable.Then.Accept(This);
        var elseType = visitable.Else.Accept(This);
        if (thenType != elseType)
            throw new TernaryOperatorTypesNotSame(thenType, elseType);
        
        return thenType;
    }
    
    /// <inheritdoc cref="IVisitor{BinaryExpression, Type}" />
    public ElType Visit(BinaryExpression visitable)
    {
        var leftType = visitable.Left.Accept(This);
        var rightType = visitable.Right.Accept(This);
        
        if (leftType == VoidType || rightType == VoidType)
            throw new UnsupportedTypesOperation(leftType, visitable.Operator, rightType);
        
        switch (visitable.Operator.Value)
        {
            case PLUS:
            {
                if (leftType == DoubleType && rightType == DoubleType ||
                    leftType == IntType && rightType == IntType ||
                    leftType == LongType && rightType == LongType ||
                    leftType == StringType && rightType == StringType)
                {
                    return leftType;
                }
                
                throw new UnsupportedTypesOperation(leftType, visitable.Operator, rightType);
            }
            case MINUS or MULTIPLY or DIVIDE or REMAINDER:
            {
                if (leftType == DoubleType && rightType == DoubleType ||
                    leftType == IntType && rightType == IntType ||
                    leftType == LongType && rightType == LongType)
                {
                    return leftType;
                }
                
                throw new UnsupportedTypesOperation(leftType, visitable.Operator, rightType);
            }
            case OR or AND:
            {
                if (leftType == BooleanType && rightType == BooleanType)
                    return BooleanType;
                
                throw new UnsupportedTypesOperation(leftType, visitable.Operator, rightType);
            }
            case EQUAL or NOT_EQUAL:
            {
                if (leftType == rightType)
                    return BooleanType;
                
                throw new UnsupportedTypesOperation(leftType, visitable.Operator, rightType);
            }
            case LESS_THAN or GREATER_THAN or LESS_THAN_OR_EQUAL or GREATER_THAN_OR_EQUAL:
            {
                if (leftType == DoubleType && rightType == DoubleType ||
                    leftType == IntType && rightType == IntType ||
                    leftType == LongType && rightType == LongType)
                {
                    return BooleanType;
                }
                
                throw new UnsupportedTypesOperation(leftType, visitable.Operator, rightType);
            }
            
            default:
                throw new UnsupportedTypesOperation(leftType, visitable.Operator, rightType);
        }
    }
    
    /// <inheritdoc cref="IVisitor{UnaryExpression, Type}" />
    public ElType Visit(UnaryExpression visitable)
    {
        var type = visitable.Expression.Accept(This);
        switch (visitable.Operator.Value)
        {
            case NOT:
            {
                if (type == BooleanType)
                    return BooleanType;
                
                throw new UnsupportedTypeOperation(type, visitable.Operator);
            }
            
            case INCREMENT or DECREMENT:
            {
                if (type == DoubleType ||
                    type == IntType ||
                    type == LongType)
                {
                    return type;
                }
                
                throw new UnsupportedTypeOperation(type, visitable.Operator);
            }
            
            default:
                throw new UnsupportedTypeOperation(type, visitable.Operator);
        }
    }
    
    /// <inheritdoc cref="IVisitor{VariableAssignmentExpression, Type}" />
    public ElType Visit(VariableAssignmentExpression visitable)
    {
        var table = _tablesStorage.GetParentTable(visitable);
        if (table.ContainsSymbol(new FunctionParameterSymbolId(visitable.Id)))
            return visitable.Id.Accept(This);
        
        var variableSymbol = table.FindSymbol(new VariableSymbolId(visitable.Id));
        if (variableSymbol is null)
            throw new UnknownIdentifierExpression(visitable.Id);
        
        variableSymbol.IsInitialized = true;
        return visitable.Id.Accept(This);
    }
    
    /// <inheritdoc cref="IVisitor{IdentifierExpression, Type}" />
    public ElType Visit(IdentifierExpression visitable)
    {
        var scope = visitable.Scope.ShouldBeInitialized();
        var table = _tablesStorage[scope];
        
        if (table.FindSymbol(new VariableSymbolId(visitable.Name)) is { } variableSymbol)
        {
            if (!variableSymbol.IsInitialized)
                throw new VariableShouldBeInitializedBeforeUsage(variableSymbol);
            
            return variableSymbol.Type;
        }
        
        if (table.FindSymbol(new FunctionParameterSymbolId(visitable.Name)) is { } parameterSymbol)
            return parameterSymbol.Type;
        
        throw new UnknownIdentifierExpression(visitable);
    }
    
    /// <inheritdoc cref="IVisitor{LiteralExpression, Type}" />
    public ElType Visit(LiteralExpression visitable)
    {
        return visitable.Accept(_typesBuilder);
    }
}