using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChooseYourCar.Entities;

namespace ChooseYourCar.Utilities
{
    public class SearchUrl
    {
        public string GetSearchUrl(SearchOptions selectedSearchOption)
        {
            string queryString = "https://www.cars.com/shopping/results/?";

            queryString = queryString + "list_price_max=" + selectedSearchOption.MaxPrice + "&";
            queryString = queryString + "maximum_distance=" + selectedSearchOption.MaxDistance + "&";
            queryString = queryString + "makes[]=" + selectedSearchOption.Makes + "&";
            queryString = queryString + "models=" + selectedSearchOption.Models + "&";
            queryString = queryString + "page=" + selectedSearchOption.Page + "&";
            queryString = queryString + "page_size=" + selectedSearchOption.PageSize + "&";
            queryString = queryString + "stock_type=" + selectedSearchOption.StockType + "&";
            queryString = queryString + "zip=" + selectedSearchOption.Zip;

            return queryString;
        }
    }
}
