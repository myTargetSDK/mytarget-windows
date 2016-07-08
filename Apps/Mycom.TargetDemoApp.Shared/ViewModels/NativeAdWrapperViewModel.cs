using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;
using Mycom.TargetDemoApp.Helpers;
using Mycom.Target.NativeAds;

namespace Mycom.TargetDemoApp.ViewModels
{
    using CustomPropertyFactory = CustomPropertyFactory<NativeAdWrapperViewModel>;

    internal class NativeAdWrapperViewModel : ICustomPropertyProvider
    {
        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory.CreateDictionary(CustomPropertyFactory.Create(nameof(Ad), o => o.Ad));

        public readonly NativeAd Ad;

        public NativeAdViewType DesiredViewType { get; }

        public NativeAdWrapperViewModel(NativeAd ad,
                                        NativeAdViewType desiredViewType)
        {
            Ad = ad;
            DesiredViewType = desiredViewType;
        }

        public ICustomProperty GetCustomProperty(String name) => CustomProperties[name];

        public ICustomProperty GetIndexedProperty(String name, Type type) => CustomProperties[name];

        public String GetStringRepresentation() => ToString();

        public Type Type => GetType();
    }
}