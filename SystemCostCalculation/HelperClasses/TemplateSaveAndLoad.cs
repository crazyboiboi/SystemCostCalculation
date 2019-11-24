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
        private static string fileName = "";
        private static int fileCount = 0;





        //Check the folder and keep a counter for how many files are there
        private static int countFiles()
        {
            return fileCount++;
        }

        //A method to return a path and a file name based on existing list of text files in the folder
        public static string generateTemplateSaveFilePath()
        {
            fileName = "testFile" + countFiles() + ".txt";
            return fileName;
        }



        //A method to store files and its metadata
        //TO-DO: Only do this OnWindowClosing()
        public static void saveTemplateData()
        {
            string desktopPath = "D:\\";
            string path = Path.Combine(desktopPath, "templatesSave.txt");
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(fileCount);

                foreach (TemplateModel template in Constants.templates)
                {
                    sw.Write(template.TemplateSaveName + ";");
                    sw.Write(template.SystemName + ";");
                    sw.Write(template.TemplateCode + ";");
                    sw.Write(template.TenderName + ";");
                    sw.WriteLine(template.DateModified);
                }
            }
        }

        public static void loadTemplateData()
        {
            string desktopPath = "D:\\";
            string path = Path.Combine(desktopPath, "templatesSave.txt");

            try
            {
                string[] lines = File.ReadAllLines(path);
                fileCount = Convert.ToInt32(lines[0]);

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] col = lines[i].Split(new char[] { ';' });
                    TemplateModel template = new TemplateModel()
                    {
                        TemplateSaveName = col[0],
                        SystemName = col[1],
                        TemplateCode = col[2],
                        TenderName = col[3],
                        DateModified = Convert.ToDateTime(col[4])
                    };
                    Constants.AddTemplate(template);
                }

            }
            catch (FileNotFoundException ex)
            {
                //Error message
            }
        }



        //Save the template in a .txt file
        public static void save(TemplateModel template)
        {
            string desktopPath = "D:\\";
            string path = Path.Combine(desktopPath, fileName);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(template.TemplateSaveName);
                sw.WriteLine(template.SystemName);
                sw.WriteLine(template.TemplateCode);
                sw.WriteLine(template.TenderName);
                sw.WriteLine(template.DateCreated);
                sw.WriteLine(template.DateModified);
                sw.WriteLine(template.Location);
                sw.WriteLine(template.Remark);
                sw.WriteLine(template.Discount);

                foreach (ItemModel item in template.SystemItems)
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
                    sw.Write(item.Quantity + ";");
                    sw.WriteLine(item.ItemDiscount);
                }
            }
            //MessageBox display message
        }



        //Load the template in a .txt file
        public static TemplateModel load(string fileName)
        {
            string desktopPath = "D:\\";
            string path = Path.Combine(desktopPath, fileName);

            try
            {
                string[] lines = File.ReadAllLines(path);
                TemplateModel template = new TemplateModel();
                List<ItemModel> items = new List<ItemModel>();

                template.TemplateSaveName = lines[0];
                template.SystemName = lines[1];
                template.TemplateCode = lines[2];
                template.TenderName = lines[3];
                template.DateCreated = Convert.ToDateTime(lines[4]);
                template.DateModified = Convert.ToDateTime(lines[5]);
                template.Location = lines[6];
                template.Remark = lines[7];
                template.Discount = Convert.ToInt16(lines[8]);

                //Load items with delimiter
                for (int i = 9; i < lines.Length; i++)
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
                    item.Quantity = Convert.ToUInt16(col[9]);
                    item.ItemDiscount = Convert.ToInt16(col[10]);

                    items.Add(item);
                }
                template.SystemItems = items;
                return template;

            }
            catch (FileNotFoundException e)
            {
                return null;
                //Error message here
            }

        }



    }
}
