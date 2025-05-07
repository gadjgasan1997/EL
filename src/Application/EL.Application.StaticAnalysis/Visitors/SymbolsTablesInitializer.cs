using EL.Domain.IR.Symbols;
using EL.Domain.IR.Symbols.Ids;
using EL.Domain.Share.Dictionaries;
using EL.Domain.Frontend.Parser.Extensions;
using EL.Domain.Frontend.Parser.Ast;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;
using EL.Application.StaticAnalysis.Exceptions;
using EL.Application.StaticAnalysis.Extensions;
using EL.Application.StaticAnalysis.Services.SymbolTableStorage;
using EL.CommonUtils.Collections.Extensions;
using Microsoft.Extensions.DependencyInjection;
using static EL.Application.StaticAnalysis.Dictionaries.KnownVisitors;
// ReSharper disable ForCanBeConvertedToForeach

namespace EL.Application.StaticAnalysis.Visitors;

/// <summary>
/// Инициализатор таблиц символов. Отвечает за заполнение таблиц символами
/// </summary>
internal class SymbolsTablesInitializer : VisitorNoReturnBase<IAbstractSyntaxTreeNode>,
    IVisitor<ClassDeclaration>,
    IVisitor<FunctionDeclaration>,
    IVisitor<StatementsBlock>,
    IVisitor<VariableDeclaration>
{
    private readonly ISymbolTablesStorage _tablesStorage;
    private readonly IVisitor<IAbstractSyntaxTreeNode, ElType> _typesBuilder;
    
    public SymbolsTablesInitializer(
        ISymbolTablesStorage tablesStorage,
        [FromKeyedServices(TYPES_BUILDER)] IVisitor<IAbstractSyntaxTreeNode, ElType> typesBuilder)
    {
        _tablesStorage = tablesStorage;
        _typesBuilder = typesBuilder;
    }
    
    /// <inheritdoc cref="VisitorNoReturnBase{IAbstractSyntaxTreeNode}" />
    public override VisitUnit Visit(IAbstractSyntaxTreeNode visitable)
    {
        for (var i = 0; i < visitable.Count; i++)
            visitable[i].Accept(This);
        
        return default;
    }
    
    /// <inheritdoc cref="VisitorNoReturnBase{ClassDeclaration}" />
    public VisitUnit Visit(ClassDeclaration visitable)
    {
        // Проверка на дубликаты классов
        var namespaceNode = visitable.GetRequiredClosestNode<NamespaceDeclaration>();
        var table = _tablesStorage.GetRequiredSymbolTable(namespaceNode);
        if (table.ContainsSymbol(new ClassSymbolId(visitable.Name)))
            throw new TypeHasAlreadyBeenDeclared(namespaceNode, visitable);
        
        table.AddSymbol(new ClassSymbol(visitable.Name));
        
        for (var i = 0; i < visitable.Count; i++)
            visitable[i].Accept(This);
        
        return default;
    }
    
    /// <inheritdoc cref="VisitorNoReturnBase{FunctionDeclaration}" />
    public VisitUnit Visit(FunctionDeclaration visitable)
    {
        var parameters = visitable.Parameters
            .Select(parameter => new FunctionParameterSymbol(parameter.Accept(_typesBuilder), parameter.Name))
            .ToList();
        
        // Проверка на аналогичную перегрузку
        var classNode = visitable.GetRequiredClosestNode<ClassDeclaration>();
        var parametersTypes = parameters.Select(symbol => symbol.Type).ToList();
        var classSymbolsTable = _tablesStorage.GetRequiredSymbolTable(classNode);
        if (classSymbolsTable.ContainsSymbol(new FunctionSymbolId(visitable.Name, parametersTypes)))
            throw SameOverloadHasAlreadyBeenDeclared.Create(classNode, visitable);
        
        classSymbolsTable.AddSymbol(new FunctionSymbol(visitable.Name, parameters));
        
        // Проверка на дубликаты параметров
        var functionSymbolsTable = _tablesStorage.GetRequiredSymbolTable(visitable.Body);
        parameters.ForEachWithDuplicatesCheck(
            SymbolByNameComparer.Instance,
            action: parameter => functionSymbolsTable.AddSymbol(parameter),
            onDuplicate: duplicate =>
                throw new SameParameterHasAlreadyBeenDeclared(
                    classNode, visitable, duplicate));
        
        return visitable.Body.Accept(This);
    }
    
    /// <inheritdoc cref="VisitorNoReturnBase{StatementsBlock}" />
    public VisitUnit Visit(StatementsBlock visitable)
    {
        var declarations = visitable.Where(node => node is Declaration).ToList();
        for (var i = 0; i < declarations.Count; i++)
            declarations[i].Accept(This);
        
        var statements = visitable.Where(node => node is Statement).ToList();
        for (var i = 0; i < statements.Count; i++)
            statements[i].Accept(This);
        
        return default;
    }
    
    /// <inheritdoc cref="VisitorNoReturnBase{VariableDeclaration}" />
    public VisitUnit Visit(VariableDeclaration visitable)
    {
        // Проверка на дубликаты переменных
        var table = _tablesStorage.GetParentTable(visitable);
        if (table.ContainsSymbol(new VariableSymbolId(visitable.Name))
            || table.ContainsSymbol(new FunctionParameterSymbolId(visitable.Name)))
        {
            throw new SameVariableHasAlreadyBeenDeclared(visitable);
        }
        
        var symbol = new VariableSymbol(visitable.Accept(_typesBuilder), visitable.Name)
        {
            IsInitialized = visitable.InitializeExpression is not null
        };
        
        table.AddSymbol(symbol);
        return default;
    }
}