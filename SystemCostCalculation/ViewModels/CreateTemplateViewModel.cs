using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCostCalculation.Models;

namespace SystemCostCalculation.ViewModels
{
    class CreateTemplateViewModel : ViewModelBase
    {

        #region Fields

        public ObservableCollection<ItemModel> TemplateItems { get; set; }

        public ObservableCollection<SupplierModel> Suppliers { get; set; }
        
        public ObservableCollection<ItemModel> SupplierItems { get; set; }

        private ObservableCollection<ItemModel> allItems { get; set; }

        private SupplierModel _selectedSupplier;
        public SupplierModel selectedSupplier
        {
            get
            {
                return _selectedSupplier;
            }
            set
            {
                Set(ref _selectedSupplier, value);
                if (value != null)
                {
                    List<ItemModel> sqlItems = SqliteDataAccess.LoadItems();
                    allItems = new ObservableCollection<ItemModel>(sqlItems as List<ItemModel>);
                    if (SupplierItems.Count() > 0)
                    {
                        SupplierItems.Clear();
                    }
                    foreach (ItemModel item in allItems)
                    {
                        if (item.SupplierID == value.ID)
                        {
                            SupplierItems.Add(item);
                        }
                    }
                }
            }
        }

        private ItemModel _selectedItem;
        public ItemModel selectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                Set(ref _selectedItem, value);
                AddItemCommand.RaiseCanExecuteChanged();
            }
        }

        private ItemModel _selectedTemplateItem;
        public ItemModel selectedTemplateItem
        {
            get
            {
                return _selectedTemplateItem;
            }
            set
            {
                Set(ref _selectedTemplateItem);
            }
        }

        #endregion

        #region UI Commands

        private RelayCommand addItemCommand;
        public RelayCommand AddItemCommand
        {
            get
            {
                if (addItemCommand == null)
                {
                    addItemCommand = new RelayCommand(() =>
                    {
                        AddItemToTemplate();
                    });
                }
                return addItemCommand;
            }
        }

        #endregion

        #region Command Methods

        private void AddItemToTemplate()
        {
            if (!TemplateItems.Contains(selectedItem))
            {
                selectedItem.Quantity = 0;
                TemplateItems.Add(selectedItem);
            }
        }

        #endregion

        #region Helper Methods



        #endregion

        #region Default Constructor

        public CreateTemplateViewModel()
        {
            List<SupplierModel> sqlSuppliers = SqliteDataAccess.LoadSuppliers();
            Suppliers = new ObservableCollection<SupplierModel>(sqlSuppliers as List<SupplierModel>);
            List<ItemModel> sqlItems = SqliteDataAccess.LoadItems();
            allItems = new ObservableCollection<ItemModel>(sqlItems as List<ItemModel>);
            SupplierItems = new ObservableCollection<ItemModel>();
            TemplateItems = new ObservableCollection<ItemModel>();
        }

        #endregion

    }
}
