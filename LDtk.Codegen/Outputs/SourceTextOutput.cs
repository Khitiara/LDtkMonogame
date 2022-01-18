using System.Collections.Generic;
using LDtk.Codegen.CompilationUnits;
using LDtk.Codegen.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace LDtk.Codegen.Outputs;

public class SourceTextOutput
{
    public SourceTextOutput(GeneratorExecutionContext context, bool singleFile, string filename)
    {
        Context = context;
        Filename = filename;
        SingleFile = singleFile;
    }

    private GeneratorExecutionContext Context { get; }
    private bool SingleFile { get; }

    public void OutputCode(IEnumerable<CompilationUnitFragment> fragments, LdtkGeneratorContext ctx)
    {
        if (SingleFile)
        {
            CompilationUnit cu = new(Filename, ctx.CodeSettings.Namespace, fragments);

            CompilationUnitSource source = new(ctx.CodeSettings);
            cu.Render(source);

            Context.AddSource(Filename, SourceText.From(source.GetSourceCode()));
        }
        else
        {
            foreach (CompilationUnitFragment fragment in fragments)
            {
                CompilationUnit cuFile = new(classNamespace: ctx.CodeSettings.Namespace, name: fragment.Name,
                    fragments: new[] { fragment });
                CompilationUnitSource source = new(ctx.CodeSettings);
                cuFile.Render(source);

                Context.AddSource(fragment.Name, SourceText.From(source.GetSourceCode()));
            }
        }
    }

    public string Filename { get; set; }
}
