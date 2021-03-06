using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MobileShopping.Model
{

    public class Product
    {
        public Product()
        {
            ListImage = new List<string>();
            ListSpec = new List<string>();
        }
        public List<string> ListImage { get; set; }
        public List<string> ListSpec { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string Thumbnail { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string BH { get; set; }

    }
}
