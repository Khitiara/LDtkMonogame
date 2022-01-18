using System;
using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace LDtk.ContentPipeline.World;

[ContentImporter(".ldtk", DisplayName = "LDtk World Importer", DefaultProcessor = "LDtkWorldProcessor")]
public class LDtkWorldImporter : ContentImporter<string>
{
    public override string Import(string filename, ContentImporterContext context)
    {
        try
        {
            return File.ReadAllText(Path.GetFullPath(filename));
        }
        catch (Exception e)
        {
            context.Logger.LogImportantMessage(e.StackTrace);
            throw;
        }
    }
}
