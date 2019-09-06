using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCostCalculation.HelperClasses;
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

        public string CurrentDate { get; }

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
                if (value != null)
                {
                    AddItemCommand.RaiseCanExecuteChanged();
                }
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
                Set(ref _selectedTemplateItem, value);
                if (value != null)
                {
                    RemoveItemFromTemplateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string systemName;
        public string SystemName
        {
            get
            {
                return systemName;
            }
            set
            {
                Set(ref systemName, value);
                if (value != null)
                {
                    CreateTemplateCommand.RaiseCanExecuteChanged();
                    SaveTemplateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string tenderName;
        public string TenderName
        {
            get
            {
                return tenderName;
            }
            set
            {
                Set(ref tenderName, value);
                if (value != null)
                {
                    CreateTemplateCommand.RaiseCanExecuteChanged();
                    SaveTemplateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string location;
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                Set(ref location, value);
                if (value != null)
                {
                    CreateTemplateCommand.RaiseCanExecuteChanged();
                    SaveTemplateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string templateCode;
        public string TemplateCode
        {
            get
            {
                return templateCode;
            }
            set
            {
                Set(ref templateCode, value);
                if (value != null)
                {
                    CreateTemplateCommand.RaiseCanExecuteChanged();
                    SaveTemplateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string templateRemark;
        public string TemplateRemark
        {
            get
            {
                return templateRemark;
            }
            set
            {
                Set(ref templateRemark, value);
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
                    },
                    () => selectedItem != null);
                }
                return addItemCommand;
            }
        }

        private RelayCommand removeItemFromTemplateCommand;
        public RelayCommand RemoveItemFromTemplateCommand
        {
            get
            {
                if (removeItemFromTemplateCommand == null)
                {
                    removeItemFromTemplateCommand = new RelayCommand(() =>
                    {
                        RemoveItemFromTemplate();
                    },
                    () => selectedTemplateItem != null);
                }
                return removeItemFromTemplateCommand;
            }
        }

        private RelayCommand createTemplateCommand;
        public RelayCommand CreateTemplateCommand
        {
            get
            {
                if (createTemplateCommand == null)
                {
                    createTemplateCommand = new RelayCommand(() =>
                    {
                        CreateTemplate();
                    });
                }
                return createTemplateCommand;
            }
        }

        private RelayCommand saveTemplateCommand;
        public RelayCommand SaveTemplateCommand
        {
            get
            {
                if (saveTemplateCommand == null)
                {
                    saveTemplateCommand = new RelayCommand(() =>
                    {
                        SaveTemplate();
                    },
                    () => !string.IsNullOrEmpty(SystemName) && !string.IsNullOrEmpty(TenderName) && !string.IsNullOrEmpty(Location) && !string.IsNullOrEmpty(TemplateCode));
                }
                return saveTemplateCommand;
            }
        }

        private RelayCommand deleteTemplateCommand;
        public RelayCommand DeleteTemplateCommand
        {
            get
            {
                if (deleteTemplateCommand == null)
                {
                    deleteTemplateCommand = new RelayCommand(() =>
                    {
                        DeleteTemplate();
                    });
                }
                return deleteTemplateCommand;
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
            selectedItem = null;
        }

        private void RemoveItemFromTemplate()
        {
            if (selectedTemplateItem != null)
            {
                TemplateItems.Remove(selectedTemplateItem);
            }
        }

        private void CreateTemplate()
        {
            //TO-DO: Use helper class to save information and template items to a text file

            //Emptying text fields and datagrids
            selectedItem = null;
            selectedSupplier = null;
            selectedTemplateItem = null;
            SupplierItems.Clear();
            TemplateItems.Clear();
            SystemName = "";
            TenderName = "";
            Location = "";
            TemplateCode = "";
            TemplateRemark = "";
        }

        private void SaveTemplate()
        {
            //TO-DO: Updates template if it exists already

            TemplateModel templateToSave = new TemplateModel()
            {
                systemName = SystemName,
                templateCode = TemplateCode,
                tenderName = tenderName,
                //dateCreated = CurrentDate;
                location = Location,
                remark = TemplateRemark,
                //totalCost =
                //discount = 
                systemItems = TemplateItems.ToList()
            };
            Console.WriteLine(templateToSave.location);

            TemplateSaveAndLoad.save(templateToSave);
        }

        private void DeleteTemplate()
        {
            //TO-DO: Deletes template if it has been selected from view templates tab

            //Emptying text fields and datagrids
            selectedItem = null;
            selectedSupplier = null;
            selectedTemplateItem = null;
            SupplierItems.Clear();
            TemplateItems.Clear();
            SystemName = "";
            TenderName = "";
            Location = "";
            TemplateCode = "";
            TemplateRemark = "";
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
            CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
        }

        #endregion

    }
}
