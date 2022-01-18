﻿using System;
using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace LDtk.ContentPipeline.Level;

[ContentImporter(".ldtkl", DisplayName = "LDtk Level Importer", DefaultProcessor = "LDtkLevelProcessor")]
public class LDtkLevelImporter : ContentImporter<string>
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
