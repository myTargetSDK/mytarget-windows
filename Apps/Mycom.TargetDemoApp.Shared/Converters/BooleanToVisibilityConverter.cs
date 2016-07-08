using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Mycom.TargetDemoApp.Converters
{
    internal sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, String language) =>
            (Boolean) value ? Visibility.Visible : Visibility.Collapsed;

        public Object ConvertBack(Object value, Type targetType, Object parameter, String language) =>
            (Visibility) value == Visibility.Visible;
    }
}