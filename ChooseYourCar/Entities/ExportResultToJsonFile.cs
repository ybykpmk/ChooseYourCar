using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseYourCar.Entities
{
    public class ExportResultToJsonFile
    {
        public ExportResultToJsonFile() {
            SearchAndResultsList = new List<SearchAndResults>();
            SelectedCarFromSearchResults = new Car();
        }
        public List<SearchAndResults> SearchAndResultsList { get; set; }
        public Car SelectedCarFromSearchResults { get; set; }
    }
}
