using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Mycom.TargetDemoApp.ViewModels;

namespace Mycom.TargetDemoApp.Views
{
    internal class SimpleCustomView : Grid
    {
        private static void RemoveIconHolderOnPointerPressed(Object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }

        public SimpleCustomView()
        {
            var removeIcon = new TextBlock
                             {
                                 Foreground = ItemViewConstatns.RemoveIconForegroundBrush,
                                 FontSize = 20.0,
                                 FontFamily = ItemViewConstatns.RemoveIconFontFamily,
                                 Text = ItemViewConstatns.RemoveIconText
                             };

            var removeIconHolder = new Border
                                   {
                                       Padding = ItemViewConstatns.RemoveIconHolderPadding,
                                       Child = removeIcon,
                                       Background = ItemViewConstatns.RemoveIconHolderBackground,
                                       HorizontalAlignment = HorizontalAlignment.Right,
                                       VerticalAlignment = VerticalAlignment.Top
                                   };

            SetRow(removeIconHolder, 0);

            Children.Add(new CustomItemView());
            Children.Add(removeIconHolder);

            removeIconHolder.Tapped += RemoveIconHolderOnTapped;
            removeIconHolder.PointerPressed += RemoveIconHolderOnPointerPressed;
        }

        private void RemoveIconHolderOnTapped(Object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            (DataContext as CustomItemViewModel)?.Remove();
        }
    }
}