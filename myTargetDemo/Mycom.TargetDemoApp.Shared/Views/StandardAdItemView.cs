using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Mycom.Target.Ads;
using Mycom.TargetDemoApp.ViewModels;

namespace Mycom.TargetDemoApp.Views
{
    internal sealed class StandardAdItemView : ContentPresenter
    {
        private static void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var standardAdView = (StandardAdItemView) sender;

            standardAdView.Update();
        }

        private StandardAdItemViewModel _currenViewModel;
        private Boolean _isLoaded;

        public StandardAdItemView()
        {
            DataContextChanged += OnDataContextChanged;

            Loaded += (sender, args) =>
                      {
                          _isLoaded = true;
                          HandleVisibility(((DataContext as StandardAdItemViewModel)?.CanBeVisible).GetValueOrDefault());
                      };
        }

        private void HandleVisibility(Boolean canBeVisible)
        {
            if (!_isLoaded)
            {
                return;
            }

            var myTargetControl = Content as MyTargetControl;
            if (myTargetControl == null)
            {
                return;
            }

            if (canBeVisible)
            {
                myTargetControl.Start();
                myTargetControl.Resume();
            }
            else
            {
                myTargetControl.Pause();
            }
        }

        private void Update()
        {
            var viewModel = DataContext as StandardAdItemViewModel;
            if (viewModel == _currenViewModel)
            {
                return;
            }

            _currenViewModel = viewModel;

            if (viewModel != null)
            {
                viewModel.RequestUpdate += () => UpdateImpl(viewModel);
                viewModel.CanBeVisibleChanged += HandleVisibility;
            }

            UpdateImpl(viewModel);
        }

        private async void UpdateImpl(StandardAdItemViewModel viewModel)
        {
            (Content as MyTargetControl)?.Dispose();

            if (viewModel == null)
            {
                return;
            }

            var myTargetControl = new MyTargetControl(viewModel.SlotId, adSize: viewModel.AdSize);

            Content = (await myTargetControl.LoadAsync()).IsLoaded ? myTargetControl : null;

            HandleVisibility(viewModel.CanBeVisible);
        }
    }
}