using System.Net.WebSockets;
using Company.Moustafa.DAL.Models;
using Company.Moustafa.PL.Dtos;
using Company.Moustafa.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Moustafa.PL.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMailService _mailService;


        public AccountController(UserManager<AppUser> userManager ,SignInManager<AppUser> signInManager , IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
        }


        #region SignUp

        [HttpGet] //Get: /Account/SignUp
        public IActionResult SignUp()
        { 
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp( SignUpDto model )
        {
            if (ModelState.IsValid)
            {
             var user = await   _userManager.FindByNameAsync(model.UserName);

                if (user is null)
                { 
                   user = await _userManager.FindByEmailAsync(model.Email);

                    if (user is null)
                    {
                        user = new AppUser()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree,



                        };


                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(SignIn));

                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                    }
                }

                ModelState.AddModelError("", "Invalid SignUp !!");


               

            }


            return View(model);
        }



        #endregion

        #region SignIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDot model)
        {
            if (ModelState.IsValid)
            {
              var user = await  _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);

                    if (flag) 
                    {
                        //Sign In

                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false ,false);
                        
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");

                        }
                    }
                   
                }

                ModelState.AddModelError("", "Invalid Login !");
            }

            return View(model);
        }


        #endregion

        #region SignOut

        [HttpGet]
        public  new  async Task<IActionResult> SignOut()
        {
             await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));


        }

        #endregion'

        #region Forget Password

        [HttpGet]
        public IActionResult ForgetPassword() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordURL(ForgetPasswordDto model)
        {

            if (ModelState.IsValid)
            {
               var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {

                    //Genertae Token 
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // Create URL 

                  var url=  Url.Action("ResetPassword", "Account", new { email = model.Email , token } ,Request.Scheme );


                    //Create Email 
                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = url
                    };


                    // Send Email 
                    //var flag = Emailsetting.SendEmail(email);
                    _mailService.SendEmail(email);

                    return RedirectToAction(nameof(CheckYourInbox));

                }
            ModelState.AddModelError("", "Invalid Reset Password !!");
            }


            return View("ForgetPassword" , model);
        }

        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }

        #endregion

        #region Reset Password

        [HttpGet]
        public IActionResult ResetPassword(string email , string token )
        {
            TempData["email"] = email; 
            TempData["token"] = token;  
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model )
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                if (email == null || token == null)
                    return BadRequest(error: "Invalid Operations");

                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(actionName: "SignIn");
                    }
                }

                ModelState.AddModelError(key: "", errorMessage: "Invalid Reset Password Operation");
            }

            return View();
        }


        #endregion


    }


}
