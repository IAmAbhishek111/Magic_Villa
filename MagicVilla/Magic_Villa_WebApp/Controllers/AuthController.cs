using Magic_Villa_WebApp.Models;
using Magic_Villa_WebApp.Models.Dto;
using Magic_Villa_WebApp.Services.IServices;
using MagicVilla_Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Claims;

namespace Magic_Villa_WebApp.Controllers
{
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;

        }

        [HttpGet]

        public IActionResult Login()
        {
            LoginRequestDto obj = new();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            APIResponse response = await _authService.LoginAsync<APIResponse>(obj);

            if(response != null && response.IsSuccess)
            {
                //After we receive the response, we will check if that is not null and if that is successful we can deserialize the result in that response and we know that will be the log in response DTO that we have.  

                // Let's call that model is equal to we will use JSON convert to deserialize.              The output will be log in response DTO we just need to convert to string the response start result.That is similar to what we have been doing so far, so that should be straight forward.

                LoginResponseDto model = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));


                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, model.User.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, model.User.Role));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                /* After that we want to retrieve token from there and we want to store that in our session.*/
                HttpContext.Session.SetString(SD.SessionToken , model.Token);

                return RedirectToAction("Index", "Home");
            }

            else
            {
                ModelState.AddModelError("CustomError", response.ErrorMessages.FirstOrDefault());

                return View(obj);
            }
        }

        [HttpGet]

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            APIResponse result = await _authService.RegisterAsync<APIResponse>(obj);

            if (result != null && result.IsSuccess)
            {
                return RedirectToAction(nameof(Login));

            }

            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            //We need the key name that is inside a static detail.session token and we will make that an empty string.
            HttpContext.Session.SetString(SD.SessionToken, "");
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> AccessDenied()
        {
            return View();

        }

    }
}
