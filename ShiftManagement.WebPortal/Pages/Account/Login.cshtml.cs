using ShiftManagement.Data.Models;
using ShiftManagement.WebPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftManagement.Data.Enums;

namespace ShiftManagement.WebPortal.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserAuthenticationService _userAuthentication;

        public LoginModel(IUserAuthenticationService userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }

        public void OnGet()
        {           
            LoginUser = new User_Login();
        }

        [BindProperty]
        public User_Login LoginUser { get; set; }

        public IActionResult OnPost(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = Url.Page("/Index");
           
            var isValidLogin = _userAuthentication.TryGetLoginUser(LoginUser, out USR_Users user);

            if (!isValidLogin)
                ModelState.AddModelError("Username", "Incorrect User Name or Password");
            //else if (!user.IsAdmin)
            //    ModelState.AddModelError("Username", "Not authorized to access this page");
            else
            {
                _userAuthentication.SignInUser(user);
    //            if (user.RoleType == RoleType.SAdmin)
    //                return Redirect(Url.Page("/SAdminIndex"));
    //            else if(user.RoleType == RoleType.CAdmin)
    //                return Redirect(Url.Page("/CAdminIndex"));
				//else if (user.RoleType == RoleType.Staff)
				//	return Redirect(Url.Page("/StaffIndex"));
				//else
				//	return Redirect(Url.Page("/Account/Unauthorized"));
				return Redirect(Url.Page("/Index"));
			}
            return Page();
        }

        
    }
}
