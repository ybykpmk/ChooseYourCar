using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.OffScreen;
using ChooseYourCar.Entities;
using ChooseYourCar.Utilities;
using Newtonsoft.Json;

namespace ChooseYourCar.DataAccess
{
    public class CarDal
    {
        public static async Task<List<Car>> GetCarListBySearchOptions(SearchOptions selectedSearchOption)
        {
            string searchUrl = new SearchUrl().GetSearchUrl(selectedSearchOption);
            var chromiumWebBrowser = new ChromiumWebBrowser(searchUrl);
            List<Car> cars = new List<Car>();
            Car car = new Car();

            var initialLoadResponse = await chromiumWebBrowser.WaitForInitialLoadAsync();

            if (!initialLoadResponse.Success)
            {
                throw new Exception(string.Format("Page load failed with ErrorCode:{0}, HttpStatusCode:{1}", initialLoadResponse.ErrorCode, initialLoadResponse.HttpStatusCode));
            }

            dynamic rawDataReq = await chromiumWebBrowser.EvaluateScriptAsync("document.querySelector('div[class=\"sds-page-section listings-page\"]').getAttribute(\"data-site-activity\")");
            var CarRawData = JsonConvert.DeserializeObject(rawDataReq.Result.ToString());
            int carCount = Convert.ToInt32((await chromiumWebBrowser.EvaluateScriptAsync("document.querySelectorAll('div[data-tracking-type=\"srp-vehicle-card\"]').length"))?.Result?.ToString());


            if (CarRawData != null && CarRawData.vehicleArray != null)
            {
                for (int i = 0; i < carCount; i++)
                {
                    car = await GetCarData(CarRawData?.vehicleArray[i], searchUrl, i, chromiumWebBrowser);
                    cars.Add(car);
                }
            }
            return cars;
        }

        public static async Task<Car> GetCarData(dynamic carData, string searchUrl, int i, ChromiumWebBrowser browser)
        {
            Car currentCar = new Car();

            currentCar.BodyStyle = carData.bodystyle;
            currentCar.CanonicalMmt = carData.canonical_mmt;
            currentCar.Category = carData.cat;
            currentCar.CertifiedPreowned = carData.certified_preowned;
            currentCar.CpoIndicator = carData.cpo_indicator;
            currentCar.CustomerId = carData.customer_id;
            currentCar.ExteriorColor = carData.exterior_color;
            currentCar.FuelType = carData.fuel_type;
            currentCar.ListingId = carData.listing_id;
            currentCar.Make = carData.make;
            currentCar.Mileage = carData.mileage;
            currentCar.Model = carData.model;
            currentCar.Msrp = carData.msrp;
            currentCar.NviProgram = carData.nvi_program;
            currentCar.Price = carData.price;
            currentCar.SellerType = carData.seller_type;
            currentCar.Sponsored = carData.sponsored;
            currentCar.SponsoredType = carData.sponsored_type;
            currentCar.StockSub = carData.stock_sub;
            currentCar.StockType = carData.stock_type;
            currentCar.Trim = carData.trim;
            currentCar.Vin = carData.vin;
            currentCar.Year = carData.year;
            currentCar.Title = carData.year + " " + carData.make + " " + carData.model + " " + carData.trim;
            currentCar.Url = searchUrl + $"vehicledetail/{carData.listing_id}";

            if (carData.badges != null && carData.badges.Count > 0 && carData.badges.Contains("great_deal"))
            {
                currentCar.CarBadges.IsGreatDeal = true;
            }
            else
            {
                currentCar.CarBadges.IsGreatDeal = false;
            }

            if (carData.badges != null && carData.badges.Count > 0 && carData.badges.Contains("home_delivery"))
            {
                currentCar.CarBadges.IsHomeDelivery = true;
            }
            else
            {
                currentCar.CarBadges.IsHomeDelivery = false;
            }

            if (carData.badges != null && carData.badges.Count > 0 && carData.badges.Contains("price_drop_in_cents"))
            {
                currentCar.CarBadges.IsPriceDropInCents = true;
            }
            else
            {
                currentCar.CarBadges.IsPriceDropInCents = false;
            }

            if (carData.badges != null && carData.badges.Count > 0 && carData.badges.Contains("virtual_appt"))
            {
                currentCar.CarBadges.IsVirtualAppointments = true;
            }
            else
            {
                currentCar.CarBadges.IsVirtualAppointments = false;
            }

            int carImageCount = Convert.ToInt32((await browser.EvaluateScriptAsync($"document.querySelectorAll('div[data-tracking-type=\"srp-vehicle-card\"]')[{i}].querySelectorAll('div[class=\"image-wrap\"').length"))?.Result?.ToString());

            for (int j = 0; j < carImageCount; j++)
            {
                var carImageData = await browser.EvaluateScriptAsync($"document.querySelectorAll('div[data-tracking-type=\"srp-vehicle-card\"]')[{i}].querySelectorAll('div[class=\"image-wrap\"')[{j}].querySelector('img').getAttribute('data-src')");
                if (carImageData != null && carImageData.Result != null && !string.IsNullOrEmpty(carImageData.Result.ToString()))
                {
                    currentCar.CarImageUrls.Add(carImageData.Result.ToString());
                }
            }

            return currentCar;
        }

        public static async Task<Car> GetSelectedCarDetails(Car selectedCar)
        {
            var chromiumWebBrowser = new ChromiumWebBrowser(selectedCar.Url);

            var initialLoadResponse = await chromiumWebBrowser.WaitForInitialLoadAsync();

            if (!initialLoadResponse.Success)
            {
                throw new Exception(string.Format("Page load failed with ErrorCode:{0}, HttpStatusCode:{1}", initialLoadResponse.ErrorCode, initialLoadResponse.HttpStatusCode));
            }

            var selectedCarRawData = await chromiumWebBrowser.EvaluateScriptAsync("document.querySelector('dl[class=\"fancy-description-list\"]').children[3].textContent");
            selectedCar.InteriorColor = selectedCarRawData?.Result?.ToString();

            selectedCarRawData = await chromiumWebBrowser.EvaluateScriptAsync("document.querySelector('dl[class=\"fancy-description-list\"]').children[5].textContent");
            selectedCar.Drivetrain = selectedCarRawData?.Result?.ToString();

            selectedCarRawData = await chromiumWebBrowser.EvaluateScriptAsync("document.querySelector('dl[class=\"fancy-description-list\"]').children[9].textContent");
            selectedCar.Transmission = selectedCarRawData?.Result?.ToString();

            selectedCarRawData = await chromiumWebBrowser.EvaluateScriptAsync("document.querySelector('dl[class=\"fancy-description-list\"]').children[11].textContent");
            selectedCar.Engine = selectedCarRawData?.Result?.ToString();

            selectedCarRawData = await chromiumWebBrowser.EvaluateScriptAsync("document.querySelector('div[class=\"sellers-notes\"]').textContent");
            selectedCar.SellersNote = selectedCarRawData?.Result?.ToString();


            selectedCarRawData = await chromiumWebBrowser.EvaluateScriptAsync("document.querySelector('cars-price-history').getAttribute('price-history-chart')");
            if (selectedCarRawData != null && selectedCarRawData.Result != null)
            {
                dynamic carHistoryRawData = JsonConvert.DeserializeObject(selectedCarRawData.Result.ToString());

                string dateTime;
                if (carHistoryRawData != null && carHistoryRawData.data_points != null)
                {
                    for (int i = 0; i < carHistoryRawData.data_points.Count; i++)
                    {
                        dateTime = carHistoryRawData.x_axis_ticks[i].label + "/" + DateTime.Now.Year.ToString().Substring(2, 2);
                        selectedCar.PriceHistories.Add(new PriceHistory
                        {
                            CarPricedDate = dateTime,
                            CarPrice = Convert.ToDouble(carHistoryRawData.data_points[i].y)
                        });
                    }
                }
            }
            return selectedCar;
        }


        public static async Task<Car> ClickHomeDelivery(Car selectedCar)
        {
            var chromiumWebBrowser = new ChromiumWebBrowser(selectedCar.Url);

            var initialLoadResponse = await chromiumWebBrowser.WaitForInitialLoadAsync();

            if (!initialLoadResponse.Success)
            {
                throw new Exception(string.Format("Page load failed with ErrorCode:{0}, HttpStatusCode:{1}", initialLoadResponse.ErrorCode, initialLoadResponse.HttpStatusCode));
            }

            string message;
            var homeDeliveryClick = await chromiumWebBrowser.EvaluateScriptAsync("document.getElementsByClassName('sds-badge sds-badge--home-delivery')[0].click()");

            var rawData = await chromiumWebBrowser.EvaluateScriptAsync("document.getElementsByClassName('badge-description').length");
            int tablerowcount = Convert.ToInt32(rawData?.Result?.ToString());

            for (int i = 0; i < tablerowcount; i++)
            {
                rawData = await chromiumWebBrowser.EvaluateScriptAsync($"document.getElementsByClassName('badge-description')[{i}].innerHTML");
                message = rawData?.Result?.ToString();
                selectedCar.HomeDeliveryMessages.Add(message);
            }
            return selectedCar;
        }

    }
}
