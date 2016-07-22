using System;
using Windows.UI.Xaml.Input;
using Mycom.TargetDemoApp.ViewModels;

namespace Mycom.TargetDemoApp.Views
{
    public sealed partial class AddCustomSlotView
    {
        public AddCustomSlotView()
        {
            InitializeComponent();
        }

        private void Cancle(Object sender, TappedRoutedEventArgs e)
        {
            (DataContext as AddCustomSlotViewModel)?.Close();
        }

        private void Confirm(Object sender, TappedRoutedEventArgs e)
        {
            (DataContext as AddCustomSlotViewModel)?.Confirm();
        }
    }
}