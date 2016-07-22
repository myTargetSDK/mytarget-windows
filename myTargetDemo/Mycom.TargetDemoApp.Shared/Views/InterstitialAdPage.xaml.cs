using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Mycom.TargetDemoApp.ViewModels;

namespace Mycom.TargetDemoApp.Views
{
    public sealed partial class InterstitialAdPage
    {
        public InterstitialAdPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (DataContext == null)
            {
                DataContext = new InterstitialAdPageViewModel();
            }
        }

        private void WrapGridSizeChanged(Object sender, SizeChangedEventArgs e)
        {
            var wrapGrid = sender as ItemsWrapGrid;
            if (wrapGrid == null)
            {
                return;
            }

            wrapGrid.ItemWidth = (Int32)(e.NewSize.Width / 2.0);
        }

        private void OnItemClick(Object sender, ItemClickEventArgs e)
        {
            (DataContext as InterstitialAdPageViewModel)?.Show(e.ClickedItem);
        }

        private void OnUpdateTapped(Object sender, TappedRoutedEventArgs e)
        {
            (DataContext as InterstitialAdPageViewModel)?.Update();
        }
    }
}