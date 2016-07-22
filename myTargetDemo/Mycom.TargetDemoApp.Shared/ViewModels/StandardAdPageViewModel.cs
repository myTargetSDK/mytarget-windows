using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;
using Mycom.TargetDemoApp.Helpers;

namespace Mycom.TargetDemoApp.ViewModels
{
    using CustomPropertyFactory = CustomPropertyFactory<StandardAdPageViewModel>;

    internal sealed class StandardAdPageViewModel : ICustomPropertyProvider
    {
        private const String Title = "Banners 320x50";

        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory.CreateDictionary(CustomPropertyFactory.Create(nameof(Title), o => Title),
                                                   CustomPropertyFactory.Create(nameof(SlotId), o => o.SlotId),
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
                                                                                }));

        internal readonly Int32 SlotId;

        private readonly IReadOnlyList<LoremIpsumItemViewModel> DataList;

        public StandardAdPageViewModel(Int32 slotId = 30272)
        {
            SlotId = slotId;
            var dataList = new List<LoremIpsumItemViewModel>();
            for (var i = 0; i < 49; i++)
            {
                dataList.Add(new LoremIpsumItemViewModel());
                dataList.Add(null);
            }
            dataList.Add(new LoremIpsumItemViewModel());

            DataList = dataList;
        }

        public ICustomProperty GetCustomProperty(String name) => CustomProperties[name];

        public ICustomProperty GetIndexedProperty(String name, Type type) => CustomProperties[name];

        public String GetStringRepresentation() => ToString();

        public Type Type => GetType();
    }
}