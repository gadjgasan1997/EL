using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using EL.Domain.Share.Dictionaries;
using EL.Domain.Frontend.Lexer.Services.Lexer;
using EL.Domain.Frontend.Lexer.TokensEnumerator;
using EL.Domain.Frontend.Parser.Ast;
using EL.Domain.Frontend.Parser.Services.Parser;
using EL.Infrastructure.Services.Emitter;
using EL.Infrastructure.Services.SourceCodeProvider;
using EL.Application.CodeGeneration.Services.CodeGenerator;
using EL.Application.StaticAnalysis.Services.StaticAnalyzer;

namespace EL.Compiler;

/// <summary>
/// Заглушка для компиляции примеров
/// </summary>
internal class DumpCompiler(
    ISourceCodeProvider sourceCodeProvider,
    ILexer lexer,
    ICodeGenerator codeGenerator,
    IStaticAnalyzer staticAnalyzer,
    IEmitter emitter,
    IServiceProvider serviceProvider,
    ILogger<DumpCompiler> logger)
    : ICompiler
{
    private static string[] RuntimeAssemblies { get; } =
        Directory.GetFiles(
            RuntimeEnvironment.GetRuntimeDirectory(),
            $"*.{FileExtension.Library.Value}");
    
    /// <inheritdoc cref="ICompiler.Compile" />
    public int Compile(
        string projectDirectory,
        string[] filesRelativePaths,
        string compiledAssemblyName,
        string? compiledAssemblyOutputPath,
        bool? compileInParallel,
        FileExtension outputType)
    {
        var files = sourceCodeProvider
            .GetFilesContent(projectDirectory, filesRelativePaths)
            .ToList();
        
        if (files.Count == 0)
            return ExitCodes.SUCCESS;
        
        MetadataAssemblyResolver resolver = new PathAssemblyResolver(RuntimeAssemblies);
        var context = new MetadataLoadContext(resolver, "System.Runtime");
        var assemblyBuilder = new PersistedAssemblyBuilder(new AssemblyName(compiledAssemblyName), context.CoreAssembly!);
        var module = assemblyBuilder.DefineDynamicModule(compiledAssemblyName);
        
        try
        {
            if (compileInParallel ?? true)
                CompileInParallel(files, context, module);
            else CompileSequentially(files, context, module);
        }
        catch
        {
            return ExitCodes.ERROR;
        }
        
        compiledAssemblyOutputPath ??= Path.Combine(projectDirectory, "bin");
        
        try
        {
            emitter.Emit(assemblyBuilder, compiledAssemblyName, compiledAssemblyOutputPath, outputType);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Во время сохранения сборки '{assembly}' произошло исключение",
                compiledAssemblyName);
            
            return ExitCodes.ERROR;
        }
        
        return ExitCodes.SUCCESS;
    }
    
    private void CompileInParallel(
        List<SourceCodeFile> files,
        MetadataLoadContext context,
        ModuleBuilder module)
    {
        var filesTokens = new ConcurrentBag<FileContent<ITokensEnumerator>>();
        Parallel.ForEach(files, file => filesTokens.Add(GetTokens(file)));
        
        var filesAst = new ConcurrentBag<FileContent<IAbstractSyntaxTree>>();
        Parallel.ForEach(filesTokens, file => filesAst.Add(BuildAst(file)));
        
        Parallel.ForEach(filesAst, Analyze);
        Parallel.ForEach(filesAst, file => GenerateCode(file, context, module));
    }
    
    private void CompileSequentially(
        List<SourceCodeFile> files,
        MetadataLoadContext context,
        ModuleBuilder module)
    {
        var filesTokens = files.Select(GetTokens).ToList();
        var filesAst = filesTokens.Select(BuildAst).ToList();
        
        foreach (var file in filesAst)
            Analyze(file);
        
        foreach (var file in filesAst)
            GenerateCode(file, context, module);
    }
    
    private FileContent<ITokensEnumerator> GetTokens(SourceCodeFile file)
    {
        try
        {
            var tokens = lexer.GetTokens(file.SourceCode);
            return new FileContent<ITokensEnumerator>(file.FilePath, tokens);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Во время получения списка токенов из файла '{path}' произошло исключение",
                file.FilePath);
            
            throw;
        }
    }
    
    private FileContent<IAbstractSyntaxTree> BuildAst(FileContent<ITokensEnumerator> fileContent)
    {
        try
        {
            var parser = serviceProvider.GetRequiredService<IParser>();
            var ast = parser.Parse(fileContent.Content);
            return new FileContent<IAbstractSyntaxTree>(fileContent.FilePath, ast);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Во время формирования АСТ из списка токенов для файла '{path}' произошло исключение",
                fileContent.FilePath);
            
            throw;
        }
    }
    
    private void Analyze(FileContent<IAbstractSyntaxTree> fileContent)
    {
        try
        {
            staticAnalyzer.Analyze(fileContent.Content);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Во время статического анализа кода для файла '{path}' произошло исключение",
                fileContent.FilePath);
            
            throw;
        }
    }
    
    private void GenerateCode(
        FileContent<IAbstractSyntaxTree> fileContent,
        MetadataLoadContext context,
        ModuleBuilder module)
    {
        try
        {
            codeGenerator.Generate(fileContent.Content, context, module);
            logger.LogInformation("Код для файла '{path}' был успешно скомпилирован", fileContent.FilePath);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Во время генерации кода для файла '{path}' произошло исключение",
                fileContent.FilePath);
            
            throw;
        }
    }
    
    private sealed record FileContent<T>(string FilePath, T Content);
    
    private static class ExitCodes
    {
        public const int SUCCESS = 0;
        public const int ERROR = 1;
    }
}