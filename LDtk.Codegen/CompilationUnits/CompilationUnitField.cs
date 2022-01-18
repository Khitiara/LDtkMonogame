using System.Globalization;

namespace LDtk.Codegen.CompilationUnits;

public class CompilationUnitField : CompilationUnitFragment
{
    public enum FieldVisibility
    {
        Private,
        Protected,
        Public,
    }

    public string? RequiredImport { get; }
    public FieldVisibility? Visibility { get; }
    public string Type { get; }

    public CompilationUnitField(string name, string type, string? requiredImport, FieldVisibility visibility) : base(name)
    {
        Type = type;
        Visibility = visibility;
        RequiredImport = requiredImport;
    }

    public CompilationUnitField(string name, string type) : base(name)
    {
        Type = type;
        Visibility = FieldVisibility.Public;
        RequiredImport = null;
    }

    public override void Render(CompilationUnitSource source)
    {
        if (RequiredImport != null)
        {
            source.Using(RequiredImport);
        }

        string vStr = "";
        if (Visibility.HasValue)
        {
            vStr = Visibility.GetValueOrDefault().ToString().ToLower(CultureInfo.InvariantCulture);
        }

        source.AddLine($"{vStr} {Type} {Name} {{ get; set; }}");
    }
}
