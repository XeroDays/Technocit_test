using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Technocitt.AssistantControllers;
using Technocitt.Models;
using Technocitt.mydb;
using Technocitt.Services;

namespace Technocitt.Controllers
{
    
    public class AccountController : Controller


    {

        private readonly IUsersProvider usersServices;

        public AccountController(IUsersProvider _usersServices)
        { 
            this.usersServices = _usersServices;
        }
         
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel user)
        {

            if (ModelState.IsValid)
            {
                CustomUser usser = usersServices.GetUserWithUsername(user.Email).Result;
                if (usser != null)
                {
                    if (usser.Password.Equals(sysController.CreateMD5(user.Password)))
                    {
                        await SignInUser(usser);
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Login Credentials");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login Credentials");
                    return View();
                }
            }
            else
            {
                return View();
            }   

             
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel user)
        {

            if(ModelState.IsValid)
            {
                CustomUser check =  await usersServices.GetUserWithUsername(user.Email);
                if(check==null)
                {
                    // here i am hcekcing if the user already exist or not
                    ModelState.AddModelError("", "User Already Exist!");
                    return View();
                }


                CustomUser usser =  await usersServices.RegisterUser( user.Firstname    , user.Lastname, user.Email, user.Mobile, user.Password);
                if(usser != null)
                {
                    //here i am creating cookie session for the user
                    await SignInUser (usser);
                    return RedirectToAction("Index", "Dashboard");
                } else
                {
                    ModelState.AddModelError("", "Error Creating User");
                    return View();
                }    
            }  else
            {
                return View();
            }
          
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        private async Task SignInUser(CustomUser user )
        {
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.FirstName),
                        new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role,user.isAdmin? "Admin":"User"),
                    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


            var pricipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(pricipal); 
        }


    }
}
