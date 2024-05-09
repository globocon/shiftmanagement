
using ShiftManagement.Data.Helpers;
using ShiftManagement.Data.Models;
using ShiftManagement.Data.Providers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Security.Claims;
using ShiftManagement.WebPortal.Helpers;

namespace ShiftManagement.WebPortal.Pages.Admin
{
    public class SettingsModel : PageModel
    {


        private readonly IUserDataProvider _userDataProvider;
               
        public SettingsModel(IWebHostEnvironment webHostEnvironment,
            IUserDataProvider userDataProvider)
        {
            _userDataProvider = userDataProvider;
        }
        public ActionResult OnGet()
        {
            var userClaimsRole = User.Claims.Single(x => x.Type == ClaimTypes.Role).Value;
            if (!AuthUserHelper.IsAdminUserLoggedIn)
                return Redirect(Url.Page("/Account/Unauthorized"));

            return Page();
			
		}


        #region User
        public JsonResult OnGetUsers()
        {
            var users = _userDataProvider.GetUsers(true).Select(x => new { x.Id, x.UserName, x.IsDeleted });
            return new JsonResult(users);
        }

        public JsonResult OnPostShowPassword(USR_Users user)
        {
            var value = string.Empty;
            try
            {
                var currUser = _userDataProvider.GetUsers().SingleOrDefault(x => x.Id == user.Id);
                if (currUser != null)
                    value = PasswordHelper.DecryptPassword(currUser.Password);
            }
            catch
            {
            }

            return new JsonResult(value);
        }

        public JsonResult OnPostUser(USR_Users record)
        {
            var status = true;
            var message = "Success";
            try
            {
                if (record != null)
                {
                    record.Password = PasswordHelper.EncryptPassword(record.Password);
                    _userDataProvider.SaveUser(record);
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




        public JsonResult OnPostUpdateUserStatus(Guid id, bool deleted)
        {
            var status = true;
            var message = "Success";
            try
            {
                _userDataProvider.UpdateUserStatus(id, deleted);
            }
            catch (Exception ex)
            {
                status = false;
                message = "Error " + ex.Message;
            }

            return new JsonResult(new { status, message });
        }
        #endregion User


    }
}
