// 0.7.2

using System;
using System.Collections.Generic;

using System.Globalization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#pragma warning disable 1591
#pragma warning disable 1570
namespace LDtk.Json
{
    /// <summary>
    /// This file is a JSON schema of files created by LDtk level editor (https://ldtk.io).
    ///
    /// This is the root of any Project JSON file. It contains:  - the project settings, - an
    /// array of levels, - and a definition object (that can probably be safely ignored for most
    /// users).
    /// </summary>
    public partial class LDtkJson
    {
        /// <summary>
        /// Number of backup files to keep, if the `backupOnSave` is TRUE
        /// </summary>
        [JsonProperty("backupLimit")]
        public long BackupLimit { get; set; }

        /// <summary>
        /// If TRUE, an extra copy of the project will be created in a sub folder, when saving.
        /// </summary>
        [JsonProperty("backupOnSave")]
        public bool BackupOnSave { get; set; }

        /// <summary>
        /// Project background color
        /// </summary>
        [JsonProperty("bgColor")]
        public string BgColor { get; set; }

        /// <summary>
        /// Default grid size for new layers
        /// </summary>
        [JsonProperty("defaultGridSize")]
        public long DefaultGridSize { get; set; }

        /// <summary>
        /// Default background color of levels
        /// </summary>
        [JsonProperty("defaultLevelBgColor")]
        public string DefaultLevelBgColor { get; set; }

        /// <summary>
        /// Default X pivot (0 to 1) for new entities
        /// </summary>
        [JsonProperty("defaultPivotX")]
        public double DefaultPivotX { get; set; }

        /// <summary>
        /// Default Y pivot (0 to 1) for new entities
        /// </summary>
        [JsonProperty("defaultPivotY")]
        public double DefaultPivotY { get; set; }

        /// <summary>
        /// A structure containing all the definitions of this project
        /// </summary>
        [JsonProperty("defs", NullValueHandling = NullValueHandling.Ignore)]
        public Definitions Defs { get; set; }

        /// <summary>
        /// If TRUE, all layers in all levels will also be exported as PNG along with the project
        /// file (default is FALSE)
        /// </summary>
        [JsonProperty("exportPng")]
        public bool ExportPng { get; set; }

        /// <summary>
        /// If TRUE, a Tiled compatible file will also be generated along with the LDtk JSON file
        /// (default is FALSE)
        /// </summary>
        [JsonProperty("exportTiled")]
        public bool ExportTiled { get; set; }

        /// <summary>
        /// If TRUE, one file will be saved for the project (incl. all its definitions) and one file
        /// in a sub-folder for each level.
        /// </summary>
        [JsonProperty("externalLevels")]
        public bool ExternalLevels { get; set; }

        /// <summary>
        /// File format version
        /// </summary>
        [JsonProperty("jsonVersion")]
        public string JsonVersion { get; set; }

        /// <summary>
        /// All levels. The order of this array is only relevant in `LinearHorizontal` and
        /// `linearVertical` world layouts (see `worldLayout` value). Otherwise, you should refer to
        /// the `worldX`,`worldY` coordinates of each Level.
        /// </summary>
        [JsonProperty("levels")]
        public LDtkLevel[] Levels { get; set; }

        /// <summary>
        /// If TRUE, the Json is partially minified (no indentation, nor line breaks, default is
        /// FALSE)
        /// </summary>
        [JsonProperty("minifyJson")]
        public bool MinifyJson { get; set; }

        [JsonProperty("nextUid")]
        public long NextUid { get; set; }

        /// <summary>
        /// File naming pattern for exported PNGs
        /// </summary>
        [JsonProperty("pngFilePattern")]
        public string PngFilePattern { get; set; }

        /// <summary>
        /// Height of the world grid in pixels.
        /// </summary>
        [JsonProperty("worldGridHeight")]
        public long WorldGridHeight { get; set; }

        /// <summary>
        /// Width of the world grid in pixels.
        /// </summary>
        [JsonProperty("worldGridWidth")]
        public long WorldGridWidth { get; set; }

        /// <summary>
        /// An enum that describes how levels are organized in this project (ie. linearly or in a 2D
        /// space). Possible values: `Free`, `GridVania`, `LinearHorizontal`, `LinearVertical`
        /// </summary>
        [JsonProperty("worldLayout", NullValueHandling = NullValueHandling.Ignore)]
        public WorldLayout? WorldLayout { get; set; }
    }

    /// <summary>
    /// A structure containing all the definitions of this project
    ///
    /// If you're writing your own LDtk importer, you should probably just ignore *most* stuff in
    /// the `defs` section, as it contains data that are mostly important to the editor. To keep
    /// you away from the `defs` section and avoid some unnecessary JSON parsing, important data
    /// from definitions is often duplicated in fields prefixed with a double underscore (eg.
    /// `__identifier` or `__type`).  The 2 only definition types you might need here are
    /// **Tilesets** and **Enums**.
    /// </summary>
    public partial class Definitions
    {
        [JsonProperty("entities")]
        public EntityDefinition[] Entities { get; set; }

        [JsonProperty("enums")]
        public EnumDefinition[] Enums { get; set; }

        /// <summary>
        /// Note: external enums are exactly the same as `enums`, except they have a `relPath` to
        /// point to an external source file.
        /// </summary>
        [JsonProperty("externalEnums")]
        public EnumDefinition[] ExternalEnums { get; set; }

        [JsonProperty("layers")]
        public LayerDefinition[] Layers { get; set; }

        [JsonProperty("tilesets")]
        public TilesetDefinition[] Tilesets { get; set; }
    }

    public partial class EntityDefinition
    {
        /// <summary>
        /// Base entity color
        /// </summary>
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// Array of field definitions
        /// </summary>
        [JsonProperty("fieldDefs")]
        public FieldDefinition[] FieldDefs { get; set; }

        /// <summary>
        /// Pixel height
        /// </summary>
        [JsonProperty("height")]
        public long Height { get; set; }

        /// <summary>
        /// Unique String identifier
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Possible values: `DiscardOldOnes`, `PreventAdding`, `MoveLastOne`
        /// </summary>
        [JsonProperty("limitBehavior", NullValueHandling = NullValueHandling.Ignore)]
        public LimitBehavior? LimitBehavior { get; set; }

        /// <summary>
        /// Max instances per level
        /// </summary>
        [JsonProperty("maxPerLevel")]
        public long MaxPerLevel { get; set; }

        /// <summary>
        /// Pivot X coordinate (from 0 to 1.0)
        /// </summary>
        [JsonProperty("pivotX")]
        public double PivotX { get; set; }

        /// <summary>
        /// Pivot Y coordinate (from 0 to 1.0)
        /// </summary>
        [JsonProperty("pivotY")]
        public double PivotY { get; set; }

        /// <summary>
        /// Possible values: `Rectangle`, `Ellipse`, `Tile`, `Cross`
        /// </summary>
        [JsonProperty("renderMode", NullValueHandling = NullValueHandling.Ignore)]
        public RenderMode? RenderMode { get; set; }

        /// <summary>
        /// Display entity name in editor
        /// </summary>
        [JsonProperty("showName")]
        public bool ShowName { get; set; }

        /// <summary>
        /// Tile ID used for optional tile display
        /// </summary>
        [JsonProperty("tileId")]
        public long? TileId { get; set; }

        /// <summary>
        /// Possible values: `Stretch`, `Crop`
        /// </summary>
        [JsonProperty("tileRenderMode", NullValueHandling = NullValueHandling.Ignore)]
        public TileRenderMode? TileRenderMode { get; set; }

        /// <summary>
        /// Tileset ID used for optional tile display
        /// </summary>
        [JsonProperty("tilesetId")]
        public long? TilesetId { get; set; }

        /// <summary>
        /// Unique Int identifier
        /// </summary>
        [JsonProperty("uid")]
        public long Uid { get; set; }

        /// <summary>
        /// Pixel width
        /// </summary>
        [JsonProperty("width")]
        public long Width { get; set; }
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
        [JsonProperty("__type")]
        public string Type { get; set; }

        /// <summary>
        /// Optional list of accepted file extensions for FilePath value type. Includes the dot:
        /// `.ext`
        /// </summary>
        [JsonProperty("acceptFileTypes")]
        public string[] AcceptFileTypes { get; set; }

        /// <summary>
        /// Array max length
        /// </summary>
        [JsonProperty("arrayMaxLength")]
        public long? ArrayMaxLength { get; set; }

        /// <summary>
        /// Array min length
        /// </summary>
        [JsonProperty("arrayMinLength")]
        public long? ArrayMinLength { get; set; }

        /// <summary>
        /// TRUE if the value can be null. For arrays, TRUE means it can contain null values
        /// (exception: array of Points can't have null values).
        /// </summary>
        [JsonProperty("canBeNull")]
        public bool CanBeNull { get; set; }

        /// <summary>
        /// Default value if selected value is null or invalid.
        /// </summary>
        [JsonProperty("defaultOverride")]
        public object DefaultOverride { get; set; }

        [JsonProperty("editorAlwaysShow")]
        public bool EditorAlwaysShow { get; set; }

        /// <summary>
        /// Possible values: `Hidden`, `ValueOnly`, `NameAndValue`, `EntityTile`, `PointStar`,
        /// `PointPath`, `RadiusPx`, `RadiusGrid`
        /// </summary>
        [JsonProperty("editorDisplayMode", NullValueHandling = NullValueHandling.Ignore)]
        public EditorDisplayMode? EditorDisplayMode { get; set; }

        /// <summary>
        /// Possible values: `Above`, `Center`, `Beneath`
        /// </summary>
        [JsonProperty("editorDisplayPos", NullValueHandling = NullValueHandling.Ignore)]
        public EditorDisplayPos? EditorDisplayPos { get; set; }

        /// <summary>
        /// Unique String identifier
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// TRUE if the value is an array of multiple values
        /// </summary>
        [JsonProperty("isArray")]
        public bool IsArray { get; set; }

        /// <summary>
        /// Max limit for value, if applicable
        /// </summary>
        [JsonProperty("max")]
        public double? Max { get; set; }

        /// <summary>
        /// Min limit for value, if applicable
        /// </summary>
        [JsonProperty("min")]
        public double? Min { get; set; }

        /// <summary>
        /// Optional regular expression that needs to be matched to accept values. Expected format:
        /// `/some_reg_ex/g`, with optional "i" flag.
        /// </summary>
        [JsonProperty("regex")]
        public string Regex { get; set; }

        /// <summary>
        /// Internal type enum
        /// </summary>
        [JsonProperty("type")]
        public object FieldDefinitionType { get; set; }

        /// <summary>
        /// Unique Intidentifier
        /// </summary>
        [JsonProperty("uid")]
        public long Uid { get; set; }
    }

    public partial class EnumDefinition
    {
        [JsonProperty("externalFileChecksum")]
        public string ExternalFileChecksum { get; set; }

        /// <summary>
        /// Relative path to the external file providing this Enum
        /// </summary>
        [JsonProperty("externalRelPath")]
        public string ExternalRelPath { get; set; }

        /// <summary>
        /// Tileset UID if provided
        /// </summary>
        [JsonProperty("iconTilesetUid")]
        public long? IconTilesetUid { get; set; }

        /// <summary>
        /// Unique String identifier
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Unique Int identifier
        /// </summary>
        [JsonProperty("uid")]
        public long Uid { get; set; }

        /// <summary>
        /// All possible enum values, with their optional Tile infos.
        /// </summary>
        [JsonProperty("values")]
        public EnumValueDefinition[] Values { get; set; }
    }

    public partial class EnumValueDefinition
    {
        /// <summary>
        /// An array of 4 Int values that refers to the tile in the tileset image: `[ x, y, width,
        /// height ]`
        /// </summary>
        [JsonProperty("__tileSrcRect")]
        public long[] TileSrcRect { get; set; }

        /// <summary>
        /// Enum value
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The optional ID of the tile
        /// </summary>
        [JsonProperty("tileId")]
        public long? TileId { get; set; }
    }

    public partial class LayerDefinition
    {
        /// <summary>
        /// Type of the layer (*IntGrid, Entities, Tiles or AutoLayer*)
        /// </summary>
        [JsonProperty("__type")]
        public string Type { get; set; }

        /// <summary>
        /// Contains all the auto-layer rule definitions.
        /// </summary>
        [JsonProperty("autoRuleGroups")]
        public Dictionary<string, object>[] AutoRuleGroups { get; set; }

        [JsonProperty("autoSourceLayerDefUid")]
        public long? AutoSourceLayerDefUid { get; set; }

        /// <summary>
        /// Reference to the Tileset UID being used by this auto-layer rules
        /// </summary>
        [JsonProperty("autoTilesetDefUid")]
        public long? AutoTilesetDefUid { get; set; }

        /// <summary>
        /// Opacity of the layer (0 to 1.0)
        /// </summary>
        [JsonProperty("displayOpacity")]
        public double DisplayOpacity { get; set; }

        /// <summary>
        /// Width and height of the grid in pixels
        /// </summary>
        [JsonProperty("gridSize")]
        public long GridSize { get; set; }

        /// <summary>
        /// Unique String identifier
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// An array (using IntGrid value as array index, starting from 0) that defines extra
        /// optional info for each IntGrid value.
        /// </summary>
        [JsonProperty("intGridValues")]
        public IntGridValueDefinition[] IntGridValues { get; set; }

        /// <summary>
        /// X offset of the layer, in pixels (IMPORTANT: this should be added to the `LayerInstance`
        /// optional offset)
        /// </summary>
        [JsonProperty("pxOffsetX")]
        public long PxOffsetX { get; set; }

        /// <summary>
        /// Y offset of the layer, in pixels (IMPORTANT: this should be added to the `LayerInstance`
        /// optional offset)
        /// </summary>
        [JsonProperty("pxOffsetY")]
        public long PxOffsetY { get; set; }

        /// <summary>
        /// If the tiles are smaller or larger than the layer grid, the pivot value will be used to
        /// position the tile relatively its grid cell.
        /// </summary>
        [JsonProperty("tilePivotX")]
        public double TilePivotX { get; set; }

        /// <summary>
        /// If the tiles are smaller or larger than the layer grid, the pivot value will be used to
        /// position the tile relatively its grid cell.
        /// </summary>
        [JsonProperty("tilePivotY")]
        public double TilePivotY { get; set; }

        /// <summary>
        /// Reference to the Tileset UID being used by this Tile layer
        /// </summary>
        [JsonProperty("tilesetDefUid")]
        public long? TilesetDefUid { get; set; }

        /// <summary>
        /// Type of the layer as Haxe Enum Possible values: `IntGrid`, `Entities`, `Tiles`,
        /// `AutoLayer`
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public TypeEnum? LayerDefinitionType { get; set; }

        /// <summary>
        /// Unique Int identifier
        /// </summary>
        [JsonProperty("uid")]
        public long Uid { get; set; }
    }

    /// <summary>
    /// IntGrid value definition
    /// </summary>
    public partial class IntGridValueDefinition
    {
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// Unique String identifier
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier { get; set; }
    }

    /// <summary>
    /// The `Tileset` definition is the most important part among project definitions. It
    /// contains some extra informations about each integrated tileset. If you only had to parse
    /// one definition section, that would be the one.
    /// </summary>
    public partial class TilesetDefinition
    {
        /// <summary>
        /// The following data is used internally for various optimizations. It's always synced with
        /// source image changes.
        /// </summary>
        [JsonProperty("cachedPixelData")]
        public Dictionary<string, object> CachedPixelData { get; set; }

        /// <summary>
        /// Unique String identifier
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Distance in pixels from image borders
        /// </summary>
        [JsonProperty("padding")]
        public long Padding { get; set; }

        /// <summary>
        /// Image height in pixels
        /// </summary>
        [JsonProperty("pxHei")]
        public long PxHei { get; set; }

        /// <summary>
        /// Image width in pixels
        /// </summary>
        [JsonProperty("pxWid")]
        public long PxWid { get; set; }

        /// <summary>
        /// Path to the source file, relative to the current project JSON file
        /// </summary>
        [JsonProperty("relPath")]
        public string RelPath { get; set; }

        /// <summary>
        /// Array of group of tiles selections, only meant to be used in the editor
        /// </summary>
        [JsonProperty("savedSelections")]
        public Dictionary<string, object>[] SavedSelections { get; set; }

        /// <summary>
        /// Space in pixels between all tiles
        /// </summary>
        [JsonProperty("spacing")]
        public long Spacing { get; set; }

        [JsonProperty("tileGridSize")]
        public long TileGridSize { get; set; }

        /// <summary>
        /// Unique Intidentifier
        /// </summary>
        [JsonProperty("uid")]
        public long Uid { get; set; }
    }

    /// <summary>
    /// This section contains all the level data. It can be found in 2 distinct forms, depending
    /// on Project current settings:  - If "*Separate level files*" is **disabled** (default):
    /// full level data is *embedded* inside the main Project JSON file, - If "*Separate level
    /// files*" is **enabled**: level data is stored in *separate* standalone `.ldtkl` files (one
    /// per level). In this case, the main Project JSON file will still contain most level data,
    /// except heavy sections, like the `layerInstances` array (which will be null). The
    /// `externalRelPath` string points to the `ldtkl` file.  A `ldtkl` file is just a JSON file
    /// containing exactly what is described below.
    /// </summary>
    public partial class LDtkLevel
    {
        /// <summary>
        /// Background color of the level (same as `bgColor`, except the default value is
        /// automatically used here if its value is `null`)
        /// </summary>
        [JsonProperty("__bgColor")]
        public string BgColor { get; set; }

        /// <summary>
        /// Position informations of the background image, if there is one.
        /// </summary>
        [JsonProperty("__bgPos", NullValueHandling = NullValueHandling.Ignore)]
        public LevelBackgroundPosition BgPos { get; set; }

        /// <summary>
        /// An array listing all other levels touching this one on the world map. In "linear" world
        /// layouts, this array is populated with previous/next levels in array, and `dir` depends on
        /// the linear horizontal/vertical layout.
        /// </summary>
        [JsonProperty("__neighbours")]
        public NeighbourLevel[] Neighbours { get; set; }

        /// <summary>
        /// Background color of the level. If `null`, the project `defaultLevelBgColor` should be
        /// used.
        /// </summary>
        [JsonProperty("bgColor")]
        public string LevelBgColor { get; set; }

        /// <summary>
        /// Background image X pivot (0-1)
        /// </summary>
        [JsonProperty("bgPivotX")]
        public double BgPivotX { get; set; }

        /// <summary>
        /// Background image Y pivot (0-1)
        /// </summary>
        [JsonProperty("bgPivotY")]
        public double BgPivotY { get; set; }

        /// <summary>
        /// An enum defining the way the background image (if any) is positioned on the level. See
        /// `__bgPos` for resulting position info. Possible values: `Unscaled`, `Contain`, `Cover`,
        /// `CoverDirty`
        /// </summary>
        [JsonProperty("bgPos", NullValueHandling = NullValueHandling.Ignore)]
        public BgPos? LevelBgPos { get; set; }

        /// <summary>
        /// The *optional* relative path to the level background image.
        /// </summary>
        [JsonProperty("bgRelPath")]
        public string BgRelPath { get; set; }

        /// <summary>
        /// This value is not null if the project option "*Save levels separately*" is enabled. In
        /// this case, this **relative** path points to the level Json file.
        /// </summary>
        [JsonProperty("externalRelPath")]
        public string ExternalRelPath { get; set; }

        /// <summary>
        /// Unique String identifier
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// An array containing all Layer instances. **IMPORTANT**: if the project option "*Save
        /// levels separately*" is enabled, this field will be `null`.<br/>  This array is **sorted
        /// in display order**: the 1st layer is the top-most and the last is behind.
        /// </summary>
        [JsonProperty("layerInstances")]
        public LayerInstance[] LayerInstances { get; set; }

        /// <summary>
        /// Height of the level in pixels
        /// </summary>
        [JsonProperty("pxHei")]
        public long PxHei { get; set; }

        /// <summary>
        /// Width of the level in pixels
        /// </summary>
        [JsonProperty("pxWid")]
        public long PxWid { get; set; }

        /// <summary>
        /// Unique Int identifier
        /// </summary>
        [JsonProperty("uid")]
        public long Uid { get; set; }

        /// <summary>
        /// World X coordinate in pixels
        /// </summary>
        [JsonProperty("worldX")]
        public long WorldX { get; set; }

        /// <summary>
        /// World Y coordinate in pixels
        /// </summary>
        [JsonProperty("worldY")]
        public long WorldY { get; set; }
    }

    /// <summary>
    /// Position informations of the background image, if there is one.
    ///
    /// Level background image position info
    /// </summary>
    public partial class LevelBackgroundPosition
    {
        /// <summary>
        /// An array of 4 float values describing the cropped sub-rectangle of the displayed
        /// background image. This cropping happens when original is larger than the level bounds.
        /// Array format: `[ cropX, cropY, cropWidth, cropHeight ]`
        /// </summary>
        [JsonProperty("cropRect")]
        public double[] CropRect { get; set; }

        /// <summary>
        /// An array containing the `[scaleX,scaleY]` values of the **cropped** background image,
        /// depending on `bgPos` option.
        /// </summary>
        [JsonProperty("scale")]
        public double[] Scale { get; set; }

        /// <summary>
        /// An array containing the `[x,y]` pixel coordinates of the top-left corner of the
        /// **cropped** background image, depending on `bgPos` option.
        /// </summary>
        [JsonProperty("topLeftPx")]
        public long[] TopLeftPx { get; set; }
    }

    public partial class LayerInstance
    {
        /// <summary>
        /// Grid-based height
        /// </summary>
        [JsonProperty("__cHei")]
        public long CHei { get; set; }

        /// <summary>
        /// Grid-based width
        /// </summary>
        [JsonProperty("__cWid")]
        public long CWid { get; set; }

        /// <summary>
        /// Grid size
        /// </summary>
        [JsonProperty("__gridSize")]
        public long GridSize { get; set; }

        /// <summary>
        /// Layer definition identifier
        /// </summary>
        [JsonProperty("__identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Layer opacity as Float [0-1]
        /// </summary>
        [JsonProperty("__opacity")]
        public double Opacity { get; set; }

        /// <summary>
        /// Total layer X pixel offset, including both instance and definition offsets.
        /// </summary>
        [JsonProperty("__pxTotalOffsetX")]
        public long PxTotalOffsetX { get; set; }

        /// <summary>
        /// Total layer Y pixel offset, including both instance and definition offsets.
        /// </summary>
        [JsonProperty("__pxTotalOffsetY")]
        public long PxTotalOffsetY { get; set; }

        /// <summary>
        /// The definition UID of corresponding Tileset, if any.
        /// </summary>
        [JsonProperty("__tilesetDefUid")]
        public long? TilesetDefUid { get; set; }

        /// <summary>
        /// The relative path to corresponding Tileset, if any.
        /// </summary>
        [JsonProperty("__tilesetRelPath")]
        public string TilesetRelPath { get; set; }

        /// <summary>
        /// Layer type (possible values: IntGrid, Entities, Tiles or AutoLayer)
        /// </summary>
        [JsonProperty("__type")]
        public string Type { get; set; }

        /// <summary>
        /// An array containing all tiles generated by Auto-layer rules. The array is already sorted
        /// in display order (ie. 1st tile is beneath 2nd, which is beneath 3rd etc.).<br/><br/>
        /// Note: if multiple tiles are stacked in the same cell as the result of different rules,
        /// all tiles behind opaque ones will be discarded.
        /// </summary>
        [JsonProperty("autoLayerTiles")]
        public TileInstance[] AutoLayerTiles { get; set; }

        [JsonProperty("entityInstances")]
        public EntityInstance[] EntityInstances { get; set; }

        [JsonProperty("gridTiles")]
        public TileInstance[] GridTiles { get; set; }

        [JsonProperty("intGrid")]
        public IntGridValueInstance[] IntGrid { get; set; }

        /// <summary>
        /// Reference the Layer definition UID
        /// </summary>
        [JsonProperty("layerDefUid")]
        public long LayerDefUid { get; set; }

        /// <summary>
        /// Reference to the UID of the level containing this layer instance
        /// </summary>
        [JsonProperty("levelId")]
        public long LevelId { get; set; }

        /// <summary>
        /// X offset in pixels to render this layer, usually 0 (IMPORTANT: this should be added to
        /// the `LayerDef` optional offset, see `__pxTotalOffsetX`)
        /// </summary>
        [JsonProperty("pxOffsetX")]
        public long PxOffsetX { get; set; }

        /// <summary>
        /// Y offset in pixels to render this layer, usually 0 (IMPORTANT: this should be added to
        /// the `LayerDef` optional offset, see `__pxTotalOffsetY`)
        /// </summary>
        [JsonProperty("pxOffsetY")]
        public long PxOffsetY { get; set; }

        /// <summary>
        /// Random seed used for Auto-Layers rendering
        /// </summary>
        [JsonProperty("seed")]
        public long Seed { get; set; }
    }

    /// <summary>
    /// This structure represents a single tile from a given Tileset.
    /// </summary>
    public partial class TileInstance
    {
        /// <summary>
        /// Internal data used by the editor.<br/>  For auto-layer tiles: `[ruleId, coordId]`.<br/>
        /// For tile-layer tiles: `[coordId]`.
        /// </summary>
        [JsonProperty("d")]
        public long[] D { get; set; }

        /// <summary>
        /// "Flip bits", a 2-bits integer to represent the mirror transformations of the tile.<br/>
        /// - Bit 0 = X flip<br/>   - Bit 1 = Y flip<br/>   Examples: f=0 (no flip), f=1 (X flip
        /// only), f=2 (Y flip only), f=3 (both flips)
        /// </summary>
        [JsonProperty("f")]
        public long F { get; set; }

        /// <summary>
        /// Pixel coordinates of the tile in the **layer** (`[x,y]` format). Don't forget optional
        /// layer offsets, if they exist!
        /// </summary>
        [JsonProperty("px")]
        public long[] Px { get; set; }

        /// <summary>
        /// Pixel coordinates of the tile in the **tileset** (`[x,y]` format)
        /// </summary>
        [JsonProperty("src")]
        public long[] Src { get; set; }

        /// <summary>
        /// The *Tile ID* in the corresponding tileset.
        /// </summary>
        [JsonProperty("t")]
        public long T { get; set; }
    }

    public partial class EntityInstance
    {
        /// <summary>
        /// Grid-based coordinates (`[x,y]` format)
        /// </summary>
        [JsonProperty("__grid")]
        public long[] Grid { get; set; }

        /// <summary>
        /// Entity definition identifier
        /// </summary>
        [JsonProperty("__identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Pivot coordinates  (`[x,y]` format, values are from 0 to 1) of the Entity
        /// </summary>
        [JsonProperty("__pivot")]
        public double[] Pivot { get; set; }

        /// <summary>
        /// Optional Tile used to display this entity (it could either be the default Entity tile, or
        /// some tile provided by a field value, like an Enum).
        /// </summary>
        [JsonProperty("__tile", NullValueHandling = NullValueHandling.Ignore)]
        public EntityInstanceTile Tile { get; set; }

        /// <summary>
        /// Reference of the **Entity definition** UID
        /// </summary>
        [JsonProperty("defUid")]
        public long DefUid { get; set; }

        [JsonProperty("fieldInstances")]
        public FieldInstance[] FieldInstances { get; set; }

        /// <summary>
        /// Pixel coordinates (`[x,y]` format) in current level coordinate space. Don't forget
        /// optional layer offsets, if they exist!
        /// </summary>
        [JsonProperty("px")]
        public long[] Px { get; set; }
    }

    public partial class FieldInstance
    {
        /// <summary>
        /// Field definition identifier
        /// </summary>
        [JsonProperty("__identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Type of the field, such as `Int`, `Float`, `Enum(my_enum_name)`, `Bool`, etc.
        /// </summary>
        [JsonProperty("__type")]
        public string Type { get; set; }

        /// <summary>
        /// Actual value of the field instance. The value type may vary, depending on `__type`
        /// (Integer, Boolean, String etc.)<br/>  It can also be an `Array` of those same types.
        /// </summary>
        [JsonProperty("__value")]
        public object Value { get; set; }

        /// <summary>
        /// Reference of the **Field definition** UID
        /// </summary>
        [JsonProperty("defUid")]
        public long DefUid { get; set; }

        /// <summary>
        /// Editor internal raw values
        /// </summary>
        [JsonProperty("realEditorValues")]
        public object[] RealEditorValues { get; set; }
    }

    /// <summary>
    /// Optional Tile used to display this entity (it could either be the default Entity tile, or
    /// some tile provided by a field value, like an Enum).
    ///
    /// Tile data in an Entity instance
    /// </summary>
    public partial class EntityInstanceTile
    {
        /// <summary>
        /// An array of 4 Int values that refers to the tile in the tileset image: `[ x, y, width,
        /// height ]`
        /// </summary>
        [JsonProperty("srcRect")]
        public long[] SrcRect { get; set; }

        /// <summary>
        /// Tileset ID
        /// </summary>
        [JsonProperty("tilesetUid")]
        public long TilesetUid { get; set; }
    }

    /// <summary>
    /// IntGrid value instance
    /// </summary>
    public partial class IntGridValueInstance
    {
        /// <summary>
        /// Coordinate ID in the layer grid
        /// </summary>
        [JsonProperty("coordId")]
        public long CoordId { get; set; }

        /// <summary>
        /// IntGrid value
        /// </summary>
        [JsonProperty("v")]
        public long V { get; set; }
    }

    /// <summary>
    /// Nearby level info
    /// </summary>
    public partial class NeighbourLevel
    {
        /// <summary>
        /// A single lowercase character tipping on the level location (`n`orth, `s`outh, `w`est,
        /// `e`ast).
        /// </summary>
        [JsonProperty("dir")]
        public string Dir { get; set; }

        [JsonProperty("levelUid")]
        public long LevelUid { get; set; }
    }

    /// <summary>
    /// Possible values: `Hidden`, `ValueOnly`, `NameAndValue`, `EntityTile`, `PointStar`,
    /// `PointPath`, `RadiusPx`, `RadiusGrid`
    /// </summary>
    public enum EditorDisplayMode { EntityTile, Hidden, NameAndValue, PointPath, PointStar, RadiusGrid, RadiusPx, ValueOnly };

    /// <summary>
    /// Possible values: `Above`, `Center`, `Beneath`
    /// </summary>
    public enum EditorDisplayPos { Above, Beneath, Center };

    /// <summary>
    /// Possible values: `DiscardOldOnes`, `PreventAdding`, `MoveLastOne`
    /// </summary>
    public enum LimitBehavior { DiscardOldOnes, MoveLastOne, PreventAdding };

    /// <summary>
    /// Possible values: `Rectangle`, `Ellipse`, `Tile`, `Cross`
    /// </summary>
    public enum RenderMode { Cross, Ellipse, Rectangle, Tile };

    /// <summary>
    /// Possible values: `Stretch`, `Crop`
    /// </summary>
    public enum TileRenderMode { Crop, Stretch };

    /// <summary>
    /// Type of the layer as Haxe Enum Possible values: `IntGrid`, `Entities`, `Tiles`,
    /// `AutoLayer`
    /// </summary>
    public enum TypeEnum { AutoLayer, Entities, IntGrid, Tiles };

    /// <summary>
    /// An enum defining the way the background image (if any) is positioned on the level. See
    /// `__bgPos` for resulting position info. Possible values: `Unscaled`, `Contain`, `Cover`,
    /// `CoverDirty`
    /// </summary>
    public enum BgPos { Contain, Cover, CoverDirty, Unscaled };

    /// <summary>
    /// An enum that describes how levels are organized in this project (ie. linearly or in a 2D
    /// space). Possible values: `Free`, `GridVania`, `LinearHorizontal`, `LinearVertical`
    /// </summary>
    public enum WorldLayout { Free, GridVania, LinearHorizontal, LinearVertical };

    public partial class LDtkJson
    {
        public static LDtkJson FromJson(string json) => JsonConvert.DeserializeObject<LDtkJson>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this LDtkJson self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                EditorDisplayModeConverter.Singleton,
                EditorDisplayPosConverter.Singleton,
                LimitBehaviorConverter.Singleton,
                RenderModeConverter.Singleton,
                TileRenderModeConverter.Singleton,
                TypeEnumConverter.Singleton,
                BgPosConverter.Singleton,
                WorldLayoutConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class EditorDisplayModeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(EditorDisplayMode) || t == typeof(EditorDisplayMode?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "EntityTile":
                    return EditorDisplayMode.EntityTile;
                case "Hidden":
                    return EditorDisplayMode.Hidden;
                case "NameAndValue":
                    return EditorDisplayMode.NameAndValue;
                case "PointPath":
                    return EditorDisplayMode.PointPath;
                case "PointStar":
                    return EditorDisplayMode.PointStar;
                case "RadiusGrid":
                    return EditorDisplayMode.RadiusGrid;
                case "RadiusPx":
                    return EditorDisplayMode.RadiusPx;
                case "ValueOnly":
                    return EditorDisplayMode.ValueOnly;
            }
            throw new Exception("Cannot unmarshal type EditorDisplayMode");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (EditorDisplayMode)untypedValue;
            switch (value)
            {
                case EditorDisplayMode.EntityTile:
                    serializer.Serialize(writer, "EntityTile");
                    return;
                case EditorDisplayMode.Hidden:
                    serializer.Serialize(writer, "Hidden");
                    return;
                case EditorDisplayMode.NameAndValue:
                    serializer.Serialize(writer, "NameAndValue");
                    return;
                case EditorDisplayMode.PointPath:
                    serializer.Serialize(writer, "PointPath");
                    return;
                case EditorDisplayMode.PointStar:
                    serializer.Serialize(writer, "PointStar");
                    return;
                case EditorDisplayMode.RadiusGrid:
                    serializer.Serialize(writer, "RadiusGrid");
                    return;
                case EditorDisplayMode.RadiusPx:
                    serializer.Serialize(writer, "RadiusPx");
                    return;
                case EditorDisplayMode.ValueOnly:
                    serializer.Serialize(writer, "ValueOnly");
                    return;
            }
            throw new Exception("Cannot marshal type EditorDisplayMode");
        }

        public static readonly EditorDisplayModeConverter Singleton = new EditorDisplayModeConverter();
    }

    internal class EditorDisplayPosConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(EditorDisplayPos) || t == typeof(EditorDisplayPos?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Above":
                    return EditorDisplayPos.Above;
                case "Beneath":
                    return EditorDisplayPos.Beneath;
                case "Center":
                    return EditorDisplayPos.Center;
            }
            throw new Exception("Cannot unmarshal type EditorDisplayPos");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (EditorDisplayPos)untypedValue;
            switch (value)
            {
                case EditorDisplayPos.Above:
                    serializer.Serialize(writer, "Above");
                    return;
                case EditorDisplayPos.Beneath:
                    serializer.Serialize(writer, "Beneath");
                    return;
                case EditorDisplayPos.Center:
                    serializer.Serialize(writer, "Center");
                    return;
            }
            throw new Exception("Cannot marshal type EditorDisplayPos");
        }

        public static readonly EditorDisplayPosConverter Singleton = new EditorDisplayPosConverter();
    }

    internal class LimitBehaviorConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(LimitBehavior) || t == typeof(LimitBehavior?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "DiscardOldOnes":
                    return LimitBehavior.DiscardOldOnes;
                case "MoveLastOne":
                    return LimitBehavior.MoveLastOne;
                case "PreventAdding":
                    return LimitBehavior.PreventAdding;
            }
            throw new Exception("Cannot unmarshal type LimitBehavior");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (LimitBehavior)untypedValue;
            switch (value)
            {
                case LimitBehavior.DiscardOldOnes:
                    serializer.Serialize(writer, "DiscardOldOnes");
                    return;
                case LimitBehavior.MoveLastOne:
                    serializer.Serialize(writer, "MoveLastOne");
                    return;
                case LimitBehavior.PreventAdding:
                    serializer.Serialize(writer, "PreventAdding");
                    return;
            }
            throw new Exception("Cannot marshal type LimitBehavior");
        }

        public static readonly LimitBehaviorConverter Singleton = new LimitBehaviorConverter();
    }

    internal class RenderModeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(RenderMode) || t == typeof(RenderMode?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Cross":
                    return RenderMode.Cross;
                case "Ellipse":
                    return RenderMode.Ellipse;
                case "Rectangle":
                    return RenderMode.Rectangle;
                case "Tile":
                    return RenderMode.Tile;
            }
            throw new Exception("Cannot unmarshal type RenderMode");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (RenderMode)untypedValue;
            switch (value)
            {
                case RenderMode.Cross:
                    serializer.Serialize(writer, "Cross");
                    return;
                case RenderMode.Ellipse:
                    serializer.Serialize(writer, "Ellipse");
                    return;
                case RenderMode.Rectangle:
                    serializer.Serialize(writer, "Rectangle");
                    return;
                case RenderMode.Tile:
                    serializer.Serialize(writer, "Tile");
                    return;
            }
            throw new Exception("Cannot marshal type RenderMode");
        }

        public static readonly RenderModeConverter Singleton = new RenderModeConverter();
    }

    internal class TileRenderModeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TileRenderMode) || t == typeof(TileRenderMode?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Crop":
                    return TileRenderMode.Crop;
                case "Stretch":
                    return TileRenderMode.Stretch;
            }
            throw new Exception("Cannot unmarshal type TileRenderMode");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TileRenderMode)untypedValue;
            switch (value)
            {
                case TileRenderMode.Crop:
                    serializer.Serialize(writer, "Crop");
                    return;
                case TileRenderMode.Stretch:
                    serializer.Serialize(writer, "Stretch");
                    return;
            }
            throw new Exception("Cannot marshal type TileRenderMode");
        }

        public static readonly TileRenderModeConverter Singleton = new TileRenderModeConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "AutoLayer":
                    return TypeEnum.AutoLayer;
                case "Entities":
                    return TypeEnum.Entities;
                case "IntGrid":
                    return TypeEnum.IntGrid;
                case "Tiles":
                    return TypeEnum.Tiles;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.AutoLayer:
                    serializer.Serialize(writer, "AutoLayer");
                    return;
                case TypeEnum.Entities:
                    serializer.Serialize(writer, "Entities");
                    return;
                case TypeEnum.IntGrid:
                    serializer.Serialize(writer, "IntGrid");
                    return;
                case TypeEnum.Tiles:
                    serializer.Serialize(writer, "Tiles");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }

    internal class BgPosConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(BgPos) || t == typeof(BgPos?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Contain":
                    return BgPos.Contain;
                case "Cover":
                    return BgPos.Cover;
                case "CoverDirty":
                    return BgPos.CoverDirty;
                case "Unscaled":
                    return BgPos.Unscaled;
            }
            throw new Exception("Cannot unmarshal type BgPos");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (BgPos)untypedValue;
            switch (value)
            {
                case BgPos.Contain:
                    serializer.Serialize(writer, "Contain");
                    return;
                case BgPos.Cover:
                    serializer.Serialize(writer, "Cover");
                    return;
                case BgPos.CoverDirty:
                    serializer.Serialize(writer, "CoverDirty");
                    return;
                case BgPos.Unscaled:
                    serializer.Serialize(writer, "Unscaled");
                    return;
            }
            throw new Exception("Cannot marshal type BgPos");
        }

        public static readonly BgPosConverter Singleton = new BgPosConverter();
    }

    internal class WorldLayoutConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(WorldLayout) || t == typeof(WorldLayout?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Free":
                    return WorldLayout.Free;
                case "GridVania":
                    return WorldLayout.GridVania;
                case "LinearHorizontal":
                    return WorldLayout.LinearHorizontal;
                case "LinearVertical":
                    return WorldLayout.LinearVertical;
            }
            throw new Exception("Cannot unmarshal type WorldLayout");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (WorldLayout)untypedValue;
            switch (value)
            {
                case WorldLayout.Free:
                    serializer.Serialize(writer, "Free");
                    return;
                case WorldLayout.GridVania:
                    serializer.Serialize(writer, "GridVania");
                    return;
                case WorldLayout.LinearHorizontal:
                    serializer.Serialize(writer, "LinearHorizontal");
                    return;
                case WorldLayout.LinearVertical:
                    serializer.Serialize(writer, "LinearVertical");
                    return;
            }
            throw new Exception("Cannot marshal type WorldLayout");
        }

        public static readonly WorldLayoutConverter Singleton = new WorldLayoutConverter();
    }
}
#pragma warning restore 1591
#pragma warning restore 1570