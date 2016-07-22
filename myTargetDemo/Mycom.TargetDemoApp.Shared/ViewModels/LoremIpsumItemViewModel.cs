using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;
using Mycom.TargetDemoApp.Helpers;

namespace Mycom.TargetDemoApp.ViewModels
{
    using CustomPropertyFactory = CustomPropertyFactory<LoremIpsumItemViewModel>;

    internal sealed class LoremIpsumItemViewModel : ICustomPropertyProvider
    {
        private const String Content =
            "Lorem ipsum dolor sit amet, error ceteros ex mea, possim equidem verterem cum no." +
            " Eum deleniti detraxit ea. Praesent inciderint at quo, at pro munere facete, libris delenit ei cum. Laoreet argumentum his et, mei ne eros paulo delicata." +
            " Porro soluta singulis cum ad, pro ad viderer complectitur. At cum illum veritus. Duo in sanctus splendide disputando, sed case tantas eligendi in.";

        private const String Title = "Lorem ipsum dolor sit amet";

        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory.CreateDictionary(CustomPropertyFactory.Create(nameof(Content), o => Content),
                                                   CustomPropertyFactory.Create(nameof(Title), o => Title));

        public ICustomProperty GetCustomProperty(String name) => CustomProperties[name];

        public ICustomProperty GetIndexedProperty(String name, Type type) => null;

        public String GetStringRepresentation() => ToString();

        public Type Type => GetType();
    }
}