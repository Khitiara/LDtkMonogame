using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LDtk.Codegen.Core;
using LDtk.Codegen.Outputs;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace LDtk.Codegen;

[Generator]
public class LdtkSourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    { }

    private IEnumerable<(AdditionalText, LdtkGeneratorContext, bool)> GetTargets(GeneratorExecutionContext context)
    {
        foreach (AdditionalText file in context.AdditionalFiles)
        {
            if (file is null || !Path.GetExtension(file.Path).Equals(".ldtk", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            AnalyzerConfigOptions options = context.AnalyzerConfigOptions.GetOptions(file);
            LdtkGeneratorContext genCtx = new(options.TryGetValue("build_metadata.additionalfiles.LevelClassName", out string? lcn)
                ? lcn
                : "LDtkLevelData")
            {
                CodeSettings =
                {
                    Namespace = options.TryGetValue("build_metadata.additionalfiles.Namespace",
                        out string? ns)
                        ? ns
                        : "LDtkTypes"
                }
            };

            yield return (file, genCtx,
                !options.TryGetValue("build_metadata.additionalfiles.SingleFile", out string? singleFile) ||
                !bool.TryParse(singleFile, out bool sf) || sf);
        }
    }

    public void Execute(GeneratorExecutionContext context)
    {
        foreach ((AdditionalText text, LdtkGeneratorContext ctx, bool singleFile) in GetTargets(context))
        {
            LDtkWorld world = JsonSerializer.Deserialize<LDtkWorld>(text.GetText()!.ToString(), (JsonSerializerOptions)new())!;
            SourceTextOutput output = new(context, singleFile, Path.GetFileNameWithoutExtension(text.Path));
            LdtkCodeGenerator.GenerateCode(world, ctx, output);            
        }
    }
}
