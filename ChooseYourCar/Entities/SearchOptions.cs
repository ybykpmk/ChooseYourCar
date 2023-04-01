using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseYourCar.Entities
{
    public class SearchOptions
    {
        public string StockType { get; set; }
        public string Makes { get; set; }
        public string Models { get; set; }
        public double MaxPrice { get; set; }
        public string MaxDistance { get; set; }
        public string Zip { get; set; }        
        public int Page { get; set; }
        public int PageSize { get; set; }  
    }
}
