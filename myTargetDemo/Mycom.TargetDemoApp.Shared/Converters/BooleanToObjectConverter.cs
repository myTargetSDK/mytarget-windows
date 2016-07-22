using System;
using Windows.UI.Xaml.Data;

namespace Mycom.TargetDemoApp.Converters
{
    internal sealed class BooleanToObjectConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, String language) =>
            (Boolean) value ? new Object() : null;

        public Object ConvertBack(Object value, Type targetType, Object parameter, String language) =>
            value == null;
    }
}