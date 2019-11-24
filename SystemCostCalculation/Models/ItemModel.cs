using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCostCalculation.Models
{
    /// <summary>
    /// Class which models the properties of an Item
    /// </summary>
    public class ItemModel
    {

        //Class properties
        public int ID { get; set; }

        public int SupplierID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public int Size { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public bool Selected { get; set; }

        public double DiscountedPrice { get; set; }

        //public double DiscountedPrice
        //{
        //    get
        //    {
        //        return Price * Quantity * (100-ItemDiscount)/100;
        //    }
        //}

        public int ItemDiscount { get; set; }
    }
}
