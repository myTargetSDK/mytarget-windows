using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Mycom.TargetDemoApp.Resources;

namespace Mycom.TargetDemoApp.Views
{
    internal sealed class CustomFlyout : FlyoutBase
    {
        private readonly FrameworkElement _viewElement;

        public CustomFlyout(FrameworkElement viewElement)
        {
            _viewElement = viewElement;
        }

        protected override Control CreatePresenter()
        {
            return new ContentControl
                   {
                       Content = new Grid
                                 {
                                     Background = Brushes.Brush80000000,
                                     Children =
                                     {
                                         new Border
                                         {
                                             Child = _viewElement,
                                             Background = Brushes.BrushFFFFFFFF,
                                             VerticalAlignment = VerticalAlignment.Center,
                                             HorizontalAlignment = HorizontalAlignment.Stretch,
                                             Margin = new Thickness(12.0),
                                             Padding = new Thickness(12.0),
                                             CornerRadius = new CornerRadius(3.0)
                                         }
                                     },
                                     RequestedTheme = ElementTheme.Light
                                 },
                       HorizontalContentAlignment = HorizontalAlignment.Stretch,
                       VerticalContentAlignment = VerticalAlignment.Stretch
                   };
        }
    }
}