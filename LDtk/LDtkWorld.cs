using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using LDtk.Exceptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace LDtk;

public partial class LDtkWorld
{
    /// <summary>
    /// Size of the world grid in pixels.
    /// </summary>
    [JsonIgnore]
    public Point WorldGridSize => new(WorldGridWidth, WorldGridHeight);

    /// <summary>
    /// The absolute folder that the world is located in.
    /// Used to absolute relative addresses of textures
    /// </summary>
    [JsonIgnore]
    public string RootFolder { get; set; } = null!;

    /// <summary>
    /// The content manager used if you are using the contentpipeline
    /// </summary>
    public ContentManager? Content { get; set; }

    /// <summary>
    /// Loads the ldtk world file from disk directly
    /// </summary>
    /// <param name="filePath">Path to the .ldtk file excludeing file extension</param>
    /// <returns>LDtkWorld</returns>
    public static LDtkWorld LoadWorld(string filePath)
    {
        LDtkWorld world = JsonSerializer.Deserialize<LDtkWorld>(File.ReadAllText(filePath), SerializeOptions)!;

        world.RootFolder = Path.GetFullPath(Path.GetDirectoryName(filePath) ?? string.Empty);
        return world;
    }

    /// <summary>
    /// Loads the ldtk world file from disk directly
    /// </summary>
    /// <param name="filePath">Path to the .ldtk file excludeing file extension</param>
    /// <param name="content">The optional XNA content manager if you are using the content pipeline</param>
    /// <returns>LDtkWorld</returns>
    public static LDtkWorld LoadWorld(string filePath, ContentManager content)
    {
        LDtkWorld world = content.Load<LDtkWorld>(filePath);
        world.Content = content;
        world.RootFolder = Path.GetDirectoryName(filePath) ?? string.Empty;

        return world;
    }

    /// <summary>
    /// Loads the ldtkl world file from disk directly or from the embeded one depending on if externalLevels is set
    /// </summary>
    /// <param name="identifier">The Level identifier</param>
    /// <returns><see cref="LDtkLevel"/></returns>
    /// <exception cref="LevelNotFoundException"></exception>
    public LDtkLevel LoadLevel(string identifier)
    {
        LDtkLevel level = Levels.FirstOrDefault(l => l.Identifier == identifier) ??
                          throw new LevelNotFoundException($"Could not find {identifier} Level in {this}.");

        if (ExternalLevels)
        {
            string path = Path.Join(RootFolder, level.ExternalRelPath);

            level = Content?.Load<LDtkLevel>(path.Replace(".ldtkl", "")) ??
                    JsonSerializer.Deserialize<LDtkLevel>(File.ReadAllText(path), SerializeOptions)!;
        }

        level.Parent = this;
        return level;
    }

    /// <summary>
    /// Loads the ldtkl world file from disk directly or from the embeded one depending on if externalLevels is set
    /// </summary>
    /// <param name="uid">The Levels uid</param>
    /// <returns><see cref="LDtkLevel"/></returns>
    /// <exception cref="LevelNotFoundException"></exception>
    public LDtkLevel LoadLevel(int uid)
    {
        LDtkLevel level = Levels.FirstOrDefault(l => l.Uid == uid) ??
                          throw new LevelNotFoundException($"Could not find {uid} Level in {this}.");

        if (ExternalLevels)
        {
            string path = Path.Join(RootFolder, level.ExternalRelPath);

            level = Content?.Load<LDtkLevel>(path.Replace(".ldtkl", "")) ??
                    JsonSerializer.Deserialize<LDtkLevel>(File.ReadAllText(path), SerializeOptions)!;
        }

        level.Parent = this;
        return level;
    }

    /// <summary>
    /// Gets the entity definition form a uid
    /// </summary>
    /// <param name="uid"></param>
    /// <returns>EntityDefinition</returns>
    /// <exception cref="NotImplementedException"></exception>
    public EntityDefinition? GetEntityDefinitionFromUid(int uid) => Defs.Entities.FirstOrDefault(t => t.Uid == uid);

    /// <summary>
    /// Gets the intgrid value definitions
    /// </summary>
    /// <param name="identifier">leyer identifier</param>
    /// <returns></returns>
    /// <exception cref="FieldNotFoundException"></exception>
    public IntGridValueDefinition[] GetIntgridValueDefinitions(string identifier) =>
        Defs.Layers.FirstOrDefault(l => l.Identifier == identifier && l._Type == LayerType.IntGrid)
            ?.IntGridValues ?? throw new FieldNotFoundException();

    /// <summary>
    /// Gets a collection of entities of type <typeparamref name="T"/> in the current level
    /// </summary>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public T GetEntity<T>()
        where T : new() =>
        GetEntities<T>().FirstOrDefault() ??  throw new EntityNotFoundException($"Could not find entity with identifier {typeof(T).Name}");

    /// <summary>
    /// Gets an entity of type <typeparamref name="T"/> in the current level
    /// </summary>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public IEnumerable<T> GetEntities<T>()
        where T : new() {
        return Levels.SelectMany(l => l.GetEntities<T>());
    }
}
