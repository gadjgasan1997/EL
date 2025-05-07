using EL.CommonUtils.Extensions;

namespace EL.Domain.IR.SymbolTable;

/// <inheritdoc cref="ISymbolTable" />
public class SymbolTable : ISymbolTable
{
    private readonly ReaderWriterLockSlim _locker = new();
    private readonly Dictionary<ISymbolId<ISymbol>, ISymbol> _symbols = [];
    
    public SymbolTable(ISymbolTable? parent = null)
    {
        Parent = parent;
    }
    
    /// <inheritdoc cref="ISymbolTable.Parent" />
    public ISymbolTable? Parent { get; }
    
    /// <inheritdoc cref="ISymbolTable.AddSymbol" />
    public void AddSymbol(ISymbol symbol)
    {
        using var _ = _locker.UseWriteLock();
        _symbols[symbol.Id] = symbol;
    }
    
    /// <inheritdoc cref="ISymbolTable.FindSymbol{TSymbol}" />
    public TSymbol? FindSymbol<TSymbol>(ISymbolId<TSymbol> id)
        where TSymbol : class, ISymbol
    {
        using (var _ = _locker.UseReadLock())
        {
            if (_symbols.TryGetValue(id, out var symbol))
                return symbol as TSymbol;
        }
        
        ISymbolTable? table = this;
        while ((table = table.Parent) is not null)
        {
            if (table.FindSymbol(id) is { } symbol)
                return symbol;
        }
        
        return null;
    }
    
    /// <inheritdoc cref="ISymbolTable.ContainsSymbol" />
    public bool ContainsSymbol(ISymbolId<ISymbol> id) => FindSymbol(id) is not null;
}