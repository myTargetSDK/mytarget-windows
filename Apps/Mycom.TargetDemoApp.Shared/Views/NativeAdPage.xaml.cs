using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Mycom.TargetDemoApp.ViewModels;
using Mycom.TargetDemoApp.Extensions;

namespace Mycom.TargetDemoApp.Views
{
    internal sealed partial class NativeAdPage
    {
        public NativeAdPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (DataContext == null)
            {
                DataContext = e.Parameter == null ? new NativeAdPageViewModel() : new NativeAdPageViewModel(Int32.Parse(e.Parameter.ToString()));
            }
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

            flipViewScrollViewer.ViewChanging+= (o, args) =>
            {
                var currentOffset = flipViewScrollViewer.HorizontalOffset - initialOffset;
                var fullOffset = flipViewScrollViewer.ScrollableWidth - initialOffset;
                listBoxScrollViewer.ChangeView(currentOffset / fullOffset * listBoxScrollViewer.ScrollableWidth, null, null, false);
            };
        }

        private void OnUpdateTapped(Object sender, TappedRoutedEventArgs e)
        {
            (DataContext as NativeAdPageViewModel).Update();
        }
    }

    internal sealed class NativeAdDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ChatListTemplate { get; set; }

        public DataTemplate ContentStreamTemplate { get; set; }

        public DataTemplate ContentWallTemplate { get; set; }

        public DataTemplate NewsFeedTemplate { get; set; }

        public DataTemplate NullTemplate { get; set; }

        public DataTemplate ObjectTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(Object item, DependencyObject container)
        {
            return SelectTemplateCore(item);
        }

        protected override DataTemplate SelectTemplateCore(Object item)
        {
            if (item == null)
            {
                return NullTemplate;
            }

            if (item is NativeAdWrapperViewModel)
            {
                switch (((NativeAdWrapperViewModel) item).DesiredViewType)
                {
                    case NativeAdViewType.ContentStream:
                        return ContentStreamTemplate;
                    case NativeAdViewType.NewsFeed:
                        return NewsFeedTemplate;
                    case NativeAdViewType.ContentWall:
                        return ContentWallTemplate;
                    case NativeAdViewType.ChatList:
                        return ChatListTemplate;
                    default:
                        return null;
                }
            }

            return ObjectTemplate;
        }
    }
}