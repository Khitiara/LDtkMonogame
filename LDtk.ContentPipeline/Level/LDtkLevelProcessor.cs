using System;
using System.Text.Json;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace LDtk.ContentPipeline.Level;

[ContentProcessor(DisplayName = "LDtk Level Processor")]
public class LDtkLevelProcessor : ContentProcessor<string, LDtkLevel>
{
    public override LDtkLevel Process(string input, ContentProcessorContext context)
    {
        try
        {
            return JsonSerializer.Deserialize<LDtkLevel>(input, LDtkWorld.SerializeOptions);
        }
        catch (Exception ex)
        {
            context.Logger.LogImportantMessage(ex.Message);
            throw;
        }
    }
}
