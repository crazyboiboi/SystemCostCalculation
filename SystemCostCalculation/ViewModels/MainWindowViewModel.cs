using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SystemCostCalculation.HelperClasses;
using SystemCostCalculation.Models;
using SystemCostCalculation.Views;

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
            Messenger.Default.Register<SwitchViewMessage>(this, (switchViewMessage) =>
            {
                setView(switchViewMessage.ViewName);
            });
            manageItemViewModel = new ManageItemViewModel();
            manageSupplierViewModel = new ManageSupplierViewModel();
            createTemplateViewModel = new CreateTemplateViewModel();
            viewTemplateViewModel = new ViewTemplateViewModel();

            TemplateSaveAndLoad.loadTemplateData();
        }


        private FrameworkElement _contentControlView;
        public FrameworkElement ContentControlView
        {
            get
            {
                return _contentControlView;
            }
            set
            {
                _contentControlView = value;
                RaisePropertyChanged("ContentControlView");
            }
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
                ContentControlView = new ManageItemView();
                ContentControlView.DataContext = manageItemViewModel;
            } else if (name.Equals("supplier"))
            {
                ContentControlView = new ManageSupplierView();
                ContentControlView.DataContext = manageSupplierViewModel;
            } else if (name.Equals("createtemplate"))
            {
                ContentControlView = new CreateTemplateView();
                ContentControlView.DataContext = createTemplateViewModel;
            } else if (name.Equals("viewtemplate"))
            {
                ContentControlView = new ViewTemplateView();
                ContentControlView.DataContext = viewTemplateViewModel;
            }
        }


    }
}
