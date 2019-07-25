using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCostCalculation
{
    public class SqliteDataAccess
    {

        public static List<ItemModel> LoadItems()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ItemModel>("select * from Item", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<SupplierModel> LoadSuppliers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<SupplierModel>("select * from Supplier", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveItem(ItemModel item)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Item (ID, Name, Size, Type, Price) values (@ID, @Name, @Size, @Type, @Price)", item);
            }
        }

        public static void SaveSupplier(SupplierModel supplier)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Supplier (ID, Name) values (@ID, @Name)", supplier);
            }
        }

        public static void UpdateItem(ItemModel item)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Item set Name = @Name, Size = @Size, Type = @Type, Price = @Price where ID = @ID", item);
            }
        }

        public static void UpdateSupplier(SupplierModel supplier)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Supplier set Name = @Name where ID = @ID", supplier);
            }
        }

        public static void DeleteItem(ItemModel item)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("delete from Item where ID = @ID", item);
            }
        }
        public static void DeleteSupplier(SupplierModel supplier)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("delete from Item where ID = @ID", supplier);
            }
        }

        private static string LoadConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

    }
}
