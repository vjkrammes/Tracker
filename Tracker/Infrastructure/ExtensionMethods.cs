using System;
using System.Linq;
using System.Windows.Media;

using TrackerCommon;

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

        public static Uri GetIconFromEnumValue<T>(this T value) where T : Enum
        {
            if (!(typeof(T)
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(ExplorerIconAttribute), false)
                .SingleOrDefault() is ExplorerIconAttribute uri))
            {
                return null;
            }
            return new Uri(uri.ExplorerIcon, UriKind.Relative);
        }

        public static SolidColorBrush GetBrush(this long argb)
        {
            byte a, r, g, b;
            a = (byte)((argb >> 24) & 0xff);
            r = (byte)((argb >> 16) & 0xff);
            g = (byte)((argb >> 8) & 0xff);
            b = (byte)(argb & 0xff);
            return new SolidColorBrush(Color.FromArgb(a, r, g, b));
        }

        public static Color Adjust(this Color color, double percent)
        {
            if (percent < 0)
            {
                percent = 0;
            }
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;
            r = Math.Min(r * percent, 1);
            g = Math.Min(g * percent, 1);
            b = Math.Min(b * percent, 1);
            byte rret = (byte)(r * 255);
            byte gret = (byte)(g * 255);
            byte bret = (byte)(b * 255);
            return Color.FromArgb(color.A, rret, gret, bret);
        }

        public static Color Adjust(this string colorname, double percent)
        {
            if (!Pallette.HasColor(colorname))
            {
                return Colors.White;
            }
            return Pallette.GetBrush(colorname).Color.Adjust(percent);
        }

        public static (Color, Color) Adjust(this Color color) =>
            (color.Adjust(Constants.DarkPercent), color.Adjust(Constants.LightPercent));
    }
}
