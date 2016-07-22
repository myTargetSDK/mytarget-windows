using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Mycom.TargetDemoApp.ViewModels;

namespace Mycom.TargetDemoApp.Views
{
    using static ItemViewConstatns;

    internal sealed class DefaultItemView : UserControl
    {
        private static readonly Binding DescriptionTextBinding = new Binding
                                                                 {
                                                                     Path = new PropertyPath(nameof(DefaultItemViewModel.Description)),
                                                                     Mode = BindingMode.OneTime
                                                                 };

        private static readonly Binding ImageBorderBackgroundBinding = new Binding
                                                                       {
                                                                           Path = new PropertyPath(nameof(DefaultItemViewModel.Background)),
                                                                           Mode = BindingMode.OneTime
                                                                       };

        private static readonly Binding ImageSourceBinding = new Binding
                                                             {
                                                                 Path = new PropertyPath(nameof(DefaultItemViewModel.ImageSource))
                                                             };

        private static readonly Binding TitleTextBinding = new Binding
                                                           {
                                                               Path = new PropertyPath(nameof(DefaultItemViewModel.Title)),
                                                               Mode = BindingMode.OneTime
                                                           };

        public DefaultItemView()
        {
            var image = new Image
                        {
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Stretch = Stretch.Uniform
                        };

            BindingOperations.SetBinding(image,
                                         Image.SourceProperty,
                                         ImageSourceBinding);

            var innerBorder = new Border
                              {
                                  CornerRadius = InnerBordeCornerRadius,
                                  Padding = InnerBorderPadding,
                                  Child = image
                              };

            Grid.SetRow(innerBorder, 0);

            BindingOperations.SetBinding(innerBorder,
                                         Border.BackgroundProperty,
                                         ImageBorderBackgroundBinding);

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
                                           MaxLines = 2
                                       };

            Grid.SetRow(descriptionTextBlock, 2);

            BindingOperations.SetBinding(descriptionTextBlock,
                                         TextBlock.TextProperty,
                                         DescriptionTextBinding);

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
    }
}