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
    public class ManageSupplierViewModel : ViewModelBase
    {
        #region Fields
        public int currentIdNumber;
        public ObservableCollection<SupplierModel> suppliers { get; set; }

        private ObservableCollection<ItemModel> allItems { get; set; }

        public ObservableCollection<ItemModel> filteredItems { get; set; }

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
                AddSupplierCommand.RaiseCanExecuteChanged();
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
                AddSupplierCommand.RaiseCanExecuteChanged();
            }
        }

        private string _contact;
        public string contact
        {
            get
            {
                return _contact;
            }
            set
            {
                Set(ref _contact, value);
                AddSupplierCommand.RaiseCanExecuteChanged();
            }
        }

        private string _address;
        public string address
        {
            get
            {
                return _address;
            }
            set
            {
                Set(ref _address, value);
                AddSupplierCommand.RaiseCanExecuteChanged();
            }
        }

        private string _otherDetails;
        public string otherDetails
        {
            get
            {
                return _otherDetails;
            }
            set
            {
                Set(ref _otherDetails, value);
            }
        }

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
                if (_selectedSupplier != null)
                {
                    PopulateSupplierDetails(value.Code, value.Name, value.Contact, value.Address, value.OtherDetails);
                    List<ItemModel> sqlItems = SqliteDataAccess.LoadItems();
                    allItems = new ObservableCollection<ItemModel>(sqlItems as List<ItemModel>);
                    if (filteredItems.Count > 0)
                    {
                        filteredItems.Clear();
                    }
                    foreach (ItemModel item in allItems)
                    {
                        if (item.SupplierID == value.ID)
                        {
                            filteredItems.Add(item);
                        }
                    }
                }
                UpdateSupplierCommand.RaiseCanExecuteChanged();
                DeleteSupplierCommand.RaiseCanExecuteChanged();
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
                    PopulateItemDetails(value.Code, value.Price);
                }
                RemoveItemCommand.RaiseCanExecuteChanged();
            }
        }

        private string itemCode;
        public string ItemCode
        {
            get
            {
                return itemCode;
            }
            set
            {
                Set(ref itemCode, value);
                AddItemCommand.RaiseCanExecuteChanged();
                EditItemCommand.RaiseCanExecuteChanged();
            }
        }

        private double itemPrice;
        public double ItemPrice
        {
            get
            {
                return itemPrice;
            }
            set
            {
                Set(ref itemPrice, value);
                AddItemCommand.RaiseCanExecuteChanged();
                EditItemCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region UI Commands

        private RelayCommand addSupplierCommand;
        public RelayCommand AddSupplierCommand
        {
            get
            {
                if(addSupplierCommand == null)
                {
                    addSupplierCommand = new RelayCommand(() =>
                   {
                       AddSupplier();
                   },
                   () => !string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(contact) && !string.IsNullOrEmpty(address) && selectedSupplier == null);
                }
                return addSupplierCommand;
            }
        }

        private RelayCommand updateSupplierCommand;
        public RelayCommand UpdateSupplierCommand
        {
            get
            {
                if (updateSupplierCommand == null)
                {
                    updateSupplierCommand = new RelayCommand(() =>
                    {
                        UpdateSupplier();
                    },
                    () => selectedSupplier != null);
                }
                return updateSupplierCommand;
            }
        }

        private RelayCommand deleteSupplierCommand;
        public RelayCommand DeleteSupplierCommand
        {
            get
            {
                if (deleteSupplierCommand == null)
                {
                    deleteSupplierCommand = new RelayCommand(() =>
                    {
                        DeleteSupplier();
                    },
                    () => selectedSupplier != null);
                }
                return deleteSupplierCommand;
            }
        }

        private RelayCommand addItemCommand;
        public RelayCommand AddItemCommand
        {
            get
            {
                if (addItemCommand == null)
                {
                    addItemCommand = new RelayCommand(() =>
                    {
                        AddItem();
                        ResetItemFields();
                    },
                    () => !string.IsNullOrEmpty(ItemCode) && !double.IsNaN(ItemPrice) && selectedSupplier != null);
                }
                return addItemCommand;
            }
        }

        private RelayCommand editItemCommand;
        public RelayCommand EditItemCommand
        {
            get
            {
                if (editItemCommand == null)
                {
                    editItemCommand = new RelayCommand(() =>
                    {
                        EditItem();
                        ResetItemFields();
                    },
                    () => !string.IsNullOrEmpty(ItemCode) && !double.IsNaN(ItemPrice) && selectedSupplier != null);
                }
                return editItemCommand;
            }
        }

        private RelayCommand removeItemCommand;
        public RelayCommand RemoveItemCommand
        {
            get
            {
                if (removeItemCommand == null)
                {
                    removeItemCommand = new RelayCommand(() =>
                    {
                        RemoveItem();
                    },
                    () => selectedItem != null);
                }
                return removeItemCommand;
            }
        }

        #endregion

        #region Command Methods

        private void AddSupplier()
        {
            SupplierModel supplier = new SupplierModel() {
                Code = code.Trim(),
                Name = name.Trim(),
                Address = address.Trim(),
                Contact = contact.Trim(),
                OtherDetails = otherDetails,
                ID = currentIdNumber++
            };

            suppliers.Add(supplier);
            SqliteDataAccess.SaveSupplier(supplier);
            ResetSupplierFields();
        }

        private void UpdateSupplier()
        {
            SupplierModel updatedSupplier = new SupplierModel {
                Code = code.Trim(),
                Name = name.Trim(),
                Address = address.Trim(),
                Contact = contact.Trim(),
                OtherDetails = otherDetails,
                ID = selectedSupplier.ID
            };

            for (int i = 0; i < suppliers.Count; i++)
            {
                if (suppliers[i].ID == updatedSupplier.ID)
                {
                    suppliers[i] = updatedSupplier;
                    SqliteDataAccess.UpdateSupplier(updatedSupplier);
                    break;
                }
            }
            ResetSupplierFields();
        }

        private void DeleteSupplier()
        {
            SqliteDataAccess.DeleteSupplier(selectedSupplier);
            suppliers.Remove(selectedSupplier);
            ResetSupplierFields();
        }

        private void AddItem()
        {
            ItemModel itemToBeAssigned = SqliteDataAccess.FindUnassignedItem(ItemCode);
            itemToBeAssigned.ID = currentIdNumber++;
            itemToBeAssigned.SupplierID = selectedSupplier.ID;
            itemToBeAssigned.Price = Math.Round(ItemPrice, 2);
            filteredItems.Add(itemToBeAssigned);
            SqliteDataAccess.SaveItem(itemToBeAssigned);
            
        }

        private void EditItem()
        {
            for (int i = 0; i < filteredItems.Count(); i++)
            {
                if (filteredItems[i].Code == ItemCode)
                {
                    double UpdatedPrice = Math.Round(ItemPrice, 2);
                    ItemModel updatedItem = new ItemModel {
                        ID = filteredItems[i].ID,
                        SupplierID = filteredItems[i].SupplierID,
                        Code = filteredItems[i].Code,
                        Name = filteredItems[i].Name,
                        Category = filteredItems[i].Category,
                        Size = filteredItems[i].Size,
                        Type = filteredItems[i].Type,
                        Description = filteredItems[i].Description,
                        Price = UpdatedPrice
                    };

                    filteredItems[i] = updatedItem;
                    SqliteDataAccess.UpdateItem(filteredItems[i]);
                    break;
                }
            }
        }

        private void RemoveItem()
        {
            SqliteDataAccess.DeleteItem(selectedItem);
            filteredItems.Remove(selectedItem);
            ItemCode = "";
            ItemPrice = 0;
        }

        #endregion

        #region Helper Methods

        private void PopulateSupplierDetails(string c, string n, string a, string con, string det)
        {
            code = c;
            name = n;
            address = a;
            contact = con;
            otherDetails = det;
        }

        private void PopulateItemDetails(string n, double p)
        {
            ItemCode = n;
            ItemPrice = p;
        }

        public void ResetSupplierFields()
        {
            selectedSupplier = null;
            code = "";
            name = "";
            contact = "";
            address = "";
            otherDetails = "";
        }

        public void ResetItemFields()
        {
            ItemCode = "";
            ItemPrice = 0.0d;
        }

        #endregion

        #region Default Constructor
        public ManageSupplierViewModel()
        {
            suppliers = Constants.suppliers;
            currentIdNumber = Constants.currentSupplierIDNumber;
            allItems = Constants.items;
            filteredItems = new ObservableCollection<ItemModel>();
        }
        #endregion
    }
}
