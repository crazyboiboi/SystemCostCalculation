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
    public class ManageItemViewModel : ViewModelBase
    {
        #region Fields

        public int currentIdNumber = 0;

        public ObservableCollection<ItemModel> items { get; set; }
        public ObservableCollection<string> categories { get; set; }
        public ObservableCollection<int> sizes { get; set; }

        private int _id;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                Set(ref _id, value);
            }
        }

        private string _code;
        public string code
        {
            get
            {
                return _code;
            }
            set
            {
                Set(ref _code, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _name;
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                Set(ref _name, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _category;
        public string category
        {
            get
            {
                return _category;
            }
            set
            {
                Set(ref _category, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private int _size;
        public int size
        {
            get
            {
                return _size;
            }
            set
            {
                Set(ref _size, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _type;
        public string type
        {
            get
            {
                return _type;
            }
            set
            {
                Set(ref _type, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _description;
        public string description
        {
            get
            {
                return _description;
            }
            set
            {
                Set(ref _description, value);
                AddCommand.RaiseCanExecuteChanged();
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
                if (_selectedItem != null)
                {
                    PopulateItemDetails(value.Code, value.Name, value.Category, value.Size, value.Type, value.Description);
                }
                UpdateCommand.RaiseCanExecuteChanged();
                RemoveCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region UI Commands

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(() =>
                    {
                        AddItem();
                        code = "";
                        name = "";
                        category = "";
                        size = 0;
                        type = "";
                        description = "";
                    },
                    () => !string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(description)
                    );
                }
                return addCommand;
            }
        }

        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new RelayCommand(() =>
                    {
                        UpdateItem();
                    },
                    () => selectedItem != null);
                }
                return updateCommand;
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                if (removeCommand == null)
                {
                    removeCommand = new RelayCommand(() =>
                    {
                        RemoveItem();
                    },
                    () => selectedItem != null);
                }
                return removeCommand;
            }
        }

        #endregion

        #region Command Methods

        private void AddItem()
        {
            ItemModel item = new ItemModel() { Code = code, Name = name, Category = category, Size = size, Type = type, Description = description, ID = currentIdNumber++ };
            items.Add(item);
            SqliteDataAccess.SaveItem(item);
        }

        private void UpdateItem()
        {
            ItemModel updatedItem = new ItemModel() { Code = code, Name = name, Category = category, Size = size, Type = type, Description = description, ID = selectedItem.ID };
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == updatedItem.ID)
                {
                    items[i] = updatedItem;
                    SqliteDataAccess.UpdateItem(updatedItem);
                    break;
                }
            }
            selectedItem = null;
            code = "";
            name = "";
            category = "";
            size = 0;
            type = "";
            description = "";

        }

        private void RemoveItem()
        {
            SqliteDataAccess.DeleteItem(selectedItem);
            items.Remove(selectedItem);
        }

        #endregion

        #region Helper Methods

        private void PopulateItemDetails(string c, string n, string cat, int s, string t, string desc)
        {
            code = c;
            name = n;
            category = cat;
            size = s;
            type = t;
            description = desc;
        }

        #endregion

        #region Default Constructor

        public ManageItemViewModel()
        {
            List<ItemModel> sqlItems = SqliteDataAccess.LoadItems();
            items = new ObservableCollection<ItemModel>(sqlItems as List<ItemModel>);
        }

        #endregion
    }
}
