using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChooseYourCar.DataAccess;
using ChooseYourCar.Entities;

namespace ChooseYourCar.BusinessObject.Managers
{
    public class UserManager
    {
        public static async Task<bool> Login(User user)
        {
            if (user!=null)
            {               
               return await UserDal.Login(user);
            }
            return false;
        }
    }
}
