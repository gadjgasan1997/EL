namespace EL.Domain.Share.SeedWork;

/// <summary>
/// Перечисление
/// </summary>
public class Enumeration
{
    /// <summary>
    /// Значение
    /// </summary>
    public string Value { get; }
    
    protected Enumeration(string value)
    {
        Value = value;
    }
}