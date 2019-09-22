using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCostCalculation.Models;

namespace SystemCostCalculation.HelperClasses
{
    public class Constants
    {
        public static int currentSupplierIDNumber = SqliteDataAccess.GetMaxSupplierID();

        public static int currentItemIDNumber = SqliteDataAccess.GetMaxItemID();

        public static ObservableCollection<TemplateModel> templates = new ObservableCollection<TemplateModel>();

        public static ObservableCollection<SupplierModel> suppliers
        {
            get
            {
                List<SupplierModel> sqlSuppliers = SqliteDataAccess.LoadSuppliers();
                return new ObservableCollection<SupplierModel>(sqlSuppliers as List<SupplierModel>);
            }
        }

        public static ObservableCollection<ItemModel> items
        {
            get
            {
                List<ItemModel> sqlItems = SqliteDataAccess.LoadItems();
                return new ObservableCollection<ItemModel>(sqlItems as List<ItemModel>);
            }
        }

        public static ObservableCollection<ItemModel> unassignedItems
        {
            get
            {
                List<ItemModel> sqlUnassignedItems = SqliteDataAccess.FindUnassignedItems();
                return new ObservableCollection<ItemModel>(sqlUnassignedItems as List<ItemModel>);
            }
        }

        public static ObservableCollection<string> categories
        {
            get
            {
                List<string> sqlCategories = SqliteDataAccess.LoadCategories();
                return new ObservableCollection<string>(sqlCategories as List<string>);
            }
        }

        public static ObservableCollection<int> sizes
        {
            get
            {
                List<int> sqlSizes = SqliteDataAccess.LoadSizes();
                return new ObservableCollection<int>(sqlSizes as List<int>);
            }
        }

        public static void AddTemplate(TemplateModel template)
        {
            templates.Add(template);
            Console.WriteLine(templates.Count);
        }

    }

}
