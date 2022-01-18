using System;
using System.Text.Json;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace LDtk.ContentPipeline.World;

[ContentTypeWriter]
public class LDtkWorldWriter : ContentTypeWriter<LDtkWorld>
{
    protected override void Write(ContentWriter output, LDtkWorld json)
    {
        output.Write(JsonSerializer.Serialize(json, LDtkWorld.SerializeOptions));
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "LDtk.ContentPipeline.LDtkFileReader, LDtk";
    }
}
