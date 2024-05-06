using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Runtime.InteropServices;
using ShiftManagement.Data.Helpers;
using ShiftManagement.Data.Models;
using ShiftManagement.Data.Providers;
using ShiftManagement.WebPortal.Helpers;
using ShiftManagement.WebPortal.Services;

namespace ShiftManagement.WebPortal.Pages
{
    public class NewRequestModel : PageModel
    {
        private readonly IViewDataService _viewDataService;

        public NewRequestModel ( IViewDataService viewDataService) 
        { 
            _viewDataService = viewDataService;
        }

        public void OnGet()
        {
            ViewData["EditValue"] = "-1"; // New Request
            if (Request.QueryString.HasValue)
            {
                if (Request.QueryString.Value != null)
                {                    
                    // Enabling edit in request
                    ViewData["EditValue"] = Request.Query["EditValue"];                    
                }                
            }
        }

        public JsonResult OnPostSaveNewRequest(NewRequestHeader record)
        {
            var status = true;
            var message = "Success";
            try
            {
                if (record != null)
                {

                    record.RequestCreateUserID = AuthUserHelper.LoggedInUserId;
                    record.Responded = false;

                  // int reqnumb =  _viewDataService.GetSequenceNextValue;

                    string requestNumber;
                    _viewDataService.SaveUpdateNewRequest(record, out requestNumber);
                    message = "New request created successfully with id: " + requestNumber;
                }
            }
            catch (Exception ex)
            {
                status = false;
                message = "Error " + ex.Message;

                if (ex.InnerException != null &&
                    ex.InnerException is SqlException &&
                    ex.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint"))
                {
                    message = "A user with this username already exists";
                }
            }

            return new JsonResult(new { status = status, message = message });
        }

        public JsonResult OnPostLoadDataForResponseEdit(int RequestId)
        {
           // return new JsonResult(new { records, total = dailyGuardLogs.Count });            
            return new JsonResult(_viewDataService.GetDataForEditResponse(RequestId));
        }
    }
}
