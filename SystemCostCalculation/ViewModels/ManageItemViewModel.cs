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
            }
        }

        private double _price;
        public double price
        {
            get
            {
                return _price;
            }
            set
            {
                Set(ref _price, value);
            }
        }

        private string _description;
        public string descripton
        {
            get
            {
                return _description;
            }
            set
            {
                Set(ref _description, value);
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
                        descripton = "";
                    },
                    () => !string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(descripton)
                    );
                }
                return addCommand;
            }
        }

        #endregion

        #region Command Methods

        private void AddItem()
        {
            ItemModel item = new ItemModel() { Code = code, Name = name, Category = category, Size = size, Type = type, }
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
            descripton = desc;
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
