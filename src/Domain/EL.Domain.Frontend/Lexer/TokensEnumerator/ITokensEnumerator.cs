using EL.Domain.Share.Dictionaries;

namespace EL.Domain.Frontend.Lexer.TokensEnumerator;

/// <summary>
/// Енумератор для прохода по токенам
/// </summary>
public interface ITokensEnumerator : IEnumerator<Token>
{
    /// <summary>
    /// Ожидает, что текущий токен имеет тип <paramref name="type" />.
    /// Дополнительно проверяет значение токена на совпадение с <paramref name="value" />, если оно указано.
    /// В случае успешной проверки возвращает токен и устанавливает текущим следующий.
    /// </summary>
    /// <param name="type">Тип</param>
    /// <param name="value">Значение</param>
    /// <returns>Токен</returns>
    Token Expect(TokenType type, string? value = null);
    
    /// <summary>
    /// Ожидает, что текущий токен является одним из типов <paramref name="types" />.
    /// В случае успешной проверки возвращает токен и устанавливает текущим следующий.
    /// </summary>
    /// <param name="types">Типы</param>
    /// <returns>Токен</returns>
    Token ExpectAny(TokenType[] types);
    
    /// <summary>
    /// Ожидает, что текущий токен является оператором.
    /// Дополнительно проверяет оператор на совпадение с <paramref name="operator" />, если он указан.
    /// В случае успешной проверки возвращает токен и устанавливает текущим следующий.
    /// </summary>
    /// <param name="operator">Оператор</param>
    /// <returns>Токен</returns>
    Token ExpectOperator(Operator? @operator = null);
    
    /// <summary>
    /// Ожидает, что текущий токен является ключевым словом.
    /// Дополнительно проверяет ключевое слово на совпадение с <paramref name="keyword" />, если оно указано.
    /// В случае успешной проверки возвращает токен и устанавливает текущим следующий.
    /// </summary>
    /// <param name="keyword">Ключевое слово</param>
    /// <returns>Токен</returns>
    Token ExpectKeyword(Keyword keyword);
    
    /// <summary>
    /// Определяет, что текущий токен имеет тип <paramref name="type" />.
    /// Дополнительно проверяет значение токена на совпадение с <paramref name="value" />, если оно указано.
    /// </summary>
    /// <param name="type">Тип</param>
    /// <param name="value">Значение</param>
    /// <returns>Признак</returns>
    bool CurrentIs(TokenType type, string? value = null);
    
    /// <summary>
    /// Определяет, что текущий токен является одним из типов <paramref name="types" />.
    /// </summary>
    /// <param name="types">Типы</param>
    /// <returns>Признак</returns>
    bool CurrentIsAny(TokenType[] types);
    
    /// <summary>
    /// Определяет, что текущий токен является оператором
    /// </summary>
    /// <param name="operator">Оператор</param>
    /// <returns>Признак</returns>
    bool CurrentIsOperator(Operator? @operator = null);
    
    /// <summary>
    /// Определяет, что текущий токен является ключевым словом
    /// </summary>
    /// <param name="keyword">Ключевое слово</param>
    /// <returns>Признак</returns>
    bool CurrentIsKeyword(Keyword? keyword = null);
    
    /// <summary>
    /// Проходится по всем переданным действиям, выполняя первое из списка
    /// в случае совпадения указанного в <paramref name="tuples.type" />
    /// типа токена и типа текущего токена.
    /// </summary>
    /// <param name="tuples">Список действий</param>
    /// <typeparam name="T">Тип результата</typeparam>
    /// <returns>Результат</returns>
    T ForCurrent<T>(params (TokenType type, Func<TokenType, T> func)[] tuples);
    
    /// <summary>
    /// Проходится по всем переданным действиям, выполняя первое из списка
    /// в случае совпадения указанного в <paramref name="tuples.keyword" />
    /// типа токена и типа текущего токена.
    /// </summary>
    /// <param name="tuples">Список действий</param>
    /// <typeparam name="T">Тип результата</typeparam>
    /// <returns>Результат</returns>
    T ForCurrentKeyword<T>(params (Keyword keyword, Func<T> func)[] tuples);
}