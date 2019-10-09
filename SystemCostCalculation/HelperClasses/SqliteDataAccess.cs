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
        #region Item Methods

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
        /// Returns a unassigned template item
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns>Single unassigned item</returns>
        public static ItemModel FindUnassignedItem(string ItemCode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Code", ItemCode);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ItemModel>("select * from Item where Code = @Code and SupplierID = -1 and Price = -1", parameters);
                return output.First();
            }
        }

        /// <summary>
        /// Returns all unassigned template items
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns>List of unassigned items</returns>
        public static List<ItemModel> FindUnassignedItems()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ItemModel>("select * from Item where SupplierID = -1 and Price = -1", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Returns an item that is assigned to a particular supplier
        /// </summary>
        /// <param name="ItemName"></param>
        /// <param name="SupplierID"></param>
        /// <returns>Single assigned item</returns>
        public static ItemModel FindAssignedItem(string ItemName, int SupplierID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", ItemName);
            parameters.Add("@SupplierID", SupplierID);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ItemModel>("select * from Item where Name = @Name and SupplierID = @SupplierID", parameters);
                return output.First();
            }
        }

        /// <summary>
        /// Returns a list of items that are assigned to a supplier
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns>List of ItemModels</returns>
        public static List<ItemModel> LoadFilteredItems(SupplierModel supplier)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", supplier.ID);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ItemModel>("select * from Item where SupplierID = @ID", parameters);
                return output.ToList();
            }
        }

        /// <summary>
        /// Inserts a new item into the Item table
        /// </summary>
        /// <param name="item"></param>
        public static void SaveItem(ItemModel item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SupplierID", item.SupplierID);
            parameters.Add("@Code", item.Code);
            parameters.Add("@Name", item.Name);
            parameters.Add("@Category", item.Category);
            parameters.Add("@Size", item.Size);
            parameters.Add("@Type", item.Type);
            parameters.Add("@Description", item.Description);
            parameters.Add("@Price", item.Price);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Item (SupplierID, Code, Name, Category, Size, Type, Description, Price) values (@SupplierID, @Code, @Name, @Category, @Size, @Type, @Description, @Price)", parameters);
            }
        }

        /// <summary>
        /// Updates an existing record in the Item table
        /// </summary>
        /// <param name="item"></param>
        public static void UpdateItem(ItemModel item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", item.ID);
            parameters.Add("@SupplierID", item.SupplierID);
            parameters.Add("@Code", item.Code);
            parameters.Add("@Name", item.Name);
            parameters.Add("@Category", item.Category);
            parameters.Add("@Size", item.Size);
            parameters.Add("@Type", item.Type);
            parameters.Add("@Description", item.Description);
            parameters.Add("@Price", item.Price);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Item set SupplierID = @SupplierID, Code = @Code, Name = @Name, Category = @Category, Size = @Size, Type = @Type, Description = @Description, Price = @Price where ID = @ID", parameters);
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

        #endregion

        #region Supplier Methods

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

        #endregion

        #region Category and Size Methods

        /// <summary>
        /// Loads all Categories from Category table
        /// </summary>
        /// <returns>List of string</returns>
        public static List<string> LoadCategories()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<string>("select * from Category", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Loads all sizes from Size table
        /// </summary>
        /// <returns>List of ints</returns>
        public static List<int> LoadSizes()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<int>("select * from Size", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Saves new category into database
        /// </summary>
        /// <param name="category"></param>
        public static void SaveCategory(string category)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Category", category);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Category (Name) values (@Category)", parameters);
            }
        }

        /// <summary>
        /// Saves new size into database
        /// </summary>
        /// <param name="size"></param>
        public static void SaveSize(int size)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Size", size);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Size (Size) values (@Size)", parameters);
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Returns the highest current item ID in the table so that IDs won't overlap
        /// </summary>
        /// <returns>Max Item ID</returns>
        public static int GetMaxItemID()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.ExecuteScalar<int>("select COUNT(*) from Item");
                return output;
            }
        }

        /// <summary>
        /// Returns highest current supplier ID so IDs won't overlap
        /// </summary>
        /// <returns>Max Supplier ID</returns>
        public static int GetMaxSupplierID()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.ExecuteScalar<int>("select MAX(ID) from Supplier");
                return output;
            }
        }

        #endregion

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
