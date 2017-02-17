using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;
using Mycom.Target.Ads;
using Mycom.TargetDemoApp.Helpers;

namespace Mycom.TargetDemoApp.ViewModels
{
    using CustomPropertyFactory = CustomPropertyFactory<StandardFeedViewModel>;

    internal sealed class StandardFeedViewModel : ICustomPropertyProvider
    {
        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory.CreateDictionary(CustomPropertyFactory.Create(nameof(Title), o => o.Title),
                                                   CustomPropertyFactory.Create(nameof(Items), o => o.Items),
                                                   CustomPropertyFactory.Create(nameof(StandardAdItemViewModel), o => o.StandardAdItemViewModel));

        private static readonly LoremIpsumItemViewModel LoremIpsumItemViewModel = new LoremIpsumItemViewModel();

        public readonly IReadOnlyList<Object> Items;
        public readonly StandardAdItemViewModel StandardAdItemViewModel;
        private readonly String Title;

        public StandardFeedViewModel(String title,
                                     Int32 slotId,
                                     MyTargetControl.AdSize adSize)
        {
            Title = title;

            StandardAdItemViewModel = adSize == MyTargetControl.AdSize.Size320x50
                                          ? new StandardAdItemViewModel(slotId, adSize)
                                          : null;

            var items = new List<Object>();
            for (var i = 0; i < 49; i++)
            {
                if (adSize == MyTargetControl.AdSize.Size300x250 && i == 2)
                {
                    items.Add(new StandardAdItemViewModel(slotId, adSize));
                }
                else
                {
                    items.Add(LoremIpsumItemViewModel);
                }

                items.Add(null);
            }
            items.Add(new LoremIpsumItemViewModel());

            Items = items;
        }

        public void Update()
        {
            StandardAdItemViewModel?.RaiseRequestUpdate();

            foreach (var viewModel in Items.OfType<StandardAdItemViewModel>())
            {
                viewModel.RaiseRequestUpdate();
            }
        }

        public ICustomProperty GetCustomProperty(String name) => CustomProperties[name];

        public ICustomProperty GetIndexedProperty(String name, Type type) => CustomProperties[name];

        public String GetStringRepresentation() => ToString();

        public Type Type => GetType();
    }
}