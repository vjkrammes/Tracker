using System;
using System.Linq;
using System.Windows.Media;

namespace Tracker.Infrastructure
{
    public static partial class ExtensionMethods
    {
        private static readonly Capitalizer _capitalizer;

        static ExtensionMethods()
        {
            _capitalizer = new Capitalizer();
        }

        public static string Capitalize(this string value) =>
            value.First().ToString().ToUpper() + string.Join(string.Empty, value.Skip(1));

        public static string Caseify(this string value) => _capitalizer.Transform(value);

        public static SolidColorBrush GetBrush(this long argb)
        {
            byte a, r, g, b;
            a = (byte)((argb >> 24) & 0xff);
            r = (byte)((argb >> 16) & 0xff);
            g = (byte)((argb >> 8) & 0xff);
            b = (byte)(argb & 0xff);
            return new SolidColorBrush(Color.FromArgb(a, r, g, b));
        }
    }
}
