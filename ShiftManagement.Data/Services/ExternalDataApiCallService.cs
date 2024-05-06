using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ShiftManagement.Data.Services
{
    public interface IExternalDataApiCallService
    {

        Task<string> GetCountryMasterFromApi();
        string GetStaffMasterFromApi();

    }
    public class ExternalDataApiCallService : IExternalDataApiCallService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExternalDataApiCallService(IWebHostEnvironment webHostEnvironment)
            {
            _webHostEnvironment = webHostEnvironment;

        }

        public async Task<string> GetCountryMasterFromApi()
        {
            /* Used for dummy data reading from file instead of API */
            //var countryTempFileFolder = Path.Combine(_webHostEnvironment.WebRootPath, "SampleData");
            //using (StreamReader r = new StreamReader(Path.Combine(countryTempFileFolder, "CountryMaster.json") ))
            //{
            //    string json = r.ReadToEnd();
            //    // List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(json);
            //    return json;
            //}
                       
            using (var httpClient = new HttpClient())
            {
                /*https://restcountries.com/v3.1/all?fields=name,code */
                /*https://api.first.org/data/v1/countries */ /* reference https://api.first.org/v1/get-countries */
                using (var response = await httpClient.GetAsync("https://api.first.org/data/v1/countries"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return apiResponse;
                }
                
            }
        }
        public string GetStaffMasterFromApi()
        {
            var staffTempFileFolder = Path.Combine(_webHostEnvironment.WebRootPath, "SampleData");
            using (StreamReader r = new StreamReader(Path.Combine(staffTempFileFolder, "StaffMaster.json")))
            {
                string json = r.ReadToEnd();
                // List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(json);
                return json;
            }
        }
    }
}
