using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Mycom.TargetDemoApp.Extensions
{
    internal static class VisualTreeExtensions
    {
        public static IEnumerable<DependencyObject> GetAllChildren(this DependencyObject source)
        {
            var count = VisualTreeHelper.GetChildrenCount(source);
            if (count <= 0)
            {
                yield break;
            }

            for (var i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(source, i);
                yield return child;
                foreach (var innerChild in GetAllChildren(child))
                {
                    yield return innerChild;
                }
            }
        }
    }
}