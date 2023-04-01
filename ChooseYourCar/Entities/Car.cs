using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseYourCar.Entities
{
    public class Car
    {
        public Car()
        {
            PriceHistories = new List<PriceHistory>();
            CarImageUrls = new List<string>();
            HomeDeliveryMessages= new List<string>();
        }
        public string[] Badges { get; set; }
        public string BodyStyle { get; set; }
        public string CanonicalMmt { get; set; }
        public List<string> CarImageUrls { get; set; }
        public string Category { get; set; }
        public bool CertifiedPreowned { get; set; }
        public bool CpoIndicator { get; set; }
        public string CustomerId { get; set; }
        public string Drivetrain { get; set; }
        public string Engine { get; set; }
        public string ExteriorColor { get; set; }
        public string FuelType { get; set; }
        public List<string> HomeDeliveryMessages { get; set; }
        public string InteriorColor { get; set; } 
        public string ListingId { get; set; }
        public string Make { get; set; }
        public double Mileage { get; set; }
        public string Model { get; set; }
        public string Msrp { get; set; }
        public bool NviProgram { get; set; }
        public double Price { get; set; }
        public List<PriceHistory> PriceHistories { get; set; }
        public string SellersNote { get; set; }
        public string SellerType { get; set; }
        public bool Sponsored { get; set; }
        public string SponsoredType { get; set; }
        public string StockSub { get; set; }
        public string StockType { get; set; }
        public string Trim { get; set; }
        public string Title { get; set; }
        public string Transmission { get; set; }
        public string Url { get; set; }
        public string Vin { get; set; }
        public int Year { get; set; }
    }
}
