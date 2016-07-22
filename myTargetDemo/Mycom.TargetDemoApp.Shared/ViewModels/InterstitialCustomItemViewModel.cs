using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI.Xaml.Data;
using Mycom.Target.Ads;
using Mycom.TargetDemoApp.Helpers;

namespace Mycom.TargetDemoApp.ViewModels
{
    using CustomPropertyFactory = CustomPropertyFactory<InterstitialCustomItemViewModel>;

    internal sealed class InterstitialCustomItemViewModel : BaseItemViewModel, ICustomPropertyProvider, INotifyPropertyChanged, IRemoveNotify
    {
        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory<DefaultItemViewModel>.CreateDictionary(CustomPropertyFactory.Create(nameof(SlotId), o => o.SlotId),
                                                                         CustomPropertyFactory.Create(nameof(IsLoading), o => o.IsLoading),
                                                                         CustomPropertyFactory.Create(nameof(IsLoadingFailed), o => o.IsLoadingFailed),
                                                                         CustomPropertyFactory.Create(nameof(AdvertisementType), o => o.AdvertisementType));

        private static readonly PropertyChangedEventArgs IsLoadingFailedPropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(IsLoadingFailed));
        private static readonly PropertyChangedEventArgs IsLoadingPropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(IsLoading));

        private readonly UInt32 SlotId;

        private InterstitialAd _interstitialAd;
        private Boolean _isLoading;
        private Boolean _isLoadingFailed;

        public Boolean IsLoading
        {
            get { return _isLoading; }
            private set
            {
                if (_isLoading == value)
                {
                    return;
                }

                _isLoading = value;
                PropertyChanged?.Invoke(this, IsLoadingPropertyChangedEventArgs);
            }
        }

        public Boolean IsLoadingFailed
        {
            get { return _isLoadingFailed; }
            set
            {
                if (_isLoadingFailed == value)
                {
                    return;
                }

                _isLoadingFailed = value;
                PropertyChanged?.Invoke(this, IsLoadingFailedPropertyChangedEventArgs);
            }
        }

        public InterstitialCustomItemViewModel(UInt32 slotId)
            : base(AdvertisementType.Interstitial)
        {
            SlotId = slotId;

            Update();
        }

        public void Show()
        {
            _interstitialAd?.Show();
        }

        public async void Update()
        {
            if (IsLoading)
            {
                return;
            }

            IsLoading = true;
            IsLoadingFailed = false;

            _interstitialAd = new InterstitialAd((Int32) SlotId);

            var adLoadingResult = await _interstitialAd.LoadAsync();

            IsLoading = false;

            IsLoadingFailed = !adLoadingResult.IsLoaded;
        }

        public ICustomProperty GetCustomProperty(String name) => CustomProperties[name];

        public ICustomProperty GetIndexedProperty(String name, Type type) => null;

        public String GetStringRepresentation() => ToString();

        public Type Type => GetType();

        public event PropertyChangedEventHandler PropertyChanged;

        public event Action<IRemoveNotify> RemoveRequest;

        public UInt32 GetSlotId()
        {
            return SlotId;
        }

        public void Remove()
        {
            RemoveRequest?.Invoke(this);
        }
    }
}