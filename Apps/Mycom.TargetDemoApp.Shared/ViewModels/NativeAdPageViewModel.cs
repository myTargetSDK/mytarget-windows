using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI.Xaml.Data;
using Mycom.TargetDemoApp.Helpers;

namespace Mycom.TargetDemoApp.ViewModels
{
    using CustomPropertyFactory = CustomPropertyFactory<NativeAdPageViewModel>;

    internal sealed class NativeAdPageViewModel : ICustomPropertyProvider, INotifyPropertyChanged
    {
        private const String Title = "Native Ads";

        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory.CreateDictionary(CustomPropertyFactory.Create(nameof(Title), o => Title),
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
                                                                                }),
                                                   CustomPropertyFactory.Create(nameof(SelectedItem),
                                                                                o => o.SelectedItem,
                                                                                (o, value) =>
                                                                                {
                                                                                    var viewModel = o;
                                                                                    if (viewModel != null)
                                                                                    {
                                                                                        viewModel.SelectedItem = value;
                                                                                    }
                                                                                }));

        private readonly IReadOnlyList<FeedViewModel> DataList;

        private FeedViewModel _selectedItem;

        private FeedViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value)
                {
                    return;
                }

                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        public NativeAdPageViewModel(Int32 slotId = 30296)
        {
            DataList = new List<FeedViewModel>
                       {
                           new FeedViewModel(slotId, "CONTENT STREAM", NativeAdViewType.ContentStream),
                           new FeedViewModel(slotId, "NEWS FEED", NativeAdViewType.NewsFeed),
                           new FeedViewModel(slotId, "CHAT LIST", NativeAdViewType.ChatList),
                           new FeedViewModel(slotId, "CONTENT WALL", NativeAdViewType.ContentWall)
                       };

            SelectedItem = DataList[0];
        }

        public void Update()
        {
            SelectedItem?.Update();
        }

        public ICustomProperty GetCustomProperty(String name) => CustomProperties[name];

        public ICustomProperty GetIndexedProperty(String name, Type type) => CustomProperties[name];

        public String GetStringRepresentation() => ToString();

        public Type Type => GetType();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}