using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCostCalculation.ViewModels
{
    class ManageSupplierViewModel : ViewModelBase
    {

        public ManageSupplierViewModel()
        {
        }


        //This is for debugging purpose
        private RelayCommand debugCommand;
        public RelayCommand DebugCommand
        {
            get
            {
                if(debugCommand == null)
                {
                    debugCommand = new RelayCommand(() =>
                   {
                       Console.WriteLine("HELLO WORLD");
                   });
                }
                return debugCommand;
            }
        }


    }
}
