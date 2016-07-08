using System;

namespace Mycom.TargetDemoApp.ViewModels
{
    internal interface IRemoveNotify
    {
        UInt32 GetSlotId();

        void Remove();

        event Action<IRemoveNotify> RemoveRequest;
    }
}