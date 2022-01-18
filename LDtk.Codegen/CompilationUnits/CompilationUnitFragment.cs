namespace LDtk.Codegen.CompilationUnits;

public abstract class CompilationUnitFragment
{
    protected CompilationUnitFragment(string name) {
        Name = name;
    }
    public string Name { get; }

    public abstract void Render(CompilationUnitSource source);
}
