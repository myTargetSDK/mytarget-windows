namespace Mycom.TargetDemoApp.ViewModels
{
    abstract class BaseItemViewModel
    {
        protected BaseItemViewModel(AdvertisementType advertisementType)
        {
            AdvertisementType = advertisementType;
        }

        public AdvertisementType AdvertisementType { get; }
    }
}