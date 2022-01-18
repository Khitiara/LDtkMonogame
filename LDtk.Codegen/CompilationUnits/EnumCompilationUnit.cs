
using System.Collections.Generic;

namespace LDtk.Codegen.CompilationUnits;
public class EnumCompilationUnit : CompilationUnitFragment
{
    public EnumCompilationUnit(string name, List<string> literals) : base(name) {
        Literals = literals;
    }

    public List<string> Literals { get; }

    public override void Render(CompilationUnitSource source)
    {
        source.AddLine($"public enum {Name}");
        source.StartBlock();

        foreach (string literal in Literals)
        {
            source.AddLine($"{literal},");
        }

        source.EndBlock();
    }
}
