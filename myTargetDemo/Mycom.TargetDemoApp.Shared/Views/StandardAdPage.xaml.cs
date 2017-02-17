using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Mycom.TargetDemoApp.Extensions;
using Mycom.TargetDemoApp.ViewModels;

namespace Mycom.TargetDemoApp.Views
{
    internal sealed partial class StandardAdPage
    {
        public StandardAdPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (DataContext != null)
            {
                return;
            }

            Int32 slotId;
            DataContext = Int32.TryParse(e.Parameter?.ToString(), out slotId)
                              ? new StandardAdPageViewModel(slotId)
                              : new StandardAdPageViewModel();
        }

        private void OnLoaded(Object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;

            var flipViewScrollViewer = FlipView.GetAllChildren()
                                               .OfType<ScrollViewer>()
                                               .FirstOrDefault();

            var listBoxScrollViewer = ListBox.GetAllChildren()
                                             .OfType<ScrollViewer>()
                                             .FirstOrDefault();

            var initialOffset = flipViewScrollViewer.HorizontalOffset;

            flipViewScrollViewer.ViewChanging += (o, args) =>
                                                 {
                                                     var currentOffset = flipViewScrollViewer.HorizontalOffset - initialOffset;
                                                     var fullOffset = flipViewScrollViewer.ScrollableWidth - initialOffset;
                                                     listBoxScrollViewer.ChangeView(currentOffset / fullOffset * listBoxScrollViewer.ScrollableWidth, null, null, false);
                                                 };
        }

        private void OnUpdateTapped(Object sender, TappedRoutedEventArgs e)
        {
            (DataContext as StandardAdPageViewModel)?.UpdateSelectedItem();
        }
    }

    internal sealed class StandardAdTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LoremIpsumTemplate { get; set; }
        public DataTemplate NullTemplate { get; set; }
        public DataTemplate StandardAdTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(Object item)
        {
            if (item is StandardAdItemViewModel)
            {
                return StandardAdTemplate;
            }

            if (item is LoremIpsumItemViewModel)
            {
                return LoremIpsumTemplate;
            }

            return NullTemplate;
        }

        protected override DataTemplate SelectTemplateCore(Object item, DependencyObject container)
        {
            return SelectTemplateCore(item);
        }
    }
}