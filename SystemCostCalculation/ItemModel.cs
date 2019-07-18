using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCostCalculation
{
    public class ItemModel : ObservableObject
    {

        private string _ID;
        private string _name;
        private int _size;
        private string _type;
        private double _price;

        public string ID
        {
            get { return _ID; }
            set
            {
                if (value != _ID)
                {
                    _ID = value;
                    OnPropertyChanged("Item ID");
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Item Name");
                }
            }
        }

        public int Size
        {
            get { return _size; }
            set
            {
                if (value != _size)
                {
                    _size = value;
                    OnPropertyChanged("Item Size");
                }
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged("Item Type");
                }
            }
        }

        public double Price {
            get { return _price; }
            set
            {
                if (value != _price)
                {
                    _price = value;
                    OnPropertyChanged("Item Price");
                }
            }
        }

    }
}
