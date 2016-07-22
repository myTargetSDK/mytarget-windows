using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Mycom.Target.Ads;
using Mycom.TargetDemoApp.ViewModels;

namespace Mycom.TargetDemoApp.Views
{
    internal sealed partial class StandardAdPage
    {
        private static readonly DependencyProperty SlotIdProperty = DependencyProperty.Register("SlotId",
                                                                                                typeof (Object),
                                                                                                typeof (StandardAdPage),
                                                                                                new PropertyMetadata(default(Object), OnSlotIdChanged));

        private static readonly Binding SlotIdBinding = new Binding
                                                        {
                                                            Path = new PropertyPath(nameof(StandardAdPageViewModel.SlotId)),
                                                            Mode = BindingMode.OneTime
                                                        };

        private static void OnSlotIdChanged(DependencyObject dependencyObject,
                                            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((StandardAdPage) dependencyObject).OnSlotIdChangedImpl(dependencyPropertyChangedEventArgs.NewValue as Int32?);
        }

        public StandardAdPage()
        {
            InitializeComponent();

            BindingOperations.SetBinding(this,
                                         SlotIdProperty,
                                         SlotIdBinding);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (DataContext == null)
            {
                DataContext = e.Parameter == null ? new StandardAdPageViewModel() : new StandardAdPageViewModel(Int32.Parse(e.Parameter.ToString()));
            }
        }

        private async void OnSlotIdChangedImpl(Int32? newValue)
        {
            if (!newValue.HasValue)
            {
                return;
            }

            var standardAd = new MyTargetControl(newValue.Value);

            var adLoadingResult = await standardAd.LoadAsync();

            if (!adLoadingResult.IsLoaded)
            {
                return;
            }

            standardAd.Start();

            AdPlaceholder.Content = standardAd;
        }

        private void OnUpdateTapped(Object sender, TappedRoutedEventArgs e)
        {
            OnSlotIdChangedImpl((DataContext as StandardAdPageViewModel)?.SlotId);
        }
    }

    internal sealed class StandardAdTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NullTemplate { get; set; }

        public DataTemplate ObjectTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(Object item)
        {
            return item == null ? NullTemplate : ObjectTemplate;
        }

        protected override DataTemplate SelectTemplateCore(Object item, DependencyObject container)
        {
            return item == null ? NullTemplate : ObjectTemplate;
        }
    }
}