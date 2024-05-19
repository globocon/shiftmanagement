using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ShiftManagement.Data.Helpers;
using ShiftManagement.Data.Models;
using System.Security.Claims;
using ShiftManagement.Data.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace ShiftManagement.WebPortal.Services
{

    public interface IUserAuthenticationService
    {
        bool TryGetLoginUser(User_Login userLogin, out USR_Users user);
        void SignInUser(USR_Users user);
    }
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserDataProvider _userDataProvider;
        private static IHttpContextAccessor _httpContextAccessor;
        public UserAuthenticationService(IUserDataProvider userDataProvider, IHttpContextAccessor httpContextAccessor)
        {
            _userDataProvider = userDataProvider;
            _httpContextAccessor = httpContextAccessor;
        }              

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool TryGetLoginUser(User_Login userLogin, out USR_Users user)
        {
            user = null;

            if (userLogin != null &&
                !string.IsNullOrEmpty(userLogin.LoginID) &&
                !string.IsNullOrEmpty(userLogin.Password))
            {
                user = _userDataProvider.CheckUser(userLogin);
                if (user != null && PasswordHelper.VerifyEncryptedPassword(user.Password, userLogin.Password))
                    return true;
            }

            return false;
        }

        public void SignInUser(USR_Users user)
        {
            string role = user.RoleType.ToString();
            string dspnm = user.RoleType.GetDisplayName();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role , role)
            };
            claims.Add(new Claim(type: "RoleName", value: dspnm));
            if(user.ImageExtn != null)
                claims.Add(new Claim(type: "ImageExtn", value: user.ImageExtn));
            else
                claims.Add(new Claim(type: "ImageExtn", value: string.Empty));

            if (user.IsEmployee && user.EmployeeId != null)
                claims.Add(new Claim(type: "Eid", value: user.EmployeeId.ToString()));
            else
                claims.Add(new Claim(type: "Eid", value: string.Empty));

            if (user.CompanyId != null)
                claims.Add(new Claim(type: "CompanyId", value: user.CompanyId.ToString()));
            else
                claims.Add(new Claim(type: "CompanyId", value: string.Empty));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                RedirectUri = "/Account/Login" //Url.Page("/Account/Login")
            };

            _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),authProperties);
        }
    }


}
