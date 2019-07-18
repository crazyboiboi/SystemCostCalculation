using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SystemCostCalculation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ItemModel> items = new List<ItemModel>();
        List<SupplierModel> suppliers = new List<SupplierModel>();
        public MainWindow()
        {
            InitializeComponent();

            LoadItemList();
            LoadSupplierList();
        }

        private void LoadItemList()
        {
            items = SqliteDataAccess.LoadItems();
        }

        private void LoadSupplierList()
        {
            suppliers = SqliteDataAccess.LoadSuppliers();
        }
    }
}
