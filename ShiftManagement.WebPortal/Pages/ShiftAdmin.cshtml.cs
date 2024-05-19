using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol.Plugins;
using ShiftManagement.Data.Helpers;
using ShiftManagement.Data.Models;
using ShiftManagement.Data.Providers;
using ShiftManagement.WebPortal.Helpers;

namespace ShiftManagement.WebPortal.Pages
{
    public class ShiftAdminModel : PageModel
    {
        private readonly IClientDataProvider _clientDataProvider;
        private readonly IEmployeeDataProvider _employeeDataProvider;
        private readonly IShiftDataProvider _shiftDataProvider;

        public ShiftAdminModel(IClientDataProvider clientDataProvider, IEmployeeDataProvider employeeDataProvider,
             IShiftDataProvider shiftDataProvider)
        {
            _clientDataProvider = clientDataProvider;
            _employeeDataProvider = employeeDataProvider;
            _shiftDataProvider = shiftDataProvider;
        }

        [BindProperty]
        public List<Clients> ClientsList { get; set; }
        public List<Employees> EmployeesList { get; set; }

        public void OnGet()
        {
            EmployeesList = new List<Employees>();
            ClientsList = new List<Clients>();
            ClientsList = _clientDataProvider.GetClientsListForCompanyAdmin(AuthUserHelper.LoggedInUserId);
            EmployeesList = _employeeDataProvider.GetEmployeesForCompanyAdmin(AuthUserHelper.LoggedInUserId);
        }

        public IActionResult OnPostSaveNewShift(ShiftDetail sd)
        {
            string msg = string.Empty;
            sd.ShiftDate = DateTime.UtcNow;
            sd.CreateUserId = AuthUserHelper.LoggedInUserId;
            var IsSuccess = _shiftDataProvider.SaveNewShift(sd, out msg);
            return new JsonResult(new { success = IsSuccess, message = msg });
        }
        public IActionResult OnPostModifyShift(ShiftDetail sd)
        {
            string msg = string.Empty;
            var IsSuccess = _shiftDataProvider.UpdateShift(sd, out msg);
            return new JsonResult(new { success = IsSuccess, message = msg });
        }
        public IActionResult OnPostDeleteShift(int sdId)
        {
            string msg = string.Empty;
            var IsSuccess = _shiftDataProvider.DeleteShift(sdId, out msg);
            return new JsonResult(new { success = IsSuccess, message = msg });
        }
    }
}
