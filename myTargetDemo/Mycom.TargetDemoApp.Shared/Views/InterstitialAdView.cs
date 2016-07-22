using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Mycom.TargetDemoApp.Converters;
using Mycom.TargetDemoApp.Resources;
using Mycom.TargetDemoApp.ViewModels;

namespace Mycom.TargetDemoApp.Views
{
    using static ItemViewConstatns;

    internal class InterstitialAdView : Grid
    {
        private const Double ProgressHeight = 10.0;

        private static readonly Brush CowerBackgroundBrush = Brushes.Brush80000000;

        private static readonly Binding LoadingVisibilityBinding = new Binding
                                                                   {
                                                                       Path = new PropertyPath(nameof(InterstitialAdViewModel.IsLoading)),
                                                                       Converter = new BooleanToVisibilityConverter()
                                                                   };

        private static readonly Binding ProgressIsIndeterminateBinding = new Binding
                                                                         {
                                                                             Path = new PropertyPath(nameof(InterstitialAdViewModel.IsLoading))
                                                                         };

        private static readonly Thickness ProgressMargin = new Thickness(3, 0, 3, 0);

        public InterstitialAdView()
        {
            var cower = new Border
                        {
                            Margin = OutterBorderMargin,
                            CornerRadius = OutterBorderCornerRadius,
                            Background = CowerBackgroundBrush
                        };

            BindingOperations.SetBinding(cower,
                                         VisibilityProperty,
                                         LoadingVisibilityBinding);

            var progress = new ProgressBar
                           {
                               Height = ProgressHeight,
                               Margin = ProgressMargin,
                               HorizontalAlignment = HorizontalAlignment.Stretch,
                               VerticalAlignment = VerticalAlignment.Top,
                               HorizontalContentAlignment = HorizontalAlignment.Stretch,
                               Foreground = Brushes.BrushFFFFFFFF
                           };

            BindingOperations.SetBinding(progress,
                                         ProgressBar.IsIndeterminateProperty,
                                         ProgressIsIndeterminateBinding);

            BindingOperations.SetBinding(progress,
                                         VisibilityProperty,
                                         LoadingVisibilityBinding);

            Children.Add(new DefaultItemView());
            Children.Add(cower);
            Children.Add(progress);
        }
    }
}