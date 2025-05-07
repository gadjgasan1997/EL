using EL.CommonUtils.Extensions;
using EL.Domain.IR.SymbolTable;
using EL.Domain.IR.Symbols.Ids;
using EL.Domain.Frontend.Parser.Extensions;
using EL.Domain.Frontend.Parser.Ast;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Declarations;
using EL.Domain.Frontend.Parser.Ast.Implementation.Nodes.Statements;
using EL.Application.StaticAnalysis.Extensions;
using EL.Application.StaticAnalysis.Services.ScopesResolver;
using EL.Application.StaticAnalysis.Services.SymbolTableStorage;
// ReSharper disable ForCanBeConvertedToForeach

namespace EL.Application.StaticAnalysis.Visitors;

/// <summary>
/// Инициализатор храналища таблиц символов. Отвечает за создание таблиц
/// </summary>
internal class TableStorageInitializer : VisitorNoReturnBase<IAbstractSyntaxTreeNode>,
    IVisitor<NamespaceDeclaration>,
    IVisitor<ClassDeclaration>,
    IVisitor<FunctionDeclaration>,
    IVisitor<StatementsBlock>
{
    private readonly IScopesResolver _scopesResolver;
    private readonly ISymbolTablesStorage _tablesStorage;
    
    public TableStorageInitializer(
        IScopesResolver scopesResolver,
        ISymbolTablesStorage tablesStorage)
    {
        _scopesResolver = scopesResolver;
        _tablesStorage = tablesStorage;
    }
    
    /// <inheritdoc cref="VisitorNoReturnBase{IAbstractSyntaxTreeNode}" />
    public override VisitUnit Visit(IAbstractSyntaxTreeNode visitable)
    {
        if (visitable.Scope is null)
        {
            var parent = visitable.Parent.CheckNotNull();
            var parentScope = parent.Scope.ShouldBeInitialized();
            visitable.SetExistingScope(parentScope);
        }
        
        for (var i = 0; i < visitable.Count; i++)
            visitable[i].Accept(This);
        
        return default;
    }
    
    /// <inheritdoc cref="VisitorNoReturnBase{NamespaceDeclaration}" />
    public VisitUnit Visit(NamespaceDeclaration visitable)
    {
        var symbolId = new NamespaceSymbolId(visitable.Name);
        var scope = _scopesResolver.Resolve(symbolId);
        visitable.SetExistingScope(scope);
        
        var symbolTable = _tablesStorage.GetOptionTable(scope) ?? new SymbolTable();
        _tablesStorage.Init(scope, symbolTable);
        
        return visitable.Body.Accept(This);
    }
    
    /// <inheritdoc cref="VisitorNoReturnBase{ClassDeclaration}" />
    public VisitUnit Visit(ClassDeclaration visitable)
    {
        var symbolId = new ClassSymbolId(visitable.Name);
        var scope = _scopesResolver.Resolve(symbolId);
        visitable.SetExistingScope(scope);
        
        var symbolTable = _tablesStorage.GetOptionTable(scope);
        if (symbolTable is null)
        {
            var parentTable = _tablesStorage.GetParentTable(visitable);
            symbolTable = new SymbolTable(parentTable);
        }
        
        _tablesStorage.Init(scope, symbolTable);
        
        for (var i = 0; i < visitable.Count; i++)
            visitable[i].Accept(This);
        
        return default;
    }
    
    /// <inheritdoc cref="VisitorNoReturnBase{FunctionDeclaration}" />
    public VisitUnit Visit(FunctionDeclaration visitable)
    {
        visitable.InitScope();
        
        var parentTable = _tablesStorage.GetParentTable(visitable);
        _tablesStorage.Init(visitable.Scope, new SymbolTable(parentTable));
        
        for (var i = 0; i < visitable.Parameters.Count; i++)
            visitable.Parameters[i].Accept(This);
        
        return visitable.Body.Accept(This);
    }
    
    /// <inheritdoc cref="VisitorNoReturnBase{StatementsBlock}" />
    public VisitUnit Visit(StatementsBlock visitable)
    {
        visitable.InitScope();
        
        var parentTable = _tablesStorage.GetParentTable(visitable);
        _tablesStorage.Init(visitable.Scope, new SymbolTable(parentTable));
        
        for (var i = 0; i < visitable.Count; i++)
            visitable[i].Accept(This);
        
        return default;
    }
}