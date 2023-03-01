using System.Linq;
using NuGet.Versioning;
public static class ExtensionMethods {
    public static NuGetVersion BumpLastVersion(this NuGetVersion version) {
        if (version.IsPrerelease) {
            var targetReleaseLabels = version.ReleaseLabels.ToList();
            var lastLabel = targetReleaseLabels.Last();
            if (int.TryParse(lastLabel, out var label)) {
                // Removing last label if it is number
                // For example remove 2 from 1.0.0-preview1.2
                targetReleaseLabels.RemoveAt(targetReleaseLabels.Count - 1);
            }

            targetReleaseLabels.Add((label + 1).ToString());

            return new NuGetVersion(version.Major, version.Minor, version.Patch, targetReleaseLabels, null);
        }

        return new NuGetVersion(version.Major, version.Minor, version.Patch + 1);
    }
}
