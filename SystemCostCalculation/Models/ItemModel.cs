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
        public string ID { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public string Type { get; set; }

        public double Price { get; set; }

    }
}
