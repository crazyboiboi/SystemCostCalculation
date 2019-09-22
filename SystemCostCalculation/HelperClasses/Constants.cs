using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCostCalculation.Models;

namespace SystemCostCalculation.HelperClasses
{
    class Constants
    {
        public static ObservableCollection<TemplateModel> templates = new ObservableCollection<TemplateModel>();


        public static void AddTemplate(TemplateModel template)
        {
            templates.Add(template);
            Console.WriteLine(templates.Count);
        }
    }

}
