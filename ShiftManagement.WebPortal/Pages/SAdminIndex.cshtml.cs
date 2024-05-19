using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftManagement.Data.Models;
using ShiftManagement.Data.Providers;
using ShiftManagement.WebPortal.Helpers;
using System.Security.Principal;

namespace ShiftManagement.WebPortal.Pages
{
    public class SAdminIndexModel : PageModel
    {
		private readonly ICompanyDataProvider _companyDataProvider;

		public SAdminIndexModel(ICompanyDataProvider companyDataProvider)
		{
            _companyDataProvider = companyDataProvider;
		}

		[BindProperty]
		public List<Company> CompanyList { get; set; }

        [BindProperty]
        public Company newCompany { get; set; }

        public void OnGet()
        {            
            CompanyList = _companyDataProvider.GetCompanyList();
			newCompany = new Company() {
				Id = new Guid(),
                IsMasterCompany = false,
                IsDeleted = false
            };

        }
        //public async Task<IActionResult> OnPostSaveClientDetailsAsync(int id, string name)
        //{ 

        //    try
        //    {
              
        //        var success = await _clientDataProvider.SaveClientDetailsAsync(id, name);

        //        if (success)
        //        {
        //            return new JsonResult(new { success = true, message = "Client details saved successfully." });
        //        }
        //        else
        //        {
        //            return new JsonResult(new { success = false, message = "Failed to save client details." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        return new JsonResult(new { success = false, message = "An error occurred while saving client details." });
        //    }
        //}
		


		public PartialViewResult OnGetClientProfileSettings(Guid companyId)		
		{
			var company = _companyDataProvider.GetCompanyById(companyId);
            company ??= new Company() { };			
			return Partial("_companyProfile", company);
		}

		public IActionResult OnPostCheckIdAvailable(string companyId)
        {
            bool success = false;
            string message = "Id already in use.";
            success = _companyDataProvider.CheckIfCompanyIdAvailable(companyId);
            if (success)
                message = "Id is available.";

            return new JsonResult(new { success = success, message = message });
        }
        public JsonResult OnPostNewCompany()
        {
            var success = false;
            var message = "New company creation failed.<br>";
            if (newCompany != null)
            {
                try
                {
                    if (newCompany.CompanyName != string.Empty && newCompany.CompanyNameID != string.Empty)
                    {
                        string msg = "";
                        newCompany.CreationDate = DateTime.UtcNow;
                        newCompany.CreatedUserID = AuthUserHelper.LoggedInUserId;
                        success = _companyDataProvider.CreateNewCompany(newCompany, out msg);
                        message = msg;
                    }
                    else
                    {
                        message += "Please fill all required fields. !!!";
                    }
                }
                catch (Exception ex)
                {
                    message += ex.Message;
                }
            }
            else { message += "Invalid company details. !!!"; }
            return new JsonResult(new { success = success, message = message });
        }

        public JsonResult OnPostDeleteCompany(Guid companyid)
        {
            var success = false;
            var message = "Unable to delete company.<br>";
            if (companyid != new Guid("00000000-0000-0000-0000-000000000000"))
            {
                try
                {
                    if (companyid != null)
                    {
                        string msg = "";                        
                        success = _companyDataProvider.DeleteCompany(companyid, AuthUserHelper.LoggedInUserId, out msg);
                        message = msg;
                    }
                    else
                    {
                        message += "Invalid company details. !!!";
                    }
                }
                catch (Exception ex)
                {
                    message += ex.Message;
                }
            }
            else { message += "Invalid company Id. !!!"; }
            return new JsonResult(new { success = success, message = message });
        }
    }
}
