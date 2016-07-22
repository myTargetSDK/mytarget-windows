using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Mycom.TargetDemoApp.ViewModels;

namespace Mycom.TargetDemoApp.Views
{
    internal sealed partial class StartPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (DataContext == null)
            {
                DataContext = new StartPageViewModel(Frame);
            }
        }

        private void OnItemClick(Object sender, ItemClickEventArgs e)
        {
            (DataContext as StartPageViewModel)?.OnItemClicked(e.ClickedItem);
        }

        private void WrapGridSizeChanged(Object sender, SizeChangedEventArgs e)
        {
            var wrapGrid = sender as ItemsWrapGrid;
            if (wrapGrid == null)
            {
                return;
            }

            wrapGrid.ItemWidth = (Int32) (e.NewSize.Width / 2.0);
        }
    }

    internal sealed class ItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CustomItemTemplate { get; set; }

        public DataTemplate DefaultItemTemplate { get; set; }

        public DataTemplate InterstitialItemTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(Object item, DependencyObject container)
        {
            return SelectTemplateCore(item);
        }

        protected override DataTemplate SelectTemplateCore(Object item)
        {
            if (item is CustomItemViewModel)
            {
                return CustomItemTemplate;
            }

            if (item is DefaultItemViewModel)
            {
                return DefaultItemTemplate;
            }

            if (item is InterstitialCustomItemViewModel)
            {
                return InterstitialItemTemplate;
            }

            return null;
        }
    }
}