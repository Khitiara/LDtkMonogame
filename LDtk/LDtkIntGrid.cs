using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace LDtk;

/// <summary>
/// LDtk IntGrid
/// </summary>
public class LDtkIntGrid
{
    /// <summary>
    /// Size of a tile in pixels
    /// </summary>
    /// <value>Pixels</value>
    public int TileSize { get; set; }

    /// <summary>
    /// The underlying values of the int grid
    /// </summary>
    /// <value>Integer</value>
    public int[,] Values { get; set; }

    /// <summary>
    /// Worldspace start Position of the intgrid
    /// </summary>
    /// <value>Pixels</value>
    public Point WorldPosition { get; set; }

    internal readonly Dictionary<int, Color> colors;

    internal LDtkIntGrid(int[,] values, Point worldPosition, int tileSize, Dictionary<int, Color> colors) {
        Values = values;
        WorldPosition = worldPosition;
        TileSize = tileSize;
        this.colors = colors;
    }

    /// <summary>
    /// Gets the int value at location and return 0 if out of bounds
    /// </summary>
    /// <param name="x">X index</param>
    /// <param name="y">Y index</param>
    /// <returns>Value at position</returns>
    public long GetValueAt(int x, int y)
    {
        // Inside bounds
        return x >= 0 && y >= 0 && x < Values.GetLength(0) && y < Values.GetLength(1) ? Values[x, y] : 0;
    }

    /// <summary>
    /// Convert from world pixel space to int grid space
    /// Floors the value based on <see cref="TileSize"/> to an Integer
    /// </summary>
    /// <param name="position">World pixel coordinates</param>
    /// <returns>Grid position</returns>
    public Point FromWorldToGridSpace(Vector2 position)
    {
        (float xf, float yf) = position;
        int x = (int)Math.Floor(xf / TileSize);
        int y = (int)Math.Floor(yf / TileSize);
        return new Point(x, y);
    }

    /// <summary>
    /// Returns the color from the intgrid value set in ldtk
    /// </summary>
    /// <param name="value">Intgrid value</param>
    /// <returns>Color of intgrid cell value</returns>
    public Color GetColorFromValue(int value)
    {
        return colors.TryGetValue(value, out Color col) ? col : Color.HotPink;
    }
}
