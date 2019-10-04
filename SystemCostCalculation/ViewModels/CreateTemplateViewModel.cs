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
    public class CreateTemplateViewModel : ViewModelBase
    {
        #region Fields

        public ObservableCollection<ItemModel> TemplateItems { get; set; }

        public ObservableCollection<SupplierModel> Suppliers { get; set; }
        
        public ObservableCollection<ItemModel> SupplierItems { get; set; }

        private ObservableCollection<ItemModel> allItems { get; set; }

        public DateTime CurrentDate { get; }

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

        private string _systemName;
        public string systemName
        {
            get
            {
                return _systemName;
            }
            set
            {
                Set(ref _systemName, value);
                if (value != null)
                {
                    CreateTemplateCommand.RaiseCanExecuteChanged();
                    SaveTemplateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _tenderName;
        public string tenderName
        {
            get
            {
                return _tenderName;
            }
            set
            {
                Set(ref _tenderName, value);
                if (value != null)
                {
                    CreateTemplateCommand.RaiseCanExecuteChanged();
                    SaveTemplateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _location;
        public string location
        {
            get
            {
                return _location;
            }
            set
            {
                Set(ref _location, value);
                if (value != null)
                {
                    CreateTemplateCommand.RaiseCanExecuteChanged();
                    SaveTemplateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _templateCode;
        public string templateCode
        {
            get
            {
                return _templateCode;
            }
            set
            {
                Set(ref _templateCode, value);
                if (value != null)
                {
                    CreateTemplateCommand.RaiseCanExecuteChanged();
                    SaveTemplateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _templateRemark;
        public string templateRemark
        {
            get
            {
                return _templateRemark;
            }
            set
            {
                Set(ref _templateRemark, value);
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
                    },
                    () => !string.IsNullOrEmpty(systemName) && !string.IsNullOrEmpty(tenderName) &&!string.IsNullOrEmpty(templateCode));
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
                    () => !string.IsNullOrEmpty(systemName) && !string.IsNullOrEmpty(tenderName) && !string.IsNullOrEmpty(templateCode));
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
            ResetFields();
        }

        private void SaveTemplate()
        {            
            //Once save button is clicked, use a variable that stores the information
            TemplateModel templateToSave = new TemplateModel
            {
                SystemName = systemName,
                TemplateCode = templateCode,
                TenderName = tenderName,
                DateModified = CurrentDate,
                Location = location,
                Remark = templateRemark,
                SystemItems = TemplateItems.ToList()
            };

            //If template is newly created, generate it a name
            if(templateToSave.TemplateSaveName == null)
            {
                templateToSave.TemplateSaveName = TemplateSaveAndLoad.generateTemplateSaveFilePath();
                templateToSave.DateCreated = CurrentDate;
            }

            //Create a new data entry for the template
            Constants.AddTemplate(templateToSave);


            //Save the entire template
            TemplateSaveAndLoad.save(templateToSave);            
        }

        private void DeleteTemplate()
        {
            //TO-DO: Deletes template if it has been selected from view templates tab
            TemplateSaveAndLoad.saveTemplateData();

            ResetFields();
        }

        #endregion

        #region Helper Methods

        public void ResetFields()
        {
            selectedItem = null;
            selectedSupplier = null;
            selectedTemplateItem = null;
            SupplierItems.Clear();
            TemplateItems.Clear();
            systemName = "";
            tenderName = "";
            location = "";
            templateCode = "";
            templateRemark = "";
        }

        public void PopulateTemplate ()
        {
            //To-Do: Write in other property too
            TemplateModel t = Constants.currentTemplate;
            systemName = t.SystemName;
            tenderName = t.TenderName;
            location = t.Location;
            templateCode = t.TemplateCode;
            templateRemark = t.Remark;

            Constants.currentTemplate = null;
        }


        #endregion

        #region Default Constructor

        public CreateTemplateViewModel()
        {
            Suppliers = Constants.suppliers;
            allItems = Constants.items;
            SupplierItems = new ObservableCollection<ItemModel>();
            TemplateItems = new ObservableCollection<ItemModel>();
            CurrentDate = DateTime.Now;
        }

        #endregion

    }
}
