using EL.CommonUtils.Extensions;

namespace EL.CommonUtils;

/// <summary>
/// Обертка, вызывающая переданный делегат в момент освобождения ресурсов
/// </summary>
public readonly struct DisposeActionWrapper : IDisposable
{
    private readonly Action _action;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="action">Действие, которое требуется выполнить при освобождении ресурсов</param>
    public DisposeActionWrapper(Action action)
    {
        _action = action.CheckNotNull(
            $"В конструктор типа '{typeof(DisposeActionWrapper).FullName}' " +
            $"не передан делегат (action == null)");
    }
    
    /// <inheritdoc cref="IDisposable.Dispose" />
    public void Dispose() => _action();
}