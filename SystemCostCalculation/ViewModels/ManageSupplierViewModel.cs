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
    public class ManageSupplierViewModel : ViewModelBase
    {
        #region Fields
        public int currentIdNumber = 0;
        public ObservableCollection<SupplierModel> suppliers { get; set; }

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
                AddCommand.RaiseCanExecuteChanged();
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
                AddCommand.RaiseCanExecuteChanged();
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
                }
                UpdateCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Default Constructor
        public ManageSupplierViewModel()
        {
            List<SupplierModel> sqlSuppliers = SqliteDataAccess.LoadSuppliers();
            suppliers = new ObservableCollection<SupplierModel>(sqlSuppliers as List<SupplierModel>);
            
        }
        #endregion

        #region UI Commands

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                if(addCommand == null)
                {
                    addCommand = new RelayCommand(() =>
                   {
                       AddSupplier();
                       code = "";
                       name = "";
                       contact = "";
                       address = "";
                       otherDetails = "";
                   },
                   () => !string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(contact) && !string.IsNullOrEmpty(address));
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
                        UpdateSupplier();
                    },
                    () => selectedSupplier != null);
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

                    },
                    () => selectedSupplier != null);
                }
                return removeCommand;
            }
        }

        #endregion

        #region Command Methods

        private void AddSupplier()
        {
            SupplierModel supplier = new SupplierModel() { Code = code, Name = name, Address = address, Contact = contact, OtherDetails = otherDetails, ID = currentIdNumber++ };
            suppliers.Add(supplier);
            SqliteDataAccess.SaveSupplier(supplier);
        }

        private void UpdateSupplier()
        {
            SupplierModel updatedSupplier = new SupplierModel { Code = code, Name = name, Address = address, Contact = contact, OtherDetails = otherDetails, ID = selectedSupplier.ID };
            for (int i = 0; i < suppliers.Count; i++)
            {
                if (suppliers[i].ID == updatedSupplier.ID)
                {
                    suppliers[i] = updatedSupplier;
                    SqliteDataAccess.UpdateSupplier(updatedSupplier);
                    break;
                }
            }
            selectedSupplier = null;
            code = "";
            name = "";
            contact = "";
            address = "";
            otherDetails = "";
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

        #endregion
    }
}
