using Avalonia.Media;

namespace IconGenerators.Avalonia;

public interface IIconProvider
{
    string ProvideData(string kind);
}
