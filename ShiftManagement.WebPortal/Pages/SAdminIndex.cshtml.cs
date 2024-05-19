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
		
        public JsonResult  OnPostSaveUpdateClientDetails(Clients record)
		{

			try
			{

				int success = _clientDataProvider.SaveOrUpdateClientDetails(record);

				if (success==1)
				{
					return new JsonResult(new { success = true, message = "Client details updated." });
				}
				else if (success==2)
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
		
		public PartialViewResult OnGetClientProfileSettings(int clientId)
		{
			var client = _clientDataProvider.GetClientsById(clientId);
			client ??= new Clients() { };			
			return Partial("_clientProfile", client);
		}
	}
}
