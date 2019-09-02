using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCostCalculation.Models
{
    public class TemplateModel
    {
        public string systemName { get; set; }

        public string templateCode { get; set; }

        public string tenderName { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime dateModified { get; set; }

        public string location { get; set; }

        public string remark { get; set; }

        public double totalCost { get; set; }

        public int discount { get; set; }

        public List<ItemModel> systemItems { get; set; }
        

    }
}
