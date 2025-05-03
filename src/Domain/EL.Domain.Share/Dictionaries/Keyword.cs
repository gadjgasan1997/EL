using EL.Domain.Share.SeedWork;

namespace EL.Domain.Share.Dictionaries;

/// <summary>
/// Ключевое слово
/// </summary>
public class Keyword : Enumeration
{
    public static Keyword Namespace { get; } = new("namespace");
    public static Keyword Class { get; } = new("class");
    public static Keyword If { get; } = new("if");
    public static Keyword Else { get; } = new("else");
    public static Keyword While { get; } = new("while");
    public static Keyword For { get; } = new("for");
    public static Keyword Break { get; } = new("break");
    public static Keyword Continue { get; } = new("continue");
    public static Keyword Return { get; } = new("return");
    
    private Keyword(string value) : base(value)
    { }
}