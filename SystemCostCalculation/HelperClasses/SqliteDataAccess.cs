using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCostCalculation.Models;

namespace SystemCostCalculation
{
    /// <summary>
    /// Class which handles interaction between SQLite database and application
    /// </summary>
    public class SqliteDataAccess
    {
        
        /// <summary>
        /// Loads all records from Item table
        /// </summary>
        /// <returns>List of Item Models</returns>
        public static List<ItemModel> LoadItems()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ItemModel>("select * from Item", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Loads all records from Supplier table
        /// </summary>
        /// <returns>List of Supplier Models</returns>
        public static List<SupplierModel> LoadSuppliers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<SupplierModel>("select * from Supplier", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<ItemModel> LoadFilteredItems(SupplierModel supplier)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query("select ")
            }
        }

        /// <summary>
        /// Inserts a new item into the Item table
        /// </summary>
        /// <param name="item"></param>
        public static void SaveItem(ItemModel item)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Item (Code, Name, Category, Size, Type, Price, Description) values (@Code, @Name, @Category, @Size, @Type, @Price, @Description)", item);
            }
        }

        /// <summary>
        /// Inserts a new supplier into the Supplier table
        /// </summary>
        /// <param name="supplier"></param>
        public static void SaveSupplier(SupplierModel supplier)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Supplier (Code, Name, Contact, Address, OtherDetails) values (@Code, @Name, @Contact, @Address, @OtherDetails)", supplier);
            }
        }

        /// <summary>
        /// Updates an existing record in the Item table
        /// </summary>
        /// <param name="item"></param>
        public static void UpdateItem(ItemModel item)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Item set Code = @Code, Name = @Name, Category = @Category, Size = @Size, Type = @Type, Price = @Price, Description = @Description where ID = @ID", item);
            }
        }

        /// <summary>
        /// Updates an existing supplier in the Supplier table
        /// </summary>
        /// <param name="supplier"></param>
        public static void UpdateSupplier(SupplierModel supplier)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Supplier set Code = @Code, Name = @Name, Contact = @Contact, Address = @Address, OtherDetails = @OtherDetails where ID = @ID", supplier);
            }
        }

        /// <summary>
        /// Deletes an existing item from the Item table
        /// </summary>
        /// <param name="item"></param>
        public static void DeleteItem(ItemModel item)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                if (item != null)
                {
                    cnn.Execute("delete from Item where ID = @ID", item);
                }
            }
        }

        /// <summary>
        /// Deletes an existing supplier from the Supplier table
        /// </summary>
        /// <param name="supplier"></param>
        public static void DeleteSupplier(SupplierModel supplier)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                if (cnn.Query<SupplierModel>("select * from Supplier where ID = @ID", new DynamicParameters()) != null)
                {
                    cnn.Execute("delete from Supplier where ID = @ID", supplier);
                }
            }
        }

        /// <summary>
        /// Establishes connection between application and SQLite database
        /// </summary>
        /// <returns></returns>
        private static string LoadConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }
        
    }
}
