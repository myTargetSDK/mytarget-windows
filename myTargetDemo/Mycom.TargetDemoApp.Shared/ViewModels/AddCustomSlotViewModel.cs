using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Mycom.TargetDemoApp.Helpers;
using Mycom.TargetDemoApp.Resources;

namespace Mycom.TargetDemoApp.ViewModels
{
    using CustomPropertyFactory = CustomPropertyFactory<AddCustomSlotViewModel>;
    using static AdvertisementTypeHelper;

    internal sealed class AddCustomSlotViewModel : ICustomPropertyProvider, ICloseNotify
    {
        private const String CancelLabel = "CANCEL";
        private const String ConfirmLabel = "OK";
        private const String Header = "Ad unit. Insert your slot id and ad type.";
        private const String SlotHeader = "Slot id:";

        private const String TypeHeader = "Advertisement type:";

        private static readonly IReadOnlyList<String> AdvertisementTypes;
        private static readonly Brush ConfirmCancelBrush;

        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory.CreateDictionary(CustomPropertyFactory.Create(nameof(Header), o => Header),
                                                   CustomPropertyFactory.Create(nameof(SlotHeader), o => SlotHeader),
                                                   CustomPropertyFactory.Create(nameof(TypeHeader), o => TypeHeader),
                                                   CustomPropertyFactory.Create(nameof(AdvertisementTypes), o => AdvertisementTypes),
                                                   CustomPropertyFactory.Create(nameof(SelectedAdvertisementType),
                                                                                o => o.SelectedAdvertisementType,
                                                                                (o, s) =>
                                                                                {
                                                                                    if (o != null)
                                                                                    {
                                                                                        o.SelectedAdvertisementType = s;
                                                                                    }
                                                                                }),
                                                   CustomPropertyFactory.Create(nameof(SlotId),
                                                                                o => o.SlotId,
                                                                                (o, u) =>
                                                                                {
                                                                                    if (o != null)
                                                                                    {
                                                                                        o.SlotId = u;
                                                                                    }
                                                                                }),
                                                   CustomPropertyFactory.Create(nameof(CancelLabel), o => CancelLabel),
                                                   CustomPropertyFactory.Create(nameof(ConfirmLabel), o => ConfirmLabel),
                                                   CustomPropertyFactory.Create(nameof(ConfirmCancelBrush), o => ConfirmCancelBrush));

        static AddCustomSlotViewModel()
        {
            AdvertisementTypes = new List<String>
                                 {
                                     StandardAd,
                                     NativeAd,
                                     InterstitialAd
                                 };
            ConfirmCancelBrush = Brushes.BrushFF009688;
        }

        private String SelectedAdvertisementType { get; set; }

        private String SlotId { get; set; }

        public AddCustomSlotViewModel()
        {
            SelectedAdvertisementType = AdvertisementTypes.FirstOrDefault();
        }

        public void Confirm()
        {
            UInt32 slotId;
            if (!UInt32.TryParse(SlotId, out slotId))
            {
                return;
            }

            AdvertisementType type;
            switch (SelectedAdvertisementType)
            {
                case NativeAd:
                    type = AdvertisementType.Native;
                    break;
                case StandardAd:
                    type = AdvertisementType.Standard;
                    break;
                case InterstitialAd:
                    type = AdvertisementType.Interstitial;
                    break;
                default:
                    return;
            }

            if (ConfirmRequest?.Invoke(slotId, type) == true)
            {
                CloseRequest?.Invoke();
            }
        }

        public event Func<UInt32, AdvertisementType, Boolean> ConfirmRequest;

        public event Action CloseRequest;

        public void Close()
        {
            CloseRequest?.Invoke();
        }

        public ICustomProperty GetCustomProperty(String name) => CustomProperties[name];

        public ICustomProperty GetIndexedProperty(String name, Type type) => CustomProperties[name];

        public String GetStringRepresentation() => ToString();

        public Type Type => GetType();
    }
}