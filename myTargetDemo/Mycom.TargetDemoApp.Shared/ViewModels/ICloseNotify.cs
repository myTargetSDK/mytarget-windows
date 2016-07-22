using System;

namespace Mycom.TargetDemoApp.ViewModels
{
    internal interface ICloseNotify
    {
        void Close();

        event Action CloseRequest;
    }
}