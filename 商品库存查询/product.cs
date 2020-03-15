using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 商品库存查询
{
    class product
    {
        private string Id;
        public string id
        {
            get { return Id; }
            set { Id = value; }
        }
        private string Name;
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }
        private string Price;
        public string price
        {
            get { return Price; }
            set { Price = value; }
        }
        private string Quantity;
        public string quantity
        {
            get { return Quantity; }
            set { Quantity = value; }
        }
    }
}
