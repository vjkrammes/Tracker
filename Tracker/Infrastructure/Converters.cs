using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;
using TrackerCommon;
using Tracker.ECL.DTO;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Tracker.Infrastructure
{

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
            if (!(value is string spec))
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
            if (!(value is string v))
            {
                return null;
            }
            var uri = new StringToUriConverter().Convert(v, typeof(Uri), "r", CultureInfo.CurrentCulture) as Uri;
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
            string[] parts = v.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
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
