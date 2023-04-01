using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.OffScreen;
using ChooseYourCar.Entities;

namespace ChooseYourCar.DataAccess
{
    public class UserDal
    {
        public static async Task<bool> Login(User user)
        {
            try
            {
                string webSiteUrl = "https://www.cars.com/";

                var chromiumWebBrowser = new ChromiumWebBrowser(webSiteUrl);

                var initialLoadResponse = await chromiumWebBrowser.WaitForInitialLoadAsync();

                if (!initialLoadResponse.Success)
                {
                    throw new Exception(string.Format("Page load failed with ErrorCode:{0}, HttpStatusCode:{1}", initialLoadResponse.ErrorCode, initialLoadResponse.HttpStatusCode));
                }


                await chromiumWebBrowser.EvaluateScriptAsync("document.querySelector('[class=nav-user-menu-button]').click()");
                Console.WriteLine("John clicked Menu");

                await chromiumWebBrowser.EvaluateScriptAsync("document.querySelector('[data-component=sign-in-start]').click()");
                Console.WriteLine("Then,he clicked Sign in button");

                await chromiumWebBrowser.EvaluateScriptAsync($"document.querySelector('[id=auth-modal-email]').value = '{user.Email}'");
                await chromiumWebBrowser.EvaluateScriptAsync($"document.querySelector('[id=auth-modal-current-password]').value = '{user.Password}'");
                Console.WriteLine("And now he set email and password");

                await chromiumWebBrowser.EvaluateScriptAsync("document.querySelector('cars-auth-modal').form[0].form.submit()");
                Console.WriteLine("John clicked the button to log in site");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
