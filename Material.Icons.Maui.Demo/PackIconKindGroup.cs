namespace Material.Icons.Maui.Demo;

public class PackIconKindGroup {
    public PackIconKindGroup(IEnumerable<string> kinds) {
        var sortedKinds = kinds.OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToArray();
        if (sortedKinds.Length == 0)
            throw new ArgumentException($"{nameof(kinds)} must contain at least one value");
        Kind = Enum.Parse<MaterialIconKind>(sortedKinds[0]);
        DisplayName = sortedKinds[0];
        Names = sortedKinds;
    }

    public MaterialIconKind Kind { get; }
    public string DisplayName { get; }
    public string[] Names { get; }
}
