
using Microsoft.Maui.Controls.Shapes;

namespace Material.Icons.Maui;

public class MaterialIconGeometryExtension : IMarkupExtension<Geometry> {
    private readonly PathGeometryConverter _converter = new();

    public MaterialIconGeometryExtension() { }

    public MaterialIconGeometryExtension(MaterialIconKind kind) {
        Kind = kind;
    }

    public MaterialIconKind Kind { get; set; }

    public Geometry ProvideValue(IServiceProvider serviceProvider) {
        var provider =
            serviceProvider.GetService<MaterialIconDataProvider>()
            ?? MaterialIconDataProvider.Instance;
        var data = provider.ProvideData(Kind);
        var geometry = (Geometry?)_converter.ConvertFromInvariantString(data);
        if (geometry == null) {
            var li = (serviceProvider.GetService(typeof(IXmlLineInfoProvider)) is IXmlLineInfoProvider lip) ? lip.XmlLineInfo : new XmlLineInfo();
            throw new XamlParseException($"Geometry data not parsable for icon {Kind}.", li);
        }

        return geometry;
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) {
        return ProvideValue(serviceProvider);
    }
}
