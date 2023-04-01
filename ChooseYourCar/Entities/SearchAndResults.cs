using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseYourCar.Entities
{
    public class SearchAndResults
    {
        public SearchAndResults()
        {
            ListedCarsFromSearch = new List<Car>();
            SelectedSearchOption = new SearchOptions();
        }
        public SearchOptions SelectedSearchOption { get; set; }
        public List<Car> ListedCarsFromSearch { get; set; }
    }
}
