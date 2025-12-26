using System;
using System.Diagnostics.CodeAnalysis;

namespace Meta;

/// <summary>
/// Represents a token that identifies a special Material icon, including its name and associated metadata.
/// </summary>
public record MaterialIconToken() {
    /// <summary>
    /// Gets the name associated with the icon.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets the icon data path.
    /// </summary>
    public string DataPath { get; init; } = "string.Empty";

    /// <summary>
    /// Gets the index of the associated enumeration value, or null if not set (Automatic).
    /// </summary>
    public int? EnumIndex { get; init; }

    [SetsRequiredMembers]
    public MaterialIconToken(string name) : this() {
        Name = name;
    }

    [SetsRequiredMembers]
    public MaterialIconToken(string name, string dataPath) : this() {
        Name = name;
        DataPath = dataPath;
    }

    [SetsRequiredMembers]
    public MaterialIconToken(string name, int enumIndex) : this() {
        Name = name;
        EnumIndex = enumIndex;
    }

    /// <summary>
    /// The string representation of the enum member definition for use in source code generation.
    /// </summary>
    /// <remarks>If the enum member has an explicit value, the returned string includes the assignment (e.g.,
    /// "Name = 1,"); otherwise, it includes only the name followed by a comma (e.g., "Name,").</remarks>
    /// <returns>A string containing the enum member name, optionally followed by an explicit value assignment, formatted for
    /// inclusion in an enum declaration.</returns>
    public string EnumDefinition =>
        EnumIndex is null
            ? $"{Name},"
            : $"{Name} = {EnumIndex},";

    /// <summary>
    /// Gets a string that represents the mapping from the name to the data path in the format Name => DataPath,.
    /// </summary>
    public string DataPathSwitchDefinition => DataPath.StartsWith("string", StringComparison.OrdinalIgnoreCase)
        ? $"MaterialIconKind.{Name} => {DataPath},"
        : $"MaterialIconKind.{Name} => \"{DataPath}\",";

    /// <summary>
    /// Gets a string that represents the mapping from the name to the data path in the format {{Name, DataPath}},.
    /// </summary>
    public string DataPathDictionaryDefinition => DataPath.StartsWith("string", StringComparison.OrdinalIgnoreCase)
        ? $"{{{{MaterialIconKind.{Name}, {DataPath}}}}},"
        : $"{{{{MaterialIconKind.{Name}, \"{DataPath}}}}}\",";

    /// <summary>
    /// Provides predefined tokens representing special material icon display states, such as invisible or transparent
    /// icons.
    /// </summary>
    /// <remarks>Use these tokens to control whether a material icon is rendered or whether space is reserved
    /// for it in the layout. The 'Invisible' token hides the icon without reserving space, while the 'Transparent'
    /// token hides the icon but maintains its layout space.</remarks>
    public static readonly MaterialIconToken[] Tokens = [
        new("Invisible", -1), // Do not display the icon, and do not reserve space for it in layout.
        new("Transparent", 0) // Do not display the icon, but reserve space for the element in layout. (Default icon)
    ];
}