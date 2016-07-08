using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Mycom.TargetDemoApp.Converters;
using Mycom.TargetDemoApp.Resources;
using Mycom.TargetDemoApp.ViewModels;

namespace Mycom.TargetDemoApp.Views
{
    using static ItemViewConstatns;

    internal sealed class InterstitialItemView : Grid
    {
        private const Double ProgressHeight = 10.0;

        private static readonly Brush CowerBackgroundBrush = Brushes.Brush80000000;

        private static readonly Binding ErrorImageVisibilityBinding = new Binding
                                                                      {
                                                                          Path = new PropertyPath(nameof(InterstitialCustomItemViewModel.IsLoadingFailed)),
                                                                          Converter = new BooleanToVisibilityConverter()
                                                                      };

        private static readonly Binding LoadingVisibilityBinding = new Binding
                                                                   {
                                                                       Path = new PropertyPath(nameof(InterstitialCustomItemViewModel.IsLoading)),
                                                                       Converter = new BooleanToVisibilityConverter()
                                                                   };

        private static readonly Binding ProgressIsIndeterminateBinding = new Binding
                                                                         {
                                                                             Path = new PropertyPath(nameof(InterstitialCustomItemViewModel.IsLoading))
                                                                         };

        private static readonly Thickness ProgressMargin = new Thickness(3, 0, 3, 0);

        private static readonly Binding UpdateIconVisibilityBinding = new Binding
                                                                      {
                                                                          Path = new PropertyPath(nameof(InterstitialCustomItemViewModel.IsLoading)),
                                                                          Converter = new InvertedBooleanToVisibilityConverter()
                                                                      };

        private static void RemoveIconHolderOnPointerPressed(Object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }

        public InterstitialItemView()
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

            var removeIcon = new TextBlock
                             {
                                 Foreground = RemoveIconForegroundBrush,
                                 FontSize = 20.0,
                                 FontFamily = RemoveIconFontFamily,
                                 Text = RemoveIconText,
                                 VerticalAlignment = VerticalAlignment.Center,
                                 HorizontalAlignment = HorizontalAlignment.Center
                             };

            var removeIconHolder = new Border
                                   {
                                       Child = removeIcon,
                                       Background = RemoveIconHolderBackground,
                                       HorizontalAlignment = HorizontalAlignment.Right,
                                       VerticalAlignment = VerticalAlignment.Top,
                                       Height = SmallButtonSize,
                                       Width = SmallButtonSize
                                   };

            var updateIcont = new TextBlock
                              {
                                  Foreground = RemoveIconForegroundBrush,
                                  FontSize = 14.0,
                                  FontFamily = RemoveIconFontFamily,
                                  Text = UpdateIconText,
                                  VerticalAlignment = VerticalAlignment.Center,
                                  HorizontalAlignment = HorizontalAlignment.Center
                              };

            var updateIcontHolder = new Border
                                    {
                                        Height = SmallButtonSize,
                                        Width = SmallButtonSize,
                                        Child = updateIcont,
                                        Background = RemoveIconHolderBackground,
                                        HorizontalAlignment = HorizontalAlignment.Left,
                                        VerticalAlignment = VerticalAlignment.Top
                                    };

            BindingOperations.SetBinding(updateIcontHolder,
                                         VisibilityProperty,
                                         UpdateIconVisibilityBinding);

            var imageError = new Image
                        {
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Top,
                            Stretch = Stretch.Uniform,
                            Source = new BitmapImage(new Uri("ms-appx:///Resources/Error.png")),
                            Margin = new Thickness(OutterBorderMargin.Left + InnerBorderPadding.Left,
                                                   OutterBorderMargin.Top + InnerBorderPadding.Top,
                                                   OutterBorderMargin.Right + InnerBorderPadding.Right,
                                                   OutterBorderMargin.Bottom + InnerBorderPadding.Bottom)
                        };

            BindingOperations.SetBinding(imageError,
                                         VisibilityProperty,
                                         ErrorImageVisibilityBinding);

            Children.Add(new CustomItemView());
            Children.Add(cower);
            Children.Add(progress);
            Children.Add(imageError);
            Children.Add(removeIconHolder);
            Children.Add(updateIcontHolder);

            removeIconHolder.Tapped += RemoveIconHolderOnTapped;
            updateIcontHolder.Tapped += UpdateIcontHolderOnTapped;

            removeIconHolder.PointerPressed += RemoveIconHolderOnPointerPressed;
            updateIcontHolder.PointerPressed += RemoveIconHolderOnPointerPressed;
        }

        private void RemoveIconHolderOnTapped(Object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            (DataContext as InterstitialCustomItemViewModel)?.Remove();
        }

        private void UpdateIcontHolderOnTapped(Object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            (DataContext as InterstitialCustomItemViewModel)?.Update();
        }
    }
}