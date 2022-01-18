using System;
using System.Text.Json;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace LDtk.ContentPipeline.World;

[ContentProcessor(DisplayName = "LDtk World Processor")]
public class LDtkWorldProcessor : ContentProcessor<string, LDtkWorld>
{
    public override LDtkWorld Process(string input, ContentProcessorContext context)
    {
        try
        {
            return JsonSerializer.Deserialize<LDtkWorld>(input, LDtkWorld.SerializeOptions);
        }
        catch (Exception ex)
        {
            context.Logger.LogImportantMessage(ex.Message);
            throw;
        }

    }
}
