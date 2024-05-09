using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftManagement.Data.Helpers;
using ShiftManagement.Data.Models;
using ShiftManagement.Data.Providers;

namespace ShiftManagement.WebPortal.Pages
{
    public class CAdminIndexModel : PageModel
    {
		private readonly IClientDataProvider _clientDataProvider;
		private readonly IEmployeeDataProvider _employeeDataProvider;
		private readonly IShiftDataProvider _shiftDataProvider;
		public CAdminIndexModel(IClientDataProvider clientDataProvider, IEmployeeDataProvider employeeDataProvider,
			 IShiftDataProvider shiftDataProvider)
		{
			_clientDataProvider = clientDataProvider;
			_employeeDataProvider = employeeDataProvider;
			_shiftDataProvider = shiftDataProvider;
		}

		[BindProperty]
		public List<Clients> ClientsList { get; set; }
		public void OnGet()
        {
			ClientsList = _clientDataProvider.GetClientsList();
		}

		public PartialViewResult OnGetClientProfileSettings(int clientId)
		{
			var client = _clientDataProvider.GetClientsById(clientId);
			client ??= new Clients() { };
			return Partial("_clientProfile", client);
		}

		public JsonResult OnGetDataForStaffTable()
		{
			return new JsonResult(_employeeDataProvider.GetEmployees());
		}

		public IActionResult OnGetEmployeeShiftData(DateTime? shiftDate)
		{
			List<ShiftDetail> ShiftData = new List<ShiftDetail>();
			var startDate = DateHelper.thisWeekStart;
			var endDate = DateHelper.thisWeekEnd;

			if (shiftDate != null)
			{
				DateTime ndtm = new DateTime(shiftDate.Value.Year, shiftDate.Value.Month, shiftDate.Value.Day);
				startDate = DateHelper.thisWeekStartOfDate(ndtm);
				endDate = DateHelper.thisWeekEndOfDate(ndtm);
			}
			ShiftData = _shiftDataProvider.GetAllEmployeeShiftDataForTheWeek(startDate, endDate);

			return new JsonResult(new { ShiftData, startDate = startDate.ToString("dd/MM/yyyy"), endDate = endDate.ToString("dd/MM/yyyy") });
		}
	}
}
