
using System.Collections.Generic;
using System.Linq;

namespace LDtk.Codegen.CompilationUnits;

public class CompilationUnit : CompilationUnitFragment
{
    public CompilationUnit(string name, string? classNamespace, IEnumerable<CompilationUnitFragment> fragments) : base(name) {
        ClassNamespace = classNamespace;
        Fragments = fragments;
    }

    public string? ClassNamespace { get; set; }
    public IEnumerable<CompilationUnitFragment> Fragments { get; set; }

    public override void Render(CompilationUnitSource source)
    {
        if (ClassNamespace != null)
        {
            source.AddLine($"namespace {ClassNamespace}");
            source.StartBlock();
        }

        foreach (CompilationUnitFragment fragment in Fragments)
        {
            fragment.Render(source);
        }

        if (ClassNamespace != null)
        {
            source.EndBlock();
        }
    }
}
