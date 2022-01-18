using System;

namespace LDtk.Codegen.Core;

public class CodeSettings
{
    public string? Namespace { get; set; }
    public string? GeneratedFileHeader { get; set; } = "// This file was automatically generated, any modifications will be lost!";
    public string IndentString { get; set; } = "    ";
    public string NewLine { get; set; } = Environment.NewLine;
}

public class LdtkGeneratorContext
{
    public LdtkGeneratorContext(string levelClassName) {
        LevelClassName = levelClassName;
    }

    public LdtkTypeConverter TypeConverter { get; set; } = new();
    public LdtkCodeCustomizer? CodeCustomizer { get; set; } = new();
    public CodeSettings CodeSettings { get; set; } = new();

    public string LevelClassName { get; set; }
}
#pragma warning restore CS1591
