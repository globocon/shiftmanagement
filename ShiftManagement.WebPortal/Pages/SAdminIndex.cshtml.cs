using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftManagement.Data.Models;
using ShiftManagement.Data.Providers;

namespace ShiftManagement.WebPortal.Pages
{
    public class SAdminIndexModel : PageModel
    {
		private readonly IClientDataProvider _clientDataProvider;

		public SAdminIndexModel(IClientDataProvider clientDataProvider)
		{
			_clientDataProvider = clientDataProvider;
		}

		[BindProperty]
		public List<Clients> ClientsList { get; set; }

		public void OnGet()
        {
			//ClientsList.Clear();
			ClientsList = _clientDataProvider.GetClientsList();
        }

		public PartialViewResult OnGetClientProfileSettings(int clientId)
		{
			var client = _clientDataProvider.GetClientsById(clientId);
			client ??= new Clients() { };			
			return Partial("_clientProfile", client);
		}
	}
}
