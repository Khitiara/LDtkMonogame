
using System.Collections.Generic;

namespace LDtk.Codegen.CompilationUnits;

public class ClassCompilationUnit : CompilationUnitFragment
{
    public ClassCompilationUnit(string name, List<CompilationUnitField> fields) : base(name){
        Fields = fields;
    }

    public string? BaseClass { get; set; }
    public List<CompilationUnitField> Fields { get; }

    public override void Render(CompilationUnitSource source)
    {
        string extends = "";
        if (BaseClass != null)
        {
            extends = $" : {BaseClass}";
        }

        source.AddLine($"using LDtk;");
        source.AddLine($"using Microsoft.Xna.Framework;");
        source.AddLine("");
        source.AddLine($"public partial class {Name}{extends}");
        source.StartBlock();

        foreach (CompilationUnitField field in Fields)
        {
            field.Render(source);
        }

        source.EndBlock();
    }
}
