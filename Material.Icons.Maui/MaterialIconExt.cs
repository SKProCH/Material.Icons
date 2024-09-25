

namespace Material.Icons.Maui;

public class MaterialIconExt : IMarkupExtension<MaterialIcon> {

    public MaterialIconExt() { }

    public MaterialIconExt(MaterialIconKind kind) {
        Kind = kind;
    }

    public MaterialIconExt(MaterialIconKind kind, double size) {
        Kind = kind;
        Size = size;
    }

    public MaterialIconKind Kind { get; set; }

    public double? Size { get; set; }

    public MaterialIcon ProvideValue(IServiceProvider serviceProvider) {
        var result = new MaterialIcon { Kind = Kind };

        if (Size.HasValue) {
            result.WidthRequest = Size.Value;
            result.HeightRequest = Size.Value;
        }

        return result;
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) {
        return ProvideValue(serviceProvider);
    }
}
