using System;
using System.Text.Json;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace LDtk.ContentPipeline.Level;

[ContentTypeWriter]
public class LDtkLevelWriter : ContentTypeWriter<LDtkLevel>
{
    protected override void Write(ContentWriter output, LDtkLevel json)
    {
        output.Write(JsonSerializer.Serialize(json, LDtkWorld.SerializeOptions));
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "LDtk.ContentPipeline.LDtkLevelReader, LDtk";
    }
}
