using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Mycom.TargetDemoApp.Helpers;

namespace Mycom.TargetDemoApp.ViewModels
{
    using CustomPropertyFactory = CustomPropertyFactory<DefaultItemViewModel>;

    internal sealed class DefaultItemViewModel : BaseItemViewModel, ICustomPropertyProvider
    {
        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory.CreateDictionary(CustomPropertyFactory.Create(nameof(Description), o => o.Description),
                                                   CustomPropertyFactory.Create(nameof(Title), o => o.Title),
                                                   CustomPropertyFactory.Create(nameof(ImageSource), o => o.ImageSource),
                                                   CustomPropertyFactory.Create(nameof(Background), o => o.Background),
                                                   CustomPropertyFactory.Create(nameof(AdvertisementType), o => o.AdvertisementType));

        internal readonly Brush Background;
        internal readonly String Description;
        internal readonly ImageSource ImageSource;
        internal readonly String Title;

        public DefaultItemViewModel(AdvertisementType advertisementType,
                                    Brush background,
                                    String title,
                                    String description,
                                    ImageSource imageSource)
            : base(advertisementType)
        {
            Background = background;
            Title = title;
            Description = description;
            ImageSource = imageSource;
        }

        public ICustomProperty GetCustomProperty(String name) => CustomProperties[name];

        public ICustomProperty GetIndexedProperty(String name, Type type) => null;

        public String GetStringRepresentation() => ToString();

        public Type Type => GetType();
    }
}