using ePizzaHub.Models;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace ePizzaHub.UI.Controllers
{
    public class AccountController : Controller
    {
        IAuthService _authservice;
        public AccountController( IAuthService authService)
        {
            _authservice= authService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model ,string? returnUrl=null)
        {
            if (ModelState.IsValid)
            {
              var user=  _authservice.ValidateUser(model.Email, model.Password);
                if(user != null) 
                {
                    GenerateTicket(user);

                    if(!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else if(user.Roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Home", new {area="Admin"});
                    }
                    if (user.Roles.Contains("User"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "User" });
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
            }
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account", new { area = "" });

        }

        public IActionResult UnAuthorize()
        {
            return View();
        }

        private void GenerateTicket(UserModel user)
        {
            string strData=JsonSerializer.Serialize(user);
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.UserData,strData),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Role,string.Join(",",user.Roles))
            };

            var identity= new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var principle=new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
            };

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principle, properties);
        }
    }
}
