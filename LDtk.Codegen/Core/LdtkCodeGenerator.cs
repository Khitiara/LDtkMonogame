using System.Collections.Generic;
using System.Linq;
using LDtk.Codegen.CompilationUnits;
using LDtk.Codegen.Outputs;

namespace LDtk.Codegen.Core;

public static class LdtkCodeGenerator
{
    private static IEnumerable<CompilationUnitFragment> GenerateFragments(LDtkWorld ldtkJson, LdtkGeneratorContext ctx)
    {
        foreach (EnumDefinition ed in ldtkJson.Defs.Enums)
        {
            yield return new EnumCompilationUnit(ed.Identifier,
                ed.Values.Select(evd => evd.Id).ToList());
        }

        foreach (EntityDefinition ed in ldtkJson.Defs.Entities)
        {
            ClassCompilationUnit entity = new(ed.Identifier,
                ed.FieldDefs.Select(fd => ctx.TypeConverter.ToCompilationUnitField(fd, ctx)).ToList());
            yield return entity;

            ctx.CodeCustomizer?.CustomizeEntity(entity, ed, ctx);
        }

        ClassCompilationUnit level = new(ctx.LevelClassName,
            ldtkJson.Defs.LevelFields.Select(fd => ctx.TypeConverter.ToCompilationUnitField(fd, ctx)).ToList());
        yield return level;
        
        ctx.CodeCustomizer?.CustomizeLevel(level, ldtkJson, ctx);
    }
    
    public static void GenerateCode(LDtkWorld ldtkJson, LdtkGeneratorContext ctx, SourceTextOutput output)
    {
        output.OutputCode(GenerateFragments(ldtkJson, ctx), ctx);
    }
}
