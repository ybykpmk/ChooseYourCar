using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChooseYourCar.Entities;
using Newtonsoft.Json;

namespace ChooseYourCar.Utilities
{
    public class ExportToJsonFile
    {
        public void ExportActivitiesToJsonFile(List<ExportResultToJsonFile> exportResultToJsonFileList) {
            string jsonString = JsonConvert.SerializeObject(exportResultToJsonFileList);
            var jsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ExportResult.json");
            File.WriteAllText(jsonFilePath, jsonString);
            Console.WriteLine("And now all John's activities are getting saved in json file to your desktop {0}",jsonFilePath);
            Process.Start(new ProcessStartInfo(jsonFilePath)
            {
                UseShellExecute = true
            });

            Console.WriteLine("Exported Json file is saved");
        }
    }
}
