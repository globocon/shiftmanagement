using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Data.Models;
using ShiftManagement.Data.Providers;
using System.Security.Policy;

namespace ShiftManagement.WebPortal.Pages.Public
{
    [AllowAnonymous]
    public class ClientsController : Controller
    {
        private readonly IClientDataProvider _clientDataProvider;
        private readonly ICompanyDataProvider _companyDataProvider;
        public ClientsController(IWebHostEnvironment webHostEnvironment,
           IClientDataProvider clientDataProvider,
           ICompanyDataProvider companyDataProvider)
        {
            _clientDataProvider = clientDataProvider;
            _companyDataProvider = companyDataProvider;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult clientpage()
        {
            TempData["Message"] = null;
            var url = Request.HttpContext.Request.Path.ToString().Split("/");
            //var fullUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";

            if (url.Length > 0)
            {
                var companynameid = url[url.Length - 1];
                // Check if this company id exists
                var companyexists = _companyDataProvider.GetCompanyList().Where(x => x.CompanyNameID == companynameid).FirstOrDefault();
                if (companyexists != null)
                {
                    PublicClient client = new PublicClient();
                    client.CompanyId = companyexists.Id;
                    return View(client);
                }
                else
                {
                    return NotFound(); //this will automatically RedirectToPage("/Errors/404") as per settings in the program.cs file
                }
            }
            else
            {
                return NotFound(); //this will automatically RedirectToPage("/Errors/404") as per settings in the program.cs file
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult clientpage(PublicClient client)
        {
            TempData["MessageType"] = "F";

            PublicClientEmployeeRequest pcer = new PublicClientEmployeeRequest();
            pcer.Name = client.Name;
            pcer.Phone = client.Phone;
            pcer.Email = client.Emails;
            pcer.Address = client.Address;
            pcer.CompanyId = client.CompanyId;
            pcer.RequestType = "C";
            pcer.RequestDateTime = DateTime.UtcNow;
            pcer.RequestStatus = "PENDING";

            string msg = string.Empty;
            var result = _clientDataProvider.SaveNewPublicClientRequest(pcer, out msg);
            TempData["Message"] = msg;
            if (result)
            {
                TempData["MessageType"] = "S";
                PublicClient newclient = new PublicClient();
                newclient.CompanyId = client.CompanyId;
                ModelState.Clear();                
                return View(newclient);
            }
            else
            {
                return View(client);
            }

        }

    }

}
