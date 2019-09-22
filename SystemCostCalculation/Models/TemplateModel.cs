using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCostCalculation.Models
{
    public class TemplateModel
    {
        public string TemplateSaveName { get; set; }

        public string SystemName { get; set; }

        public string TemplateCode { get; set; }

        public string TenderName { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Location { get; set; }

        public string Remark { get; set; }

        public double TotalCost { get; set; }

        public int Discount { get; set; }

        public List<ItemModel> SystemItems { get; set; }
        

    }
}
