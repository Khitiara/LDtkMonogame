
using System.Text.Json;
using Microsoft.Xna.Framework.Content;

namespace LDtk.ContentPipeline;
/// <summary>
/// LDtkLevelReader
/// </summary>
public class LDtkLevelReader : ContentTypeReader<LDtkLevel>
{
    /// <summary>
    /// Read
    /// </summary>
    protected override LDtkLevel Read(ContentReader input, LDtkLevel? existingInstance)
    {
        return (existingInstance ?? JsonSerializer.Deserialize<LDtkLevel>(input.ReadString(), LDtkWorld.SerializeOptions))!;
    }
}
