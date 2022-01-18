#pragma warning disable IDE0057

using System;
using LDtk.Codegen.CompilationUnits;

namespace LDtk.Codegen.Core;

public class LdtkTypeConverter
{
    public LdtkTypeConverter() { }

    public LdtkTypeConverter(bool pointAsVector2) {
        PointAsVector2 = pointAsVector2;
    }

    public bool PointAsVector2 { get; set; }

    public virtual string? GetArrayImport()
    {
        return null;
    }

    protected static string GetCSharpTypeFor(string ldtkType)
    {
        return ldtkType.StartsWith("LocalEnum", StringComparison.Ordinal)
            ? ldtkType.Substring(10)
            : ldtkType switch
            {
                "Int" => "int",
                "Float" => "float",
                "Bool" => "bool",
                "Point" => "Point",
                "Color" => "Color",
                _ => "string",
            };
    }

    public virtual string GetDeclaringTypeFor(FieldDefinition fieldDefinition, LdtkGeneratorContext ctx)
    {
        string baseType = fieldDefinition.Type;
        if (fieldDefinition.IsArray)
        {
            int size = baseType.Length - 1 - 6;
            baseType = baseType.Substring(6, size);
        }

        string declType = GetCSharpTypeFor(baseType);

        if (declType == "Point" && PointAsVector2)
        {
            declType = "Vector2";
        }

        if (fieldDefinition.IsArray)
        {
            declType += "[]";
        }

        return declType;
    }

    public CompilationUnitField ToCompilationUnitField(FieldDefinition fieldDefinition, LdtkGeneratorContext ctx) =>
        new(fieldDefinition.Identifier, GetDeclaringTypeFor(fieldDefinition, ctx),
            fieldDefinition.IsArray ? GetArrayImport() : null,
            CompilationUnitField.FieldVisibility.Public);
}
#pragma warning restore IDE0057
