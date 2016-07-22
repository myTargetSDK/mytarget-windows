using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Mycom.TargetDemoApp.ViewModels;
using Mycom.TargetDemoApp.Views;

namespace Mycom.TargetDemoApp.Services
{
    internal static class DialogService
    {
        public static void Show<TViewModel>(TViewModel viewModel) where TViewModel : ICloseNotify
        {
            var xamlType = (Application.Current as IXamlMetadataProvider)?.GetXamlType(viewModel.GetType().FullName.Replace("ViewModel", "View"));
            var xamlInstance = xamlType?.ActivateInstance() as FrameworkElement;
            if (xamlInstance == null)
            {
                return;
            }

            xamlInstance.DataContext = viewModel;

            var customFlyout = new CustomFlyout(xamlInstance)
                               {
                                   Placement = FlyoutPlacementMode.Full
                               };
            customFlyout.ShowAt(Window.Current.Content as FrameworkElement);
            viewModel.CloseRequest += customFlyout.Hide;
        }
    }
}