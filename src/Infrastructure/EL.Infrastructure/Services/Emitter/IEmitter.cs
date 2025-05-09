using System.Reflection.Emit;
using EL.Domain.Share.Dictionaries;

namespace EL.Infrastructure.Services.Emitter;

public interface IEmitter
{
    void Emit(
        PersistedAssemblyBuilder assemblyBuilder,
        string assemblyName,
        string outputPath,
        FileExtension outputType);
}