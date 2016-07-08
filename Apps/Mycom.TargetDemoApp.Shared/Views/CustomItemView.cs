using System;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Mycom.TargetDemoApp.ViewModels;

namespace Mycom.TargetDemoApp.Views
{
    using static ItemViewConstatns;
    using static AdvertisementTypeHelper;

    internal sealed class CustomItemView : UserControl
    {
        private static readonly SolidColorBrush InnerBorderBackgroundBrush = new SolidColorBrush(Colors.LightGray);

        private static readonly Binding SlotIdTextBinding = new Binding
                                                            {
                                                                Path = new PropertyPath(nameof(CustomItemViewModel.SlotId)),
                                                                Mode = BindingMode.OneTime
                                                            };

        private static readonly Binding TitleTextBinding = new Binding
                                                           {
                                                               Path = new PropertyPath(nameof(CustomItemViewModel.AdvertisementType)),
                                                               Mode = BindingMode.OneWay,
                                                               Converter = new AdvertisementTypeToStringConverter()
                                                           };

        public CustomItemView()
        {
            var slotIdTextBlock = new TextBlock
                                  {
                                      HorizontalAlignment = HorizontalAlignment.Center,
                                      VerticalAlignment = VerticalAlignment.Center,
                                      Foreground = TitleForegroundBrush,
                                      FontSize = 26.0,
                                      FontWeight = FontWeights.Bold,
                                      FontStretch = FontStretch.ExtraExpanded
                                  };

            BindingOperations.SetBinding(slotIdTextBlock,
                                         TextBlock.TextProperty,
                                         SlotIdTextBinding);

            var innerBorder = new Border
                              {
                                  CornerRadius = InnerBordeCornerRadius,
                                  Padding = InnerBorderPadding,
                                  Child = slotIdTextBlock,
                                  Background = InnerBorderBackgroundBrush
                              };

            Grid.SetRow(innerBorder, 0);

            var titleTextBlock = new TextBlock
                                 {
                                     Margin = TextBlocksMargin,
                                     VerticalAlignment = VerticalAlignment.Center,
                                     FontSize = 16.0,
                                     FontWeight = FontWeights.Bold,
                                     Foreground = TitleForegroundBrush,
                                     TextLineBounds = TextLineBounds.Tight,
                                     LineStackingStrategy = LineStackingStrategy.BlockLineHeight
                                 };

            Grid.SetRow(titleTextBlock, 1);

            BindingOperations.SetBinding(titleTextBlock,
                                         TextBlock.TextProperty,
                                         TitleTextBinding);

            var descriptionTextBlock = new TextBlock
                                       {
                                           Margin = TextBlocksMargin,
                                           VerticalAlignment = VerticalAlignment.Top,
                                           FontSize = 12.0,
                                           Foreground = DescriptionForegroundBrush,
                                           LineHeight = 15.0,
                                           LineStackingStrategy = LineStackingStrategy.BlockLineHeight,
                                           TextWrapping = TextWrapping.WrapWholeWords,
                                           TextTrimming = TextTrimming.CharacterEllipsis,
                                           MaxLines = 2,
                                           Text = "(Custom ad)"
                                       };

            Grid.SetRow(descriptionTextBlock, 2);

            Content = new Border
                      {
                          Margin = OutterBorderMargin,
                          Background = OutterBorderBackgroundBrush,
                          BorderBrush = OutterBorderBorderBrush,
                          BorderThickness = OutterBorderBorderThickness,
                          CornerRadius = OutterBorderCornerRadius,
                          Child = new Grid
                                  {
                                      RowDefinitions =
                                      {
                                          new RowDefinition(),
                                          new RowDefinition { Height = SecondRowHeight },
                                          new RowDefinition { Height = ThirdRowHeight }
                                      },
                                      Children =
                                      {
                                          innerBorder,
                                          titleTextBlock,
                                          descriptionTextBlock
                                      }
                                  }
                      };
        }

        private sealed class AdvertisementTypeToStringConverter : IValueConverter
        {
            public Object Convert(Object value, Type targetType, Object parameter, String language)
            {
                return GetString((AdvertisementType) value);
            }

            public Object ConvertBack(Object value, Type targetType, Object parameter, String language)
            {
                throw new NotImplementedException();
            }
        }
    }
}