// 0.9.3

using System.Text.Json.Serialization;

#pragma warning disable 1591, 1570, IDE1006, CS8618
namespace LDtk.Codegen;

/// <summary>
/// The main class that contains all the project related info
/// </summary>
public partial class LDtkWorld
{

    /// <summary>
    /// A structure containing all the definitions of this project
    /// </summary>
    [JsonPropertyName("defs")]
    public Definitions Defs { get; set; }
}

/// <summary>
/// A structure containing all the definitions of this project
///
/// If you're writing your own LDtk importer, you should probably just ignore *most* stuff in
/// the `defs` section, as it contains data that are mostly important to the editor. To keep
/// you away from the `defs` section and avoid some unnecessary JSON parsing, important data
/// from definitions is often duplicated in fields prefixed with a float underscore (eg.
/// `__identifier` or `__type`).  The 2 only definition types you might need here are
/// **Tilesets** and **Enums**.
/// </summary>
public partial class Definitions
{
    /// <summary>
    /// All entities definitions, including their custom fields
    /// </summary>
    [JsonPropertyName("entities")]
    public EntityDefinition[] Entities { get; set; }

    /// <summary>
    /// All internal enums
    /// </summary>
    [JsonPropertyName("enums")]
    public EnumDefinition[] Enums { get; set; }

    /// <summary>
    /// An array containing all custom fields available to all levels.
    /// </summary>
    [JsonPropertyName("levelFields")]
    public FieldDefinition[] LevelFields { get; set; }
}

public partial class EntityDefinition
{

    /// <summary>
    /// Array of field definitions
    /// </summary>
    [JsonPropertyName("fieldDefs")]
    public FieldDefinition[] FieldDefs { get; set; }

    /// <summary>
    /// Unique String identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }
}

public partial class EnumDefinition
{
    /// <summary>
    /// Unique String identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// All possible enum values, with their optional Tile infos.
    /// </summary>
    [JsonPropertyName("values")]
    public EnumValueDefinition[] Values { get; set; }
}

public partial class EnumValueDefinition
{
    /// <summary>
    /// Enum value
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }
}

/// <summary>
/// This section is mostly only intended for the LDtk editor app itself. You can safely
/// ignore it.
/// </summary>
public partial class FieldDefinition
{
    /// <summary>
    /// Human readable value type (eg. `Int`, `Float`, `Point`, etc.). If the field is an array,
    /// this field will look like `Array<...>` (eg. `Array<Int>`, `Array<Point>` etc.)
    /// </summary>
    [JsonPropertyName("__type")]
    public string Type { get; set; }

    /// <summary>
    /// Unique String identifier
    /// </summary>
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    /// <summary>
    /// TRUE if the value is an array of multiple values
    /// </summary>
    [JsonPropertyName("isArray")]
    public bool IsArray { get; set; }
}

#pragma warning restore 1591, 1570, IDE1006, CS8618
