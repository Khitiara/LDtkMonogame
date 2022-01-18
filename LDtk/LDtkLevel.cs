using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using LDtk.Exceptions;
using Microsoft.Xna.Framework;

namespace LDtk;

public partial class LDtkLevel
{
    /// <summary>
    /// The parent world of this level
    /// </summary>
    [JsonIgnore]
    public LDtkWorld Parent { get; set; } = null!;

    /// <summary>
    /// World Position of the level in pixels
    /// </summary>
    [JsonIgnore]
    public Point Position => new(WorldX, WorldY);

    /// <summary>
    /// World size of the level in pixels
    /// </summary>
    [JsonIgnore]
    public Point Size => new(PxWid, PxHei);

    /// <summary>
    /// Gets an intgrid with the <paramref name="identifier"/> in a <see cref="LDtkLevel"/>
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns><see cref="LDtkIntGrid"/></returns>
    /// <exception cref="NotImplementedException"></exception>
    public LDtkIntGrid GetIntGrid(string identifier)
    {
        // Render Tile, Auto and Int grid layers
        LayerInstance layer =
            LayerInstances?.FirstOrDefault(l => l._Identifier == identifier && l._Type == LayerType.IntGrid) ??
            throw new IntGridNotFoundException($"{identifier} not found.");

        IntGridValueDefinition[] intgridValues = Parent.GetIntgridValueDefinitions(layer._Identifier);
        Dictionary<int, Color> colors = intgridValues.ToDictionary(t => t.Value, t => t.Color);

        LDtkIntGrid intGrid = new(new int[layer._CWid, layer._CHei], Position, layer._GridSize, colors);

        if (layer.IntGridCsv == null)
        {
            throw new IntGridNotFoundException($"{identifier} not found.");
        }

        for (int j = 0; j < layer.IntGridCsv.Length; j++)
        {
            int y = j / layer._CWid;
            int x = j - (y * layer._CWid);
            intGrid.Values[x, y] = layer.IntGridCsv[j];
        }

        return intGrid;
    }

    /// <summary>
    /// Gets the first entity it finds of type T in the current level
    /// </summary>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public T GetEntity<T>()
        where T : new() =>
        ParseEntities<T>(typeof(T).Name).FirstOrDefault() ??
        throw new EntityNotFoundException($"Could not find entity with identifier {typeof(T).Name}");

    /// <summary>
    /// Gets a collection of entities of type <typeparamref name="T"/> in the current level
    /// </summary>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public IEnumerable<T> GetEntities<T>()
        where T : new() =>
        ParseEntities<T>(typeof(T).Name);

    /// <summary>
    /// Gets a collection of entities of type <typeparamref name="T"/> with <paramref name="identifier"/> in the current level
    /// </summary>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public IEnumerable<T> GetEntities<T>(string identifier)
        where T : new()
    {
        return ParseEntities<T>(identifier);
    }

    /// <summary>
    /// Gets the custom fields of the level
    /// </summary>
    /// <typeparam name="T">The custom level type generated from compiling the level</typeparam>
    /// <exception cref="FieldNotFoundException"></exception>
    /// <returns>Custom Fields for this level</returns>
    public T GetCustomFields<T>()
        where T : new()
    {
        T levelFields = new();

        LDtkFieldParser.ParseCustomLevelFields(levelFields, FieldInstances);

        return levelFields;
    }

    /// <summary>
    /// Check if point is inside of a level
    /// </summary>
    /// <returns>True if point is inside level</returns>
    public bool Contains(Point point)
    {
        (int x, int y) = point;
        return
            x >= Position.X &&
            y >= Position.Y &&
            x <= Position.X + Size.X &&
            y <= Position.Y + Size.Y;
    }

    /// <summary>
    /// Check if point is inside of a level
    /// </summary>
    /// <returns>True if point is inside level</returns>
    public bool Contains(Vector2 point)
    {
        (float x, float y) = point;
        return
            x >= Position.X &&
            y >= Position.Y &&
            x <= Position.X + Size.X &&
            y <= Position.Y + Size.Y;
    }

    private IEnumerable<T> ParseEntities<T>(string identifier)
        where T : new() =>
        LayerInstances?.FirstOrDefault(l => l._Type == LayerType.Entities)?.EntityInstances
            ?.Where(i => i._Identifier == identifier)
            .Select(i =>
            {
                T e = new();
                LDtkFieldParser.ParseBaseEntityFields(e, i, this);
                LDtkFieldParser.ParseCustomEntityFields(e, i.FieldInstances, this);
                return e;
            }) ?? Enumerable.Empty<T>();
}
