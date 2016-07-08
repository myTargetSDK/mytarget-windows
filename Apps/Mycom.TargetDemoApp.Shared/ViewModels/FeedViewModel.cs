using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Mycom.TargetDemoApp.Helpers;
using Mycom.Target.NativeAds;

namespace Mycom.TargetDemoApp.ViewModels
{
    using CustomPropertyFactory = CustomPropertyFactory<FeedViewModel>;

    internal enum NativeAdViewType
    {
        ContentStream,
        NewsFeed,
        ContentWall,
        ChatList
    }

    internal sealed class FeedViewModel : ICustomPropertyProvider
    {
        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory.CreateDictionary(CustomPropertyFactory.Create(nameof(Title), o => o.Title),
                                                   CustomPropertyFactory.Create(nameof(DataList),
                                                                                o => o.DataList,
                                                                                (o, i) =>
                                                                                {
                                                                                    try
                                                                                    {
                                                                                        return o.DataList[Convert.ToInt32(i)];
                                                                                    }
                                                                                    catch (Exception)
                                                                                    {
                                                                                        return null;
                                                                                    }
                                                                                }));

        private static readonly LoremIpsumItemViewModel LoremIpsumItemViewModel = new LoremIpsumItemViewModel();

        private readonly ObservableCollection<Object> DataList;
        private readonly String Title;

        public FeedViewModel(Int32 slotId, String title, NativeAdViewType viewType)
        {
            Title = title;

            DataList = new ObservableCollection<Object>();
            for (var i = 0; i < 17; i++)
            {
                DataList.Add(LoremIpsumItemViewModel);
                DataList.Add(null);
            }
            DataList.Add(new LoremIpsumItemViewModel());

            for (var i = 0; i <= 2; i++)
            {
                var iTemp = i * 12 + 8;
                var nativeAd = new NativeAd(slotId) { AutoLoadImages = true };
                nativeAd.LoadAsync()
                        .ContinueWith(task =>
                                      {
                                          var result = task.Result;
                                          if (result.IsLoaded)
                                          {
                                              DataList.Insert(iTemp, null);
                                              DataList.Insert(iTemp, new NativeAdWrapperViewModel(nativeAd, viewType));
                                          }
                                      },
                                      TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        public void Update()
        {
            for (var i = 0; i < DataList.Count; i++)
            {
                var adWrapperViewModel = DataList[i] as NativeAdWrapperViewModel;
                if (adWrapperViewModel == null)
                {
                    continue;
                }

                var nativeAd = new NativeAd(30296) { AutoLoadImages = true };
                nativeAd.LoadAsync();
                DataList[i] = new NativeAdWrapperViewModel(nativeAd, adWrapperViewModel.DesiredViewType);
            }
        }

        public ICustomProperty GetCustomProperty(String name) => CustomProperties[name];

        public ICustomProperty GetIndexedProperty(String name, Type type) => CustomProperties[name];

        public String GetStringRepresentation() => ToString();

        public Type Type => GetType();
    }
}