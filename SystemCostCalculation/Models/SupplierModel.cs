using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCostCalculation.Models
{
    /// <summary>
    /// Class which models the properties of a Supplier
    /// </summary>
    public class SupplierModel
    {

        //Class properties
        public string Code { get; set; }

        public string Name { get; set; }

        public string Contact { get; set; }

        public string Address { get; set; }

        public string OtherDetails { get; set; }

        public int ID { get; set; }

        public bool Selected { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
