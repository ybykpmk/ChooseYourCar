using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChooseYourCar.BusinessObject.Managers;
using ChooseYourCar.Entities;

namespace ChooseYourCar.Utilities
{
    public class CarSearch
    {
        public async Task<SearchAndResults> getCarSearchResultAsync(string carmodel,int pageNumber)
        {
            SearchAndResults searchAndResults = new SearchAndResults();
            searchAndResults.SelectedSearchOption.Models = carmodel;
            searchAndResults.SelectedSearchOption.MaxDistance = "all";
            searchAndResults.SelectedSearchOption.StockType = "used";
            searchAndResults.SelectedSearchOption.MaxPrice = 100000;
            searchAndResults.SelectedSearchOption.Page = pageNumber;
            searchAndResults.SelectedSearchOption.Makes = "tesla";
            searchAndResults.SelectedSearchOption.PageSize = 20;
            searchAndResults.SelectedSearchOption.Zip = "94596";

            searchAndResults.ListedCarsFromSearch = await CarManager.GetCarListBySearchOptions(searchAndResults.SelectedSearchOption);

            return searchAndResults;
        }
    }
}
