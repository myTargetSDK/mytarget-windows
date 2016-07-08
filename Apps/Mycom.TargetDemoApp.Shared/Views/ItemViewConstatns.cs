using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Mycom.TargetDemoApp.Resources;

namespace Mycom.TargetDemoApp.Views
{
    internal static class ItemViewConstatns
    {
        internal const String RemoveIconText = "\uE107";
        internal const String UpdateIconText = "\uE117";
        internal const Double SmallButtonSize = 40.0;

        internal static readonly Brush DescriptionForegroundBrush = new SolidColorBrush(Colors.Gray);
        internal static readonly CornerRadius InnerBordeCornerRadius = new CornerRadius(5, 5, 0, 0);
        internal static readonly Thickness InnerBorderPadding = new Thickness(12);
        internal static readonly Brush OutterBorderBackgroundBrush = new SolidColorBrush(Colors.White);
        internal static readonly Brush OutterBorderBorderBrush = new SolidColorBrush(Colors.LightGray);
        internal static readonly Thickness OutterBorderBorderThickness = new Thickness(1);
        internal static readonly CornerRadius OutterBorderCornerRadius = new CornerRadius(5);
        internal static readonly Thickness OutterBorderMargin = new Thickness(3);
        internal static readonly FontFamily RemoveIconFontFamily = new FontFamily("Segoe UI Symbol");
        internal static readonly Brush RemoveIconForegroundBrush = Brushes.BrushFFF44336;
        internal static readonly SolidColorBrush RemoveIconHolderBackground = new SolidColorBrush(Colors.Transparent);
        internal static readonly Thickness RemoveIconHolderPadding = new Thickness(10);
        internal static readonly GridLength SecondRowHeight = new GridLength(25);
        internal static readonly Thickness TextBlocksMargin = new Thickness(8, 0, 8, 0);
        internal static readonly GridLength ThirdRowHeight = new GridLength(37);
        internal static readonly Brush TitleForegroundBrush = new SolidColorBrush(Colors.DarkSlateGray);
    }
}