using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Layout;
using Avalonia.Media;
using FluentAssertions;

namespace Material.Icons.Avalonia.Tests;

public class MaterialIconTests
{
    [AvaloniaFact]
    public void MaterialIcon_Respects_FixedSize()
    {
        var sut = new MaterialIcon
        {
            Kind = MaterialIconKind.Abacus,
            Width = 100,
            Height = 100
        };

        var window = new Window { Content = sut };
        window.Show();

        sut.Bounds.Width.Should().Be(100);
        sut.Bounds.Height.Should().Be(100);
    }

    [AvaloniaFact]
    public void MaterialIcon_Respects_IconSize()
    {
        var sut = new MaterialIcon
        {
            Kind = MaterialIconKind.Abacus,
            IconSize = 48,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        var window = new Window { Content = sut };
        window.Show();

        sut.Bounds.Width.Should().Be(48);
        sut.Bounds.Height.Should().Be(48);
    }

    [AvaloniaFact]
    public void MaterialIcon_Respects_FontSize_Fallback()
    {
        var sut = new MaterialIcon
        {
            Kind = MaterialIconKind.Abacus,
            FontSize = 32,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        var window = new Window { Content = sut };
        window.Show();

        sut.Bounds.Width.Should().Be(32);
        sut.Bounds.Height.Should().Be(32);
    }

    [AvaloniaFact]
    public void MaterialIcon_Fill_Class_Stretches_To_Container()
    {
        var sut = new MaterialIcon
        {
            Kind = MaterialIconKind.Abacus,
            Classes = { "Fill" }
        };

        var parent = new Border
        {
            Width = 200,
            Height = 200,
            Child = sut
        };

        var window = new Window { Content = parent };
        window.Show();

        sut.Bounds.Width.Should().Be(200);
        sut.Bounds.Height.Should().Be(200);
    }

    [AvaloniaFact]
    public void MaterialIcon_Implements_IImage_With_Default_Size()
    {
        IImage sut = new MaterialIcon();
        sut.Size.Width.Should().Be(24);
        sut.Size.Height.Should().Be(24);
    }

    [AvaloniaFact]
    public void MaterialIcon_Populates_Geometry_On_Kind_Change()
    {
        var sut = new MaterialIcon();
        sut.Drawing.Geometry.Should().BeNull();

        sut.Kind = MaterialIconKind.Abacus;
        sut.Drawing.Geometry.Should().NotBeNull();
    }
}
