using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Mycom.Target.NativeAds;
using Mycom.TargetDemoApp.Helpers;

namespace Mycom.TargetDemoApp.ViewModels
{
    using CustomPropertyFactory = CustomPropertyFactory<NativeFeedViewModel>;

    internal enum NativeAdViewType
    {
        ContentStream,
        NewsFeed,
        ContentWall,
        ChatList
    }

    internal sealed class NativeFeedViewModel : ICustomPropertyProvider
    {
        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory.CreateDictionary(CustomPropertyFactory.Create(nameof(Title), o => o.Title),
                                                   CustomPropertyFactory.Create(nameof(Items), o => o.Items));

        private static readonly LoremIpsumItemViewModel LoremIpsumItemViewModel = new LoremIpsumItemViewModel();

        private readonly Int32 _slotId;
        private readonly ObservableCollection<Object> Items;
        private readonly String Title;

        public NativeFeedViewModel(Int32 slotId, String title, NativeAdViewType viewType)
        {
            _slotId = slotId;
            Title = title;

            Items = new ObservableCollection<Object>();
            for (var i = 0; i < 17; i++)
            {
                Items.Add(LoremIpsumItemViewModel);
                Items.Add(null);
            }
            Items.Add(new LoremIpsumItemViewModel());

            for (var i = 0; i <= 2; i++)
            {
                var iTemp = i * 12 + 8;
                var nativeAd = new NativeAd(_slotId) { AutoLoadImages = true };
                nativeAd.LoadAsync()
                        .ContinueWith(task =>
                                      {
                                          var result = task.Result;
                                          if (result.IsLoaded)
                                          {
                                              Items.Insert(iTemp, null);
                                              Items.Insert(iTemp, new NativeAdWrapperViewModel(nativeAd, viewType));
                                          }
                                      },
                                      TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        public void Update()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var adWrapperViewModel = Items[i] as NativeAdWrapperViewModel;
                if (adWrapperViewModel == null)
                {
                    continue;
                }

                var nativeAd = new NativeAd(_slotId) { AutoLoadImages = true };
                nativeAd.LoadAsync();
                Items[i] = new NativeAdWrapperViewModel(nativeAd, adWrapperViewModel.DesiredViewType);
            }
        }

        public ICustomProperty GetCustomProperty(String name) => CustomProperties[name];

        public ICustomProperty GetIndexedProperty(String name, Type type) => CustomProperties[name];

        public String GetStringRepresentation() => ToString();

        public Type Type => GetType();
    }
}