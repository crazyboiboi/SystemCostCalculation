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
        private CreateTemplateViewModel createTemplateViewModel;
        private ViewTemplateViewModel viewTemplateViewModel;


        public MainWindowViewModel()
        {
            manageItemViewModel = new ManageItemViewModel();
            manageSupplierViewModel = new ManageSupplierViewModel();
            createTemplateViewModel = new CreateTemplateViewModel();
            viewTemplateViewModel = new ViewTemplateViewModel();
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

        private RelayCommand createTemplateTabCommand; 
        public RelayCommand CreateTemplateTabCommand
        {
            get
            {
                if(createTemplateTabCommand == null)
                {
                    createTemplateTabCommand = new RelayCommand(() =>
                   {
                       setView("createtemplate");
                   });
                }
                return createTemplateTabCommand;
            }
        }

        private RelayCommand viewTemplateTabCommand; 
        public RelayCommand ViewTemplateTabCommand
        {
            get
            {
                if(viewTemplateTabCommand == null)
                {
                    viewTemplateTabCommand = new RelayCommand(() =>
                   {
                       setView("viewtemplate");
                   });
                }
                return viewTemplateTabCommand;
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
            } else if (name.Equals("createtemplate"))
            {
                currentViewModel = createTemplateViewModel;
            } else if (name.Equals("viewtemplate"))
            {
                currentViewModel = viewTemplateViewModel;
            }
        }


    }
}
