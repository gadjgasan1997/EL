namespace EL.Domain.Share.Dictionaries;

/// <summary>
/// Типы токенов
/// </summary>
public static class TokenTypes
{
    /// <summary>
    /// Поток
    /// </summary>
    public static IEnumerable<TokenType> Stream
    {
        get
        {
            return [];
        }
    }
    
    /// <summary>
    /// Тип токена
    /// </summary>
    public readonly record struct TokenType
    {
        internal TokenType(
            string Tag,
            string Pattern,
            int Priority,
            bool CanIgnore = false)
        {
            this.Tag = Tag;
            this.Pattern = Pattern;
            this.Priority = Priority;
            this.CanIgnore = CanIgnore;
        }
        
        /// <summary>
        /// Тег
        /// </summary>
        public string Tag { get; }
        
        /// <summary>
        /// Паттерн
        /// </summary>
        public string Pattern { get; }
        
        /// <summary>
        /// Приоритет
        /// </summary>
        public int Priority { get; }
        
        /// <summary>
        /// Признак, может ли токен быть проигнорирован
        /// </summary>
        public bool CanIgnore { get; }
    }
}