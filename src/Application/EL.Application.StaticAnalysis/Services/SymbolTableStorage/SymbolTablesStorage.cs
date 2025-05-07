using EL.Domain.IR.SymbolTable;
using EL.Domain.Frontend.Parser;
using EL.CommonUtils.Extensions;

namespace EL.Application.StaticAnalysis.Services.SymbolTableStorage;

/// <inheritdoc cref="ISymbolTablesStorage" />
internal class SymbolTablesStorage : ISymbolTablesStorage
{
    private readonly ReaderWriterLockSlim _locker = new();
    private readonly Dictionary<Guid, ISymbolTable> _symbolTables = [];
    
    /// <inheritdoc cref="ISymbolTablesStorage.this" />
    public ISymbolTable this[Scope scope]
    {
        get
        {
            using var _ = _locker.UseReadLock();
            return _symbolTables[scope.Id];
        }
    }
    
    /// <inheritdoc cref="ISymbolTablesStorage.GetOptionTable" />
    public ISymbolTable? GetOptionTable(Scope scope)
    {
        using var _ = _locker.UseReadLock();
        return _symbolTables.GetValueOrDefault(scope.Id);
    }
    
    /// <inheritdoc cref="ISymbolTablesStorage.Init" />
    public void Init(Scope scope, ISymbolTable symbolTable)
    {
        using var _ = _locker.UseWriteLock();
        _symbolTables.TryAdd(scope.Id, symbolTable);
    }
}