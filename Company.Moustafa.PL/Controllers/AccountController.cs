using Company.Moustafa.DAL.Models;
using Company.Moustafa.PL.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Moustafa.PL.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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


        #endregion

        #region SignOut


        #endregion
    }
}
