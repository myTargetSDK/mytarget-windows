using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml.Data;
using Mycom.Target.Ads;
using Mycom.TargetDemoApp.Helpers;

namespace Mycom.TargetDemoApp.ViewModels
{
    using CustomPropertyFactory = CustomPropertyFactory<StandardAdPageViewModel>;

    internal sealed class StandardAdPageViewModel : ICustomPropertyProvider, INotifyPropertyChanged
    {
        private const String Title = "Banners";

        private static readonly IReadOnlyDictionary<String, ICustomProperty> CustomProperties =
            CustomPropertyFactory.CreateDictionary(CustomPropertyFactory.Create(nameof(Title), o => Title),
                                                   CustomPropertyFactory.Create(nameof(SelectedItem), o => o.SelectedItem, (o, i) => o.SelectedItem = i),
                                                   CustomPropertyFactory.Create(nameof(Items), o => o.Items));

        private readonly IReadOnlyList<StandardFeedViewModel> Items;
        private StandardFeedViewModel _selectedItem;

        private StandardFeedViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value)
                {
                    return;
                }

                if (_selectedItem != null)
                {
                    var standardAdItemViewModel = _selectedItem.StandardAdItemViewModel;
                    if (standardAdItemViewModel != null)
                    {
                        standardAdItemViewModel.CanBeVisible = false;
                    }
                    foreach (var viewModel in _selectedItem.Items.OfType<StandardAdItemViewModel>())
                    {
                        viewModel.CanBeVisible = false;
                    }
                }

                _selectedItem = value;

                if (_selectedItem != null)
                {
                    var standardAdItemViewModel = _selectedItem.StandardAdItemViewModel;
                    if (standardAdItemViewModel != null)
                    {
                        standardAdItemViewModel.CanBeVisible = true;
                    }
                    foreach (var viewModel in _selectedItem.Items.OfType<StandardAdItemViewModel>())
                    {
                        viewModel.CanBeVisible = true;
                    }
                }

                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public StandardAdPageViewModel(Int32? slotId = null)
        {
            Items = new List<StandardFeedViewModel>
                    {
                        new StandardFeedViewModel("320x50", slotId ?? 30272, MyTargetControl.AdSize.Size320x50),
                        new StandardFeedViewModel("300x250", slotId ?? 64532, MyTargetControl.AdSize.Size300x250)
                    };

            SelectedItem = Items[0];
        }

        public void UpdateSelectedItem()
        {
            SelectedItem?.Update();
        }

        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICustomProperty GetCustomProperty(String name) => CustomProperties[name];

        public ICustomProperty GetIndexedProperty(String name, Type type) => CustomProperties[name];

        public String GetStringRepresentation() => ToString();

        public Type Type => GetType();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}