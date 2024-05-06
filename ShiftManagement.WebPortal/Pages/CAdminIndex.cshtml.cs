using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftManagement.Data.Models;
using ShiftManagement.Data.Providers;

namespace ShiftManagement.WebPortal.Pages
{
    public class CAdminIndexModel : PageModel
    {
		private readonly IClientDataProvider _clientDataProvider;
		private readonly IEmployeeDataProvider _employeeDataProvider;
		public CAdminIndexModel(IClientDataProvider clientDataProvider, IEmployeeDataProvider employeeDataProvider)
		{
			_clientDataProvider = clientDataProvider;
			_employeeDataProvider = employeeDataProvider;
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
	}
}
