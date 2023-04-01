using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChooseYourCar.DataAccess;
using ChooseYourCar.Entities;

namespace ChooseYourCar.BusinessObject.Managers
{
    public class CarManager
    {
        public static Task<List<Car>> GetCarListBySearchOptions(SearchOptions selectedSearchOption)
        {
            return CarDal.GetCarListBySearchOptions(selectedSearchOption);
        }

        public static Task<Car> GetSelectedCarDetails(Car car)
        {
            return CarDal.GetSelectedCarDetails(car);
        }

        public static Task<Car> ClickHomeDelivery(Car selectedCar)
        {
            return CarDal.ClickHomeDelivery(selectedCar);
        }
    }
}
