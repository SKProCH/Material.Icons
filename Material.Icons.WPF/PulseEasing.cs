using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;

namespace Material.Icons.WPF {
    public class PulseEasing : EasingFunctionBase {
        private const int Steps = 8;

        private static readonly IEnumerable<double> _steps = Enumerable
            .Range(0, Steps + 1)
            .Select(index => 1.0 / Steps * index)
            .ToArray();

        protected override double EaseInCore(double normalizedTime) {
            return _steps.Last(step => step <= normalizedTime);
        }

        protected override Freezable CreateInstanceCore() {
            return new PulseEasing();
        }
    }
}
