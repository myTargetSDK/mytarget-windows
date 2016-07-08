using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using Mycom.TargetDemoApp.Helpers;
using Mycom.TargetDemoApp.Resources;
using Mycom.TargetDemoApp.Services;
using Mycom.TargetDemoApp.Views;

namespace Mycom.TargetDemoApp.ViewModels
{
    using CustomPropertyFactory = CustomPropertyFactory<StartPageViewModel>;

    internal sealed class StartPageViewModel : ICustomPropertyProvider
    {
        private const String Title = "myTarget Demo";

        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory.CreateDictionary(CustomPropertyFactory.Create(nameof(Pages),
                                                                                o => o.Pages,
                                                                                (o, i) => o.Pages[(Int32) i]),
                                                   CustomPropertyFactory.Create(nameof(Title), o => Title));


        private readonly Frame _frame;
        private readonly IPropertySet _localSettings = ApplicationData.Current.LocalSettings.Values;
        private readonly ObservableCollection<Object> Pages;

        internal StartPageViewModel(Frame frame)
        {
            _frame = frame;

            var items = new ObservableCollection<Object>
                        {
                            new DefaultItemViewModel(AdvertisementType.Standard,
                                                     Brushes.BrushFF3F51B5,
                                                     "Banners 320x50",
                                                     "Standard 320x50 banners",
                                                     new BitmapImage(new Uri("ms-appx:///Resources/StandardAd.png"))),
                            new DefaultItemViewModel(AdvertisementType.Interstitial,
                                                     Brushes.BrushFF009688,
                                                     "Interstitial Ads",
                                                     "Fullscreen banners",
                                                     new BitmapImage(new Uri("ms-appx:///Resources/InterstitialAd.png"))),
                            new DefaultItemViewModel(AdvertisementType.Native,
                                                     Brushes.BrushFFF44336,
                                                     "Native Ads",
                                                     "Advertisement inside app's content",
                                                     new BitmapImage(new Uri("ms-appx:///Resources/NativeAd.png"))),
                            new DefaultItemViewModel(AdvertisementType.Empty,
                                                     Brushes.Brush00000000,
                                                     "Ad unit",
                                                     "Insert your slot id and advertisement type",
                                                     new BitmapImage(new Uri("ms-appx:///Resources/Plus.png")))
                        };

            Pages = items;

            foreach (var pair in _localSettings)
            {
                try
                {
                    AddItemSlot((AdvertisementType) Enum.Parse(typeof (AdvertisementType), pair.Value.ToString()),
                                UInt32.Parse(pair.Key));
                }
                catch { }
            }
        }

        public void OnItemClicked(Object clickedItem)
        {
            if (clickedItem is DefaultItemViewModel)
            {
                switch (((DefaultItemViewModel) clickedItem).AdvertisementType)
                {
                    case AdvertisementType.Standard:
                        _frame.Navigate(typeof (StandardAdPage));
                        break;
                    case AdvertisementType.Native:
                        _frame.Navigate(typeof (NativeAdPage));
                        break;
                    case AdvertisementType.Interstitial:
                        _frame.Navigate(typeof (InterstitialAdPage));
                        break;
                    case AdvertisementType.Empty:
                        var addCustomSlotViewModel = new AddCustomSlotViewModel();
                        DialogService.Show(addCustomSlotViewModel);
                        addCustomSlotViewModel.ConfirmRequest += (u, type) =>
                                                                 {
                                                                     if (_localSettings.ContainsKey(u.ToString()))
                                                                     {
                                                                         var content = new TextBlock
                                                                                       {
                                                                                           Text = "Error: item with the same slot exists",
                                                                                           Foreground = Brushes.BrushFFF44336,
                                                                                           HorizontalAlignment = HorizontalAlignment.Center,
                                                                                           FontSize = 16,
                                                                                           TextWrapping = TextWrapping.WrapWholeWords,
                                                                                           FontWeight = FontWeights.SemiBold
                                                                                       };

                                                                         var flyout = new CustomFlyout(content)
                                                                                      {
                                                                                          Placement = FlyoutPlacementMode.Full
                                                                                      };

                                                                         flyout.ShowAt((FrameworkElement) Window.Current.Content);

                                                                         Task.Delay(3000).ContinueWith(task => flyout.Hide(), TaskScheduler.FromCurrentSynchronizationContext());

                                                                         return false;
                                                                     }

                                                                     _localSettings[u.ToString()] = type.ToString();

                                                                     AddItemSlot(type, u);

                                                                     return true;
                                                                 };
                        break;
                }
            }
            else if (clickedItem is CustomItemViewModel)
            {
                var customItemViewModel = (CustomItemViewModel) clickedItem;
                switch (customItemViewModel.AdvertisementType)
                {
                    case AdvertisementType.Standard:
                        _frame.Navigate(typeof (StandardAdPage), customItemViewModel.SlotId);
                        break;
                    case AdvertisementType.Native:
                        _frame.Navigate(typeof (NativeAdPage), customItemViewModel.SlotId);
                        break;
                }
            }
            else if (clickedItem is InterstitialCustomItemViewModel)
            {
                ((InterstitialCustomItemViewModel) clickedItem).Show();
            }
        }

        private void AddItemSlot(AdvertisementType type, UInt32 slotId)
        {
            IRemoveNotify removeNotify;

            switch (type)
            {
                case AdvertisementType.Standard:
                case AdvertisementType.Native:
                    removeNotify = new CustomItemViewModel(type, slotId);
                    break;
                case AdvertisementType.Interstitial:
                    removeNotify = new InterstitialCustomItemViewModel(slotId);
                    break;
                default:
                    return;
            }

            Pages.Insert(Pages.Count - 1, removeNotify);
            removeNotify.RemoveRequest += OnRemoveRequest;
        }

        private void OnRemoveRequest(IRemoveNotify removeNotify)
        {
            Pages.Remove(removeNotify);

            _localSettings.Remove(removeNotify.GetSlotId().ToString());
        }

        public ICustomProperty GetCustomProperty(String name) => CustomProperties[name];

        public ICustomProperty GetIndexedProperty(String name, Type type) => CustomProperties[name];

        public String GetStringRepresentation() => ToString();

        public Type Type => GetType();
    }
}