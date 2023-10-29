using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserInfoService _userInfoService;
        public AccountController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }

        [Authorize(Roles = "Admin")]
        [Route("userinfo")]
        public async Task<IActionResult> UserInfo()
        {
            var userInfo = await _userInfoService.GetUserInfo();
            return View(userInfo);
        }

        public IActionResult AccessDenied() 
        {
            return View();  
        }
    }
}
