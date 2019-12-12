using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;
using TrackerCommon;

namespace Tracker.Infrastructure
{
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
