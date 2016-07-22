using System;

namespace Mycom.TargetDemoApp.ViewModels
{
    internal enum AdvertisementType
    {
        Standard,
        Native,
        Interstitial,
        Empty
    }

    internal static class AdvertisementTypeHelper
    {
        internal const String NativeAd = "Native Ad";
        internal const String StandardAd = "Standard Ad";
        internal const String InterstitialAd = "Interstitial Ad";

        internal static String GetString(AdvertisementType type)
        {
            switch (type)
            {
                case AdvertisementType.Standard:
                    return StandardAd;
                case AdvertisementType.Native:
                    return NativeAd;
                case AdvertisementType.Interstitial:
                    return InterstitialAd;
                default:
                    return null;
            }
        }
    }
}