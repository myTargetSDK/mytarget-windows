using System;
using System.Collections.Generic;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using Mycom.TargetDemoApp.Helpers;
using Mycom.TargetDemoApp.Resources;

namespace Mycom.TargetDemoApp.ViewModels
{
    using CustomPropertyFactory = CustomPropertyFactory<InterstitialAdPageViewModel>;

    internal sealed class InterstitialAdPageViewModel : ICustomPropertyProvider
    {
        private const String DialogInsteadDescription = "Show as dialog";
        private const String Title = "Interstitial Ads";

        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory.CreateDictionary(CustomPropertyFactory.Create(nameof(Title), o => Title),
                                                   CustomPropertyFactory.Create(nameof(DialogInsteadDescription), o => DialogInsteadDescription),
                                                   CustomPropertyFactory.Create(nameof(Ads), o => o.Ads),
                                                   CustomPropertyFactory.Create(nameof(DialogInstead),
                                                                                o => o.DialogInstead,
                                                                                (o, value) =>
                                                                                {
                                                                                    var target = o;
                                                                                    if (target != null)
                                                                                    {
                                                                                        target.DialogInstead = value;
                                                                                    }
                                                                                }));

        private readonly IPropertySet _localSetting;
        private readonly IReadOnlyList<InterstitialAdViewModel> Ads;

        private Boolean DialogInstead
        {
            get { return ((Boolean?) _localSetting[nameof(DialogInstead)]).GetValueOrDefault(); }
            set { _localSetting[nameof(DialogInstead)] = value; }
        }

        public InterstitialAdPageViewModel()
        {
            Ads = new List<InterstitialAdViewModel>
                  {
                      new InterstitialAdViewModel(22095,
                                                  Brushes.BrushFF3F51B5,
                                                  "Promo",
                                                  "Fullscreen promo advertisement",
                                                  new BitmapImage(new Uri("ms-appx:///Resources/InterstitialPromoAd.png"))),
                      new InterstitialAdViewModel(22093,
                                                  Brushes.BrushFF009688,
                                                  "Promo video",
                                                  "Fullscreen advertisement with video element",
                                                  new BitmapImage(new Uri("ms-appx:///Resources/InterstitialPromoVideoAd.png"))),
                      new InterstitialAdViewModel(30297,
                                                  Brushes.BrushFFF44336,
                                                  "Image",
                                                  "Fullscreen image advertisement",
                                                  new BitmapImage(new Uri("ms-appx:///Resources/InterstitialImageAd.png"))),
                      new InterstitialAdViewModel(38839,
                                                  Brushes.BrushFF808080,
                                                  "Video",
                                                  "Video style fullscreen advertisement",
                                                  new BitmapImage(new Uri("ms-appx:///Resources/InterstitialAd_video.png")))
                  };
            _localSetting = ApplicationData.Current.LocalSettings.Values;
        }

        public void Show(Object o)
        {
            var viewModel = o as InterstitialAdViewModel;
            if (viewModel == null)
            {
                return;
            }

            if (DialogInstead)
            {
                viewModel.ShowDialog();
            }
            else
            {
                viewModel.Show();
            }
        }

        public void Update()
        {
            foreach (var ad in Ads)
            {
                ad.Update();
            }
        }

        public ICustomProperty GetCustomProperty(String name) => CustomProperties[name];

        public ICustomProperty GetIndexedProperty(String name, Type type) => CustomProperties[name];

        public String GetStringRepresentation() => ToString();

        public Type Type => GetType();
    }
}