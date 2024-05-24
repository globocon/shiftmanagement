using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Data.Models;
using ShiftManagement.Data.Providers;

namespace ShiftManagement.WebPortal.Pages.Public
{
    [AllowAnonymous]
    public class StaffController : Controller
    {
        private readonly IEmployeeDataProvider _employeeDataProvider;
        private readonly ICompanyDataProvider _companyDataProvider;
        public StaffController(IWebHostEnvironment webHostEnvironment,
           IEmployeeDataProvider employeeDataProvider,
           ICompanyDataProvider companyDataProvider)
        {
            _employeeDataProvider = employeeDataProvider;
            _companyDataProvider = companyDataProvider;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult staffpage()
        {
            var url = Request.HttpContext.Request.Path.ToString().Split("/");
            //var fullUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";

            if (url.Length > 0)
            {
                var companynameid = url[url.Length - 1];
                // Check if this company id exists
                var companyexists = _companyDataProvider.GetCompanyList().Where(x => x.CompanyNameID == companynameid).FirstOrDefault();
                if (companyexists != null)
                {
                    PublicEmployee employee = new PublicEmployee();
                    employee.CompanyId = companyexists.Id;
                    return View(employee);
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
    }
}
