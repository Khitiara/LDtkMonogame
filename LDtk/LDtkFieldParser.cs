﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using LDtk.Exceptions;
using Microsoft.Xna.Framework;

namespace LDtk;

/// <summary>
/// Utility for parsing ldtk json data into more typed versions
/// </summary>
internal static class LDtkFieldParser
{
    /// <summary>
    /// Using Reflections parse the fields in the json/<paramref name="fields"/> into <paramref name="level"/>
    /// </summary>
    /// <typeparam name="T">Level Type</typeparam>
    /// <param name="level">Level</param>
    /// <param name="fields">LDtk fields to apply to the class</param>
    /// <exception cref="FieldNotFoundException"></exception>
    public static void ParseCustomLevelFields<T>(T level, FieldInstance[] fields)
        where T : new()
    {
        ParseCustomFields(level, fields, null);
    }

    /// <summary>
    /// Using Reflections parse the fields in the json/<paramref name="fields"/> into <paramref name="entity"/>
    /// </summary>
    /// <typeparam name="T">Entity Type</typeparam>
    /// <param name="entity">Entity</param>
    /// <param name="fields">LDtk fields to apply to the class</param>
    /// <param name="level"></param>
    /// <exception cref="FieldNotFoundException"></exception>
    public static void ParseCustomEntityFields<T>(T entity, FieldInstance[] fields, LDtkLevel? level)
        where T : new()
    {
        ParseCustomFields(entity, fields, level);
    }

    private static void ParseCustomFields<T>(T classFields, FieldInstance[] fields, LDtkLevel? level)
    {
        foreach (FieldInstance fieldInstance in fields) {
            string variableName = fieldInstance._Identifier;

            if (typeof(T).GetProperty(variableName) is not { } variableDef)
            {
                throw new FieldNotFoundException(
                    $"Error: Field \"{variableName}\" not found in {typeof(T).FullName}. Maybe you should run ldtkgen again to update the files?");
            }

            int enumTypeIndex = fieldInstance._Type.LastIndexOf('.');
            int arrayEndIndex = fieldInstance._Type.LastIndexOf('>');

            string variableType = fieldInstance._Type;

            if (enumTypeIndex != -1)
            {
                variableType = arrayEndIndex != -1
                    ? variableType.Remove(enumTypeIndex, arrayEndIndex - enumTypeIndex)
                    : variableType.Remove(enumTypeIndex, variableType.Length - enumTypeIndex);
            }

            switch (variableType)
            {
                case Field.IntType:
                case Field.BoolType:
                case Field.EnumType:
                case Field.FloatType:
                case Field.StringType:
                case Field.FilePathType:
                    if (fieldInstance._Value.ValueKind != JsonValueKind.Null)
                        variableDef.SetValue(classFields,
                            fieldInstance._Value.Deserialize(variableDef.PropertyType));

                    break;

                case Field.IntArrayType:
                case Field.BoolArrayType:
                case Field.EnumArrayType:
                case Field.FloatArrayType:
                case Field.StringArrayType:
                case Field.FilePathArrayType:
                case Field.LocalEnumArrayType:
                    variableDef.SetValue(classFields,
                        Convert.ChangeType(fieldInstance._Value.Deserialize(variableDef.PropertyType,
                                new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } })!,
                            variableDef.PropertyType));
                    break;

                case Field.LocalEnumType:
                    variableDef.SetValue(classFields,
                        Enum.Parse(variableDef.PropertyType, fieldInstance._Value.GetString()!));
                    break;

                case Field.ColorType:
                    variableDef.SetValue(classFields,
                        fieldInstance._Value.Deserialize<Color>(
                            new JsonSerializerOptions { Converters = { new ColorConverter() } }));
                    break;

                // Only Entities can have point fields
                case Field.PointType:
                    if (fieldInstance._Value.ValueKind != JsonValueKind.Null)
                    {
                        fieldInstance._Value.Deserialize(variableDef.PropertyType,
                            new JsonSerializerOptions
                            {
                                Converters = { new CxCyConverter(), new Vector2Converter() }
                            });
                    }
                    else
                    {
                        if (variableDef.PropertyType == typeof(Vector2))
                        {
                            variableDef.SetValue(classFields, Vector2.Zero);
                        }
                        else if (variableDef.PropertyType == typeof(Point))
                        {
                            variableDef.SetValue(classFields, Point.Zero);
                        }
                    }

                    break;

                case Field.PointArrayType:
                    List<Point> points =
                        fieldInstance._Value.Deserialize<List<Point>>(
                            new JsonSerializerOptions { Converters = { new CxCyConverter() } })!;

                    int gridSize = 0;
                    foreach (LayerInstance t in level!.LayerInstances!)
                    {
                        if (t._Type == LayerType.Entities)
                        {
                            gridSize = t._GridSize;
                        }
                    }

                    for (int j = 0; j < points.Count; j++)
                    {
                        points[j] = new Point(points[j].X * gridSize, points[j].Y * gridSize);
                        points[j] += level.Position;
                        points[j] += new Point(gridSize / 2);
                    }

                    variableDef.SetValue(classFields, points.ToArray());
                    break;

                default:
                    throw new FieldNotFoundException("Unknown Variable of type " + fieldInstance._Type);
            }
        }
    }

    public static void ParseBaseEntityFields<T>(T entity, EntityInstance entityInstance, LDtkLevel level)
        where T : new()
    {
        EntityDefinition entityDefinition = level.Parent.GetEntityDefinitionFromUid(entityInstance.DefUid)!;

        ParseBaseField(entity, "Position", (entityInstance.Px + level.Position).ToVector2());
        ParseBaseField(entity, "Pivot", entityInstance._Pivot);
        ParseBaseField(entity, "Size", new Vector2(entityInstance.Width, entityInstance.Height));
        ParseBaseField(entity, "EditorVisualColor", entityDefinition.Color);

        if (entityInstance._Tile == null)
        {
            return;
        }

        EntityInstanceTile tileDefinition = entityInstance._Tile;
        Rectangle rect = tileDefinition.SrcRect;
        ParseBaseField(entity, "Tile", rect);
    }

    // Helpers

    private static void ParseBaseField<T>(T entity, string fieldName, object value)
    {
        PropertyInfo? variableDef = typeof(T).GetProperty(fieldName,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        if (variableDef == null)
        {
            throw new FieldNotFoundException(
                $"Error: Field \"{fieldName}\" not found in {typeof(T).FullName}. Maybe you should run ldtkgen again to update the files?");
        }

        variableDef.SetValue(entity, value);
    }

    /// <summary>
    /// Entity and Level Field Types
    /// </summary>
    private static class Field
    {
        public const string IntType            = "Int";
        public const string IntArrayType       = "Array<Int>";
        public const string FloatType          = "Float";
        public const string FloatArrayType     = "Array<Float>";
        public const string BoolType           = "Bool";
        public const string BoolArrayType      = "Array<Bool>";
        public const string EnumType           = "Enum";
        public const string EnumArrayType      = "Array<Enum>";
        public const string LocalEnumType      = "LocalEnum";
        public const string LocalEnumArrayType = "Array<LocalEnum>";
        public const string StringType         = "String";
        public const string StringArrayType    = "Array<String>";
        public const string FilePathType       = "FilePath";
        public const string FilePathArrayType  = "Array<FilePath>";
        public const string ColorType          = "Color";
        public const string ColorArrayType     = "Array<Color>";
        public const string PointType          = "Point";
        public const string PointArrayType     = "Array<Point>";
    }
}
