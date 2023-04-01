using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.OffScreen;
using ChooseYourCar.BusinessObject.Managers;
using ChooseYourCar.Entities;
using ChooseYourCar.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


CefSettings cefSettings = new CefSettings()
{
    CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"),
    LogSeverity = LogSeverity.Disable,
    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36",
    ChromeRuntime = false,
    JavascriptFlags = "--trace-opt",
    IgnoreCertificateErrors = true,
    CookieableSchemesExcludeDefaults = false,
    PersistSessionCookies = false,
};

#if ANYCPU
            //Only required for PlatformTarget of AnyCPU
            CefRuntime.SubscribeAnyCpuAssemblyResolver();
#endif

Cef.Initialize(cefSettings, performDependencyCheck: true, browserProcessHandler: null);

List<ExportResultToJsonFile> exportResultToJsonFileList = new List<ExportResultToJsonFile>();
ExportResultToJsonFile exportResultOfTeslaModelSToJsonFile = new ExportResultToJsonFile();
ExportResultToJsonFile exportResultOfTeslaModelXToJsonFile = new ExportResultToJsonFile();

Console.Write("Hi, now we will travel in www.cars.com. First, let's log in to this website. John will do these activities.");
Console.WriteLine();
Console.WriteLine();

User user = new User() { Email = "johngerson808@gmail.com", Password = "test8008" };

if (await UserManager.Login(user))
{
    Console.WriteLine("Yes, John logged in successfully");
}

#region TeslaModelS
Console.WriteLine();
Console.WriteLine("John set the search criteria for Tesla Model S and hit the search button");
exportResultOfTeslaModelSToJsonFile.SearchAndResultsList.Add(await new CarSearch().getCarSearchResultAsync("tesla-model_s", 1));
exportResultOfTeslaModelSToJsonFile.SearchAndResultsList.Add(await new CarSearch().getCarSearchResultAsync("tesla-model_s", 2));

if (exportResultOfTeslaModelSToJsonFile.SearchAndResultsList[0].ListedCarsFromSearch.Count > 0)
{
    int random = new Random().Next(exportResultOfTeslaModelSToJsonFile.SearchAndResultsList[0].ListedCarsFromSearch.Count - 1);
    Car selectedCar = await CarManager.GetSelectedCarDetails(exportResultOfTeslaModelSToJsonFile.SearchAndResultsList[0].ListedCarsFromSearch[random]);
    Console.WriteLine("John select a car ({0}) to get its detail", exportResultOfTeslaModelSToJsonFile.SearchAndResultsList[0].ListedCarsFromSearch[random].Title);

    Console.WriteLine("And Then John clicked Home Delivery button");
    exportResultOfTeslaModelSToJsonFile.SelectedCarFromSearchResults = await CarManager.ClickHomeDelivery(selectedCar);
}

exportResultToJsonFileList.Add(exportResultOfTeslaModelSToJsonFile);

#endregion   


#region TeslaModeX
Console.WriteLine();
Console.WriteLine("John set the search criteria for Tesla Model X and hit the search button");
exportResultOfTeslaModelXToJsonFile.SearchAndResultsList.Add(await new CarSearch().getCarSearchResultAsync("tesla-model_x", 1));
exportResultOfTeslaModelXToJsonFile.SearchAndResultsList.Add(await new CarSearch().getCarSearchResultAsync("tesla-model_x", 2));

if (exportResultOfTeslaModelXToJsonFile.SearchAndResultsList[0].ListedCarsFromSearch.Count > 0)
{
    int random = new Random().Next(exportResultOfTeslaModelXToJsonFile.SearchAndResultsList[0].ListedCarsFromSearch.Count - 1);
    Car selectedCar = await CarManager.GetSelectedCarDetails(exportResultOfTeslaModelXToJsonFile.SearchAndResultsList[0].ListedCarsFromSearch[random]);
    Console.WriteLine("John select a car ({0}) to get its detail", exportResultOfTeslaModelXToJsonFile.SearchAndResultsList[0].ListedCarsFromSearch[random].Title);

    Console.WriteLine("And Then John clicked Home Delivery button");
    exportResultOfTeslaModelXToJsonFile.SelectedCarFromSearchResults = await CarManager.ClickHomeDelivery(selectedCar);
}

exportResultToJsonFileList.Add(exportResultOfTeslaModelXToJsonFile);

#endregion   

(new ExportToJsonFile()).ExportActivitiesToJsonFile(exportResultToJsonFileList);

Console.ReadLine();

