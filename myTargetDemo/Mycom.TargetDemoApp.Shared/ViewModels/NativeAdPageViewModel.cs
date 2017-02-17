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
        private const Int32 DefaultSlotId = 30296;
        private const String Title = "Native Ads";
        private const Int32 VideoSlotId = 30157;

        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory.CreateDictionary(CustomPropertyFactory.Create(nameof(Title), o => Title),
                                                   CustomPropertyFactory.Create(nameof(Items), o => o.Items),
                                                   CustomPropertyFactory.Create(nameof(SelectedItem), o => o.SelectedItem, (o, value) => o.SelectedItem = value));

        private readonly IReadOnlyList<NativeFeedViewModel> Items;

        private NativeFeedViewModel _selectedItem;

        private NativeFeedViewModel SelectedItem
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

        public NativeAdPageViewModel(Int32? slotId = null)
        {
            var defaultNativeAdSlotId = slotId.GetValueOrDefault(DefaultSlotId);
            var videoNativeAdSlotId = slotId.GetValueOrDefault(VideoSlotId);

            Items = new List<NativeFeedViewModel>
                    {
                        new NativeFeedViewModel(defaultNativeAdSlotId, "CONTENT STREAM", NativeAdViewType.ContentStream),
                        new NativeFeedViewModel(defaultNativeAdSlotId, "NEWS FEED", NativeAdViewType.NewsFeed),
                        new NativeFeedViewModel(defaultNativeAdSlotId, "CHAT LIST", NativeAdViewType.ChatList),
                        new NativeFeedViewModel(defaultNativeAdSlotId, "CONTENT WALL", NativeAdViewType.ContentWall),
                        new NativeFeedViewModel(videoNativeAdSlotId, "CONTENT STREAM VIDEO", NativeAdViewType.ContentStream),
                        new NativeFeedViewModel(videoNativeAdSlotId, "CONTENT WALL VIDEO", NativeAdViewType.ContentWall)
                    };

            SelectedItem = Items[0];
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