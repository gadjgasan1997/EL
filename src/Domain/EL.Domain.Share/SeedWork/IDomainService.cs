namespace EL.Domain.Share.SeedWork;

/// <summary>
/// Доменный сервис
/// </summary>
public interface IDomainService;

/// <summary>
/// Доменный сервис с уровнем жизни Singleton
/// </summary>
public interface ISingletonService : IDomainService;

/// <summary>
/// Доменный сервис с уровнем жизни Transient
/// </summary>
public interface ITransientService : IDomainService;