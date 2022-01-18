using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rect = Microsoft.Xna.Framework.Rectangle;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace LDtk;

internal class ColorConverter : JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.GetString() is not { } str)
        {
            throw new JsonException();
        }

        if (!str.StartsWith('#'))
        {
            throw new Exception(str);
        }

        byte r = Convert.ToByte(str[1..3], 16);
        byte g = Convert.ToByte(str[3..5], 16);
        byte b = Convert.ToByte(str[7..9], 16);
#pragma warning disable IDE0090
        Color color = new(r, g, b, (byte)255);
#pragma warning restore IDE0090
        return color;

    }

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteStringValue($"#{value.R:X2}{value.G:X2}{value.B:X2}");
    }
}

internal class RectConverter : JsonConverter<Rect>
{
    public override Rect Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return default;
        }

        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException();
        }

        List<int> value = new();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return new Rect(value[0], value[1], value[2], value[3]);
            }

            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            int element = reader.GetInt32();
            value.Add(element);
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, Rect val, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(val.X);
        writer.WriteNumberValue(val.Y);
        writer.WriteNumberValue(val.Width);
        writer.WriteNumberValue(val.Height);
        writer.WriteEndArray();
    }
}

internal class Vector2Converter : JsonConverter<Vector2>
{
    public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException();
        }

        List<float> value = new();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return new Vector2(value[0], value[1]);
            }

            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            float element = reader.GetSingle();
            value.Add(element);
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, Vector2 val, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        (float x, float y) = val;
        writer.WriteNumberValue(x);
        writer.WriteNumberValue(y);
        writer.WriteEndArray();
    }
}

internal class PointConverter : JsonConverter<Point>
{
    public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException();
        }

        List<int> value = new();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return value.Count > 0 ? new Point(value[0], value[1]) : new Point();
            }

            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            int element = reader.GetInt32();
            value.Add(element);
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, Point val, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(val.X);
        writer.WriteNumberValue(val.Y);
        writer.WriteEndArray();
    }
}

internal class CxCyConverter : JsonConverter<Point>
{
    public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        reader.Read();

        if (reader.TokenType != JsonTokenType.PropertyName)
        {
            throw new JsonException();
        }

        reader.Read();
        int cx = reader.GetInt32();

        reader.Read();

        if (reader.TokenType != JsonTokenType.PropertyName)
        {
            throw new JsonException();
        }

        reader.Read();
        int cy = reader.GetInt32();

        reader.Read();
        return reader.TokenType != JsonTokenType.EndObject ? throw new JsonException() : new Point(cx, cy);
    }

    public override void Write(Utf8JsonWriter writer, Point val, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(val.X);
        writer.WriteNumberValue(val.Y);
        writer.WriteEndArray();
    }
}

internal class NeighborDirConverter : JsonConverter<NeighborDir>
{
    public override NeighborDir Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.GetString() switch
        {
            "n" => NeighborDir.North,
            "s" => NeighborDir.South,
            "e" => NeighborDir.East,
            "w" => NeighborDir.West,
            _ => throw new ArgumentOutOfRangeException()
        };

    public override void Write(Utf8JsonWriter writer, NeighborDir value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            NeighborDir.North => "n",
            NeighborDir.South => "s",
            NeighborDir.East => "e",
            NeighborDir.West => "w",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        });
    }
}
