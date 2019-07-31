using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCostCalculation.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private ManageItemViewModel manageItemViewModel;
        private ManageSupplierViewModel manageSupplierViewModel;


        public MainWindowViewModel()
        {
            manageItemViewModel = new ManageItemViewModel();
            manageSupplierViewModel = new ManageSupplierViewModel();
        }



        private object _currentViewModel;
        public object currentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel == value)
                    return;
                _currentViewModel = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand itemTabCommand; 
        public RelayCommand ItemTabCommand
        {
            get
            {
                if(itemTabCommand == null)
                {
                    itemTabCommand = new RelayCommand(() =>
                   {
                       setView("item");
                   });
                }
                return itemTabCommand;
            }
        }


        private RelayCommand supplierTabCommand; 
        public RelayCommand SupplierTabCommand
        {
            get
            {
                if(supplierTabCommand == null)
                {
                    supplierTabCommand = new RelayCommand(() =>
                   {
                       setView("supplier");
                   });
                }
                return supplierTabCommand;
            }
        }





        private void setView(string name)
        {
            if (name.Equals("item"))
            {
                currentViewModel = manageItemViewModel;
            } else if (name.Equals("supplier"))
            {
                currentViewModel = manageSupplierViewModel;
            }
        }


    }
}
