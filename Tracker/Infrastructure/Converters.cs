using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Tracker.ECL.DTO;

using TrackerCommon;

namespace Tracker.Infrastructure
{
    // convert from decimal to string for setting GroupBox header, base string in parm

    [ValueConversion(typeof(decimal), typeof(string), ParameterType = typeof(string))]
    public sealed class DecimalToHeaderConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            string basepart = string.Empty;
            if ((parm is string basestring) && !string.IsNullOrEmpty(basestring))
            {
                basepart = basestring;
            }
            if (!(value is decimal v) || v == 0M)
            {
                return basepart;
            }
            return $"{basepart} ({v:n2} total)";
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert to / from string / int where 0 = blank

    [ValueConversion(typeof(int), typeof(string))]
    public sealed class IntToDisplayConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is int v))
            {
                return string.Empty;
            }
            return v == 0 ? string.Empty : v.ToString();
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is string v))
            {
                return 0;
            }
            if (!int.TryParse(v, out int val))
            {
                return 0;
            }
            return string.IsNullOrEmpty(v) ? 0 : val;
        }
    }

    // convert decimal to / from string

    [ValueConversion(typeof(decimal), typeof(string))]
    public sealed class DecimalConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is decimal d) || d == 0M)
            {
                return string.Empty;
            }
            return d.ToString("n2");
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is string v) || string.IsNullOrEmpty(v))
            {
                return 0M;
            }
            string val = v;
            if (v.EndsWith('.'))
            {
                val = v[..^1];
            }
            if (!decimal.TryParse(val, out decimal d))
            {
                return 0M;
            }
            return d;
        }
    }

    // convert from string to Uri which is null if string does not contains multiple lines, else return uri in parm

    [ValueConversion(typeof(string), typeof(Uri), ParameterType = typeof(string))]
    public sealed class StringToMultiLineUriConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is string text) || !(parm is string uri))
            {
                return null;
            }
            string[] parts = text.Split(Constants.CRLF, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
            {
                return null;
            }
            return new Uri(uri, UriKind.Relative);
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from PasswordStrength to description

    [ValueConversion(typeof(PasswordStrength), typeof(string))]
    public sealed class PasswordStrengthConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is PasswordStrength ps))
            {
                return null;
            }
            return ps.GetDescriptionFromEnumValue();
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from PasswordStrength to foreground color

    [ValueConversion(typeof(PasswordStrength), typeof(SolidColorBrush))]
    public sealed class PasswordStrengthToForegroundConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is PasswordStrength ps))
            {
                return Brushes.Black;
            }
            return ps switch
            {
                PasswordStrength.VeryWeak => new SolidColorBrush(Color.FromArgb(255, 128, 0, 0)),
                PasswordStrength.Weak => new SolidColorBrush(Color.FromArgb(255, 64, 0, 0)),
                PasswordStrength.Medium => Brushes.Gray,
                PasswordStrength.Strong => new SolidColorBrush(Color.FromArgb(255, 0, 64, 0)),
                PasswordStrength.VeryStrong => new SolidColorBrush(Color.FromArgb(255, 0, 128, 0)),
                _ => Brushes.Black
            };
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from bool (IsPasswordProtected) to string for tooltip

    [ValueConversion(typeof(bool), typeof(string))]
    public sealed class HashToTooltipConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is bool ispwprotected))
            {
                return null;
            }
            return ispwprotected ? "Disable password protection (Alt-W)" : "Enable password protection (Alt-W)";
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from bool to uri where true = checkmark, false = x (overridden with parm)

    [ValueConversion(typeof(bool), typeof(Uri), ParameterType = typeof(string))]
    public sealed class BoolToUriConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            string falseuri = "/resources/x-32.png";
            if (parm is string furi && !string.IsNullOrEmpty(furi))
            {
                falseuri = furi;
            }
            if (!(value is bool v))
            {
                return null;
            }
            return v ? new Uri(Constants.Checkmark, UriKind.Relative) : new Uri(falseuri, UriKind.Relative);
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from ExploretItemType to Icon uri

    [ValueConversion(typeof(ExplorerItemType), typeof(Uri))]
    public sealed class ExplorerItemTypeConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is ExplorerItemType type))
            {
                return null;
            }
            return type.GetIconFromEnumValue();
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from "/resources/foo-32.png" to "Foo"

    [ValueConversion(typeof(string), typeof(string))]
    public sealed class StringToHeaderConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is string uri))
            {
                return string.Empty;
            }
            return uri.Replace("resources/", "").Replace("-32.png", "").TrimStart('/');
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from string to uri with urikind in parm, default Relative

    [ValueConversion(typeof(string), typeof(Uri), ParameterType = typeof(string))]
    public sealed class StringToUriConverter : IValueConverter
    {
        private static readonly Dictionary<string, UriKind> kinds = new Dictionary<string, UriKind>
        {
            ["r"] = UriKind.Relative,
            ["a"] = UriKind.Absolute,
            ["ra"] = UriKind.RelativeOrAbsolute
        };

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is string spec) || string.IsNullOrEmpty(spec))
            {
                return null;
            }
            UriKind kind = UriKind.Relative;
            if (parm is string k)
            {
                if (kinds.ContainsKey(k.ToLower()))
                {
                    kind = kinds[k.ToLower()];
                }
            }
            return new Uri(spec, kind);
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from string to Image

    [ValueConversion(typeof(string), typeof(Image))]
    public sealed class StringToImageConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is string v) || string.IsNullOrEmpty(v))
            {
                return null;
            }
            if (!(new StringToUriConverter().Convert(v, typeof(Uri), "r", CultureInfo.CurrentCulture) is Uri uri))
            {
                return null;
            }
            return new Image { Source = new BitmapImage(uri) };
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from string to square color patch for menuitem icons width and height in parm, default 20

    [ValueConversion(typeof(string), typeof(Border), ParameterType = typeof(int))]
    public sealed class StringToColorSquareConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            int dimension = 20;
            if (parm is int)
            {
                dimension = (int)parm;
                if (dimension <= 0)
                {
                    dimension = 20;
                }
            }
            if (!(value is string color))
            {
                return null;
            }
            Border border = new Border
            {
                BorderBrush = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                Width = dimension,
                Height = dimension,
                Background = Pallette.GetBrush(color)
            };
            return border;
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from string to stackpanel for color menu items

    [ValueConversion(typeof(string), typeof(StackPanel))]
    public sealed class StringToMenuItemHeaderConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is string color))
            {
                return null;
            }
            StackPanel sp = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            SolidColorBrush brush = Pallette.GetBrush(color);
            Border border = new Border
            {
                BorderBrush = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                Background = brush,
                Width = 20,
                Height = 20
            };
            sp.Children.Add(border);
            TextBlock tb = new TextBlock
            {
                Margin = new Thickness(5, 0, 0, 0),
                Text = color
            };
            sp.Children.Add(tb);
            return sp;
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from Client Type to background colod

    [ValueConversion(typeof(ClientType), typeof(SolidColorBrush))]
    public sealed class ClientTypeToBackgroundConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is ClientType c))
            {
                return Brushes.White;
            }
            return c.ARGB.GetBrush();
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from Color Name to SolidColorBrush

    [ValueConversion(typeof(string), typeof(SolidColorBrush))]
    public sealed class ColorNameToBrushConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is string name))
            {
                return Brushes.Transparent;
            }
            if (Pallette.HasColor(name))
            {
                return Pallette.GetBrush(name);
            }
            string color;
            if (name.StartsWith("#"))
            {
                color = name[1..];
            }
            else if (name.StartsWith("0x"))
            {
                color = name[2..];
            }
            else
            {
                color = name;
            }
            if (Regex.IsMatch(color, Constants.HexPattern))
            {
                if (!long.TryParse(color, NumberStyles.HexNumber, null, out long argb))
                {
                    return Brushes.Transparent;
                }
                return argb.GetBrush();
            }
            else if (Regex.IsMatch(color, Constants.NumberPattern))
            {
                if (!long.TryParse(color, out long argb))
                {
                    return Brushes.Transparent;
                }
                return argb.GetBrush();
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from (possible) multi-line string to first line

    [ValueConversion(typeof(string), typeof(string))]
    public sealed class StringToFirstLineConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is string v))
            {
                return null;
            }
            string[] parts = v.Split(Constants.CRLF, StringSplitOptions.RemoveEmptyEntries);
            return parts[0];
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from object to visibility where null (selected item) = visible, else collapsed, inverted with parm

    [ValueConversion(typeof(object), typeof(Visibility), ParameterType = typeof(bool))]
    public sealed class SelectedItemToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            bool invert = false;
            if (parm is bool)
            {
                invert = (bool)parm;
            }
            return (value, invert) switch
            {
                (null, false) => Visibility.Visible,
                (null, true) => Visibility.Collapsed,
                (_, false) => Visibility.Collapsed,
                (_, true) => Visibility.Visible
            };
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from object to enabled where null = false, inverted with parm

    [ValueConversion(typeof(object), typeof(bool), ParameterType = typeof(bool))]
    public sealed class ObjectToEnabledConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            bool invert = false;
            if (parm is bool)
            {
                invert = (bool)parm;
            }
            return invert ? value is null : value != null;
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from count to visibility where 0 = visible else collapsed, inverted with parm

    [ValueConversion(typeof(int), typeof(Visibility), ParameterType = typeof(bool))]
    public sealed class CountToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            bool invert = false;
            if (parm is bool)
            {
                invert = (bool)parm;
            }
            if (!(value is int count))
            {
                return invert ? Visibility.Collapsed : Visibility.Visible;
            }
            return (count, invert) switch
            {
                (0, false) => Visibility.Visible,
                (0, true) => Visibility.Collapsed,
                (_, false) => Visibility.Collapsed,
                (_, true) => Visibility.Visible
            };
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from two double to "xxx of xxx used"

    public sealed class DoublesToTooltipConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type t, object parm, CultureInfo lang)
        {
            if (values.Length != 2 || values.Any(x => x == DependencyProperty.UnsetValue))
            {
                return null;
            }
            if (!(values[0] is double used) || !(values[1] is double quota))
            {
                return null;
            }
            string strused = Tools.Normalize(used);
            string strquota = Tools.Normalize(quota);
            if (quota == double.MaxValue)
            {
                return $"{strused} used";
            }
            return $"{strused} used out of {strquota}";
        }

        public object[] ConvertBack(object value, Type[] t, object parm, CultureInfo lang)
        {
            throw new NotImplementedException();
        }
    }

    // convert from double to background color, where <= 0.75 = green, <= 0.9 = yellow, else red

    [ValueConversion(typeof(double), typeof(SolidColorBrush))]
    public sealed class DoubleToColorConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is double d))
            {
                return Brushes.Transparent;
            }
            return d <= 0.75 ? Brushes.Green : d <= 0.9 ? Brushes.Yellow : Brushes.Red;
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from Visibility (StatusbarVisibility) to string for menu header

    [ValueConversion(typeof(Visibility), typeof(string))]
    public sealed class VisibilityToMenuHeaderConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is Visibility vis))
            {
                return "Unknown";
            }
            if (vis == Visibility.Visible)
            {
                return "Hide Status bar";
            }
            return "Show Status bar";
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    // convert from Visibility (StatusbarVisibility) to icon uri for menu item

    [ValueConversion(typeof(Visibility), typeof(Uri))]
    public sealed class VisibilityToMenuIconConverter : IValueConverter
    {

        public object Convert(object value, Type t, object parm, CultureInfo lang)
        {
            if (!(value is Visibility vis))
            {
                return null;
            }
            if (vis == Visibility.Visible)
            {
                return new Uri(Constants.Cancel, UriKind.Relative);
            }
            return new Uri("/resources/view-32.png", UriKind.Relative);
        }

        public object ConvertBack(object value, Type t, object parm, CultureInfo lang)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
