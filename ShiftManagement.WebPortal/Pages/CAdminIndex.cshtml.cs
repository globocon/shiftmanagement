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
		public JsonResult OnPostDeleteClientDetails(int id)
		{
			var success = false;
			var message = "Unable to delete client. Invalid client id.";
			try
			{

				_clientDataProvider.DeleteClientDetails(id);

				message = "client '" + id + "' marked for deletion !!!";
				success = true;
				ClientsList = _clientDataProvider.GetClientsList();
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}

			return new JsonResult(new { success, message });
		}
		public JsonResult OnPostSaveUpdateClientDetails(Clients record)
		{

			try
			{

				int success = _clientDataProvider.SaveOrUpdateClientDetails(record);

				if (success == 1)
				{
					return new JsonResult(new { success = true, message = "Client details updated." });
				}
				else if (success == 2)
				{
					return new JsonResult(new { success = false, message = "Client details added." });
				}
				else
				{
					return new JsonResult(new { success = false, message = "An error occurred while updating client details." });
				}
			}
			catch (Exception ex)
			{
				// Log the exception
				return new JsonResult(new { success = false, message = "An error occurred while updating client details." });
			}
		}
		public JsonResult OnPostDeleteEmployeeDetails(int id)
		{
			var success = false;
			var message = "Unable to delete Employee. Invalid Employee id.";
			try
			{

				_employeeDataProvider.DeleteEmployeeDetails(id);

				message = "Employee '" + id + "' marked for deletion !!!";
				success = true;

			}
			catch (Exception ex)
			{
				message = ex.Message;
			}

			return new JsonResult(new { success, message });
		}
		public JsonResult OnPostSaveUpdateEmployeeDetails(Employees record)
		{

			try
			{

				int success = _employeeDataProvider.SaveOrUpdateEmployeeDetails(record);

				if (success == 1)
				{
					return new JsonResult(new { success = true, message = "Employee details updated." });
				}
				else if (success == 2)
				{
					return new JsonResult(new { success = false, message = "Employee details added." });
				}
				else
				{
					return new JsonResult(new { success = false, message = "An error occurred while updating Employee details." });
				}
			}
			catch (Exception ex)
			{
				// Log the exception
				return new JsonResult(new { success = false, message = "An error occurred while updating client details." });
			}
		}
		public PartialViewResult OnGetEmployeeProfileSettings(int employeeId)
		{
			var Employee = _employeeDataProvider.GetEmployeesById(employeeId);
			Employee ??= new Employees() { };
			return Partial("_employeeProfile", Employee);
		}
	}
}
