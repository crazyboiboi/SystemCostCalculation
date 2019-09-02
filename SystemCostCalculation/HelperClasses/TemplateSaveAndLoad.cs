using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCostCalculation.Models;

namespace SystemCostCalculation.HelperClasses
{
    public class TemplateSaveAndLoad
    {
        //Check the folder and keep a counter for how many files are there


        //A method to store files and its metadata
        private void saveNewTemplateData(TemplateModel template)
        {
            string path = "";
            using (StreamWriter sw = new StreamWriter(path))
            {
                //Index
                //Template Code
                //SystemName
                //Date Modified

                //Use WriteAllText() that takes in the line of template information
            }
        }

        //A method to return a path and a file name based on existing list of text files in the folder


        //Save the template in a .txt file
        public static void save(TemplateModel template)
        {
            string path = "";

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(template.systemName);
                sw.WriteLine(template.templateCode);
                sw.WriteLine(template.tenderName);
                sw.WriteLine(template.dateCreated);
                sw.WriteLine(template.dateModified);
                sw.WriteLine(template.location);
                sw.WriteLine(template.remark);
                sw.WriteLine(template.totalCost);
                sw.WriteLine(template.discount);

                foreach(ItemModel item in template.systemItems)
                {
                    sw.Write(item.ID + ";");
                    sw.Write(item.SupplierID + ";");
                    sw.Write(item.Code + ";");
                    sw.Write(item.Name + ";");
                    sw.Write(item.Category + ";");
                    sw.Write(item.Size + ";");
                    sw.Write(item.Type + ";");
                    sw.Write(item.Description + ";");
                    sw.Write(item.Price + ";");
                    sw.WriteLine(item.ItemDiscount);
                }
            }
            //MessageBox display message
        }



        //Load the template in a .txt file
        public static void load(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                TemplateModel template = new TemplateModel();

                template.systemName = lines[0];
                template.templateCode = lines[1];
                template.tenderName = lines[2];
                template.dateCreated = Convert.ToDateTime(lines[3]);
                template.dateModified = Convert.ToDateTime(lines[4]);
                template.location = lines[5];
                template.remark = lines[6];
                template.totalCost = Convert.ToDouble(lines[7]);
                template.discount = Convert.ToInt32(lines[8]);

                //Load items with delimiter
                for (int i=9; i<lines.Length; i++)
                {
                    string[] col = lines[i].Split(new char[] { ';' });

                    ItemModel item = new ItemModel();
                    item.ID = Convert.ToUInt16(col[0]);
                    item.SupplierID = Convert.ToUInt16(col[1]);
                    item.Code = col[2];
                    item.Name = col[3];
                    item.Category = col[4];
                    item.Size = Convert.ToUInt16(col[5]);
                    item.Type = col[6];
                    item.Description = col[7];
                    item.Price = Convert.ToDouble(col[8]);
                    item.ItemDiscount = Convert.ToInt16(col[9]);
                }

            } catch(FileNotFoundException e)
            {
                //Error message here
            }

        }



    }
}
