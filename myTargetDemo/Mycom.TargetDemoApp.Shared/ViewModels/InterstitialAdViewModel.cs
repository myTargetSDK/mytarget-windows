using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Mycom.Target.Ads;
using Mycom.TargetDemoApp.Helpers;

namespace Mycom.TargetDemoApp.ViewModels
{
    using CustomPropertyFactory = CustomPropertyFactory<InterstitialAdViewModel>;

    internal class InterstitialAdViewModel : ICustomPropertyProvider, INotifyPropertyChanged
    {
        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory.CreateDictionary(CustomPropertyFactory.Create(nameof(Description), o => o.Description),
                                                   CustomPropertyFactory.Create(nameof(Title), o => o.Title),
                                                   CustomPropertyFactory.Create(nameof(ImageSource), o => o.ImageSource),
                                                   CustomPropertyFactory.Create(nameof(Background), o => o.Background),
                                                   CustomPropertyFactory.Create(nameof(IsLoading), o => o.IsLoading));

        private static readonly BitmapImage ErrorImageSource = new BitmapImage(new Uri("ms-appx:///Resources/Error.png"));
        private static readonly PropertyChangedEventArgs ImageSourcePropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(ImageSource));
        private static readonly PropertyChangedEventArgs IsLoadingPropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(IsLoading));

        protected readonly UInt32 SlotId;
        private readonly ImageSource _defaultImageSource;
        private readonly Brush Background;
        private readonly String Description;
        private readonly String Title;

        private ImageSource _imageSource;
        private InterstitialAd _interstitialAd;
        private Boolean _isLoading;

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

        private ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                if (_imageSource == value)
                {
                    return;
                }

                _imageSource = value;
                PropertyChanged?.Invoke(this, ImageSourcePropertyChangedEventArgs);
            }
        }

        public InterstitialAdViewModel(UInt32 slotId,
                                       Brush background,
                                       String title,
                                       String description,
                                       ImageSource imageSource)
        {
            _defaultImageSource = imageSource;

            SlotId = slotId;

            Background = background;
            Title = title;
            Description = description;
            ImageSource = _defaultImageSource;

            Update();
        }

        public void Show()
        {
            if (!IsLoading)
            {
                _interstitialAd?.Show();
            }
        }

        public void ShowDialog()
        {
            if (!IsLoading)
            {
                _interstitialAd?.ShowDialog();
            }
        }

        public async void Update()
        {
            if (IsLoading)
            {
                return;
            }

            IsLoading = true;

            _interstitialAd = new InterstitialAd((Int32) SlotId);

            var adLoadingResult = await _interstitialAd.LoadAsync();

            IsLoading = false;

            ImageSource = adLoadingResult.IsLoaded ? _defaultImageSource : ErrorImageSource;
        }

        public ICustomProperty GetCustomProperty(String name) => CustomProperties[name];

        public ICustomProperty GetIndexedProperty(String name, Type type) => CustomProperties[name];

        public String GetStringRepresentation() => ToString();

        public Type Type => GetType();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}