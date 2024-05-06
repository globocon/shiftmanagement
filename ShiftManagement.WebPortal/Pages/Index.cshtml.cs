using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftManagement.Data.Enums;

namespace ShiftManagement.WebPortal.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
			if(HttpContext.User.Identity.IsAuthenticated)
			{
				if (HttpContext.User.IsInRole(RoleType.SAdmin.ToString()))
				{
					HttpContext.Response.Redirect("/SAdminIndex");
				}
				else if (HttpContext.User.IsInRole(RoleType.CAdmin.ToString()))
				{
					HttpContext.Response.Redirect("/CAdminIndex");
				}
				else if (HttpContext.User.IsInRole(RoleType.Staff.ToString()))
				{
					HttpContext.Response.Redirect("/StaffIndex");
				}
				else
				{
					HttpContext.Response.Redirect("/Account/Unauthorized");
				}
			}
			else
			{
				HttpContext.Response.Redirect("/Account/Login");
			}
			

		}
	}
}
