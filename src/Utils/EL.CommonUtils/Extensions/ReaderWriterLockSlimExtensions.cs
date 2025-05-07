namespace EL.CommonUtils.Extensions;

/// <summary>
/// Методы расширения для <see cref="ReaderWriterLockSlim" />
/// </summary>
public static class ReaderWriterLockSlimExtensions
{
    /// <summary>
    /// Использовать блокировку для чтения
    /// </summary>
    /// <param name="readerWriterLockSlim"></param>
    /// <returns></returns>
    public static DisposeActionWrapper UseReadLock(this ReaderWriterLockSlim readerWriterLockSlim)
    {
        readerWriterLockSlim.CheckNotNull();
        
        readerWriterLockSlim.EnterReadLock();
        return new DisposeActionWrapper(readerWriterLockSlim.ExitReadLock);
    }
    
    /// <summary>
    /// Использовать блокировку для записи
    /// </summary>
    /// <param name="readerWriterLockSlim"></param>
    /// <returns></returns>
    public static DisposeActionWrapper UseWriteLock(this ReaderWriterLockSlim readerWriterLockSlim)
    {
        readerWriterLockSlim.CheckNotNull();
        
        readerWriterLockSlim.EnterWriteLock();
        return new DisposeActionWrapper(readerWriterLockSlim.ExitWriteLock);
    }
}