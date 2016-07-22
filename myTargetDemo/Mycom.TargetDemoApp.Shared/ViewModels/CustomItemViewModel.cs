using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;
using Mycom.TargetDemoApp.Helpers;

namespace Mycom.TargetDemoApp.ViewModels
{
    using CustomPropertyFactory = CustomPropertyFactory<CustomItemViewModel>;

    internal class CustomItemViewModel : BaseItemViewModel, ICustomPropertyProvider, IRemoveNotify
    {
        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory<DefaultItemViewModel>.CreateDictionary(CustomPropertyFactory.Create(nameof(SlotId), o => o.SlotId),
                                                                         CustomPropertyFactory.Create(nameof(AdvertisementType), o => o.AdvertisementType));

        public readonly UInt32 SlotId;

        public CustomItemViewModel(AdvertisementType advertisementType,
                                   UInt32 slotId)
            : base(advertisementType)
        {
            SlotId = slotId;
        }

        public ICustomProperty GetCustomProperty(String name) => CustomProperties[name];

        public ICustomProperty GetIndexedProperty(String name, Type type) => null;

        public String GetStringRepresentation() => ToString();

        public Type Type => GetType();

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