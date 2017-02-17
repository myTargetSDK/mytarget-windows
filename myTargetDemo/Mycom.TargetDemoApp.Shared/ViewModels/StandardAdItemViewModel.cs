using System;
using Mycom.Target.Ads;

namespace Mycom.TargetDemoApp.ViewModels
{
    internal sealed class StandardAdItemViewModel
    {
        public readonly MyTargetControl.AdSize AdSize;
        public readonly Int32 SlotId;

        private Boolean _canBeVisible;
        public Boolean CanBeVisible
        {
            get { return _canBeVisible; }
            set
            {
                _canBeVisible = value;
                CanBeVisibleChanged?.Invoke(value);
            }
        }

        public StandardAdItemViewModel(Int32 slotId, MyTargetControl.AdSize adSize)
        {
            SlotId = slotId;
            AdSize = adSize;
        }

        public void RaiseRequestUpdate() => RequestUpdate?.Invoke();

        public event Action<Boolean> CanBeVisibleChanged;

        public event Action RequestUpdate;
    }
}