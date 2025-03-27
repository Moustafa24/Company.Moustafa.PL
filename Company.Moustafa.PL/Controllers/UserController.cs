using Company.Moustafa.DAL.Models;
using Company.Moustafa.PL.Dtos;
using Company.Moustafa.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Moustafa.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<UserToReternDto> users;

            if (string.IsNullOrEmpty(SearchInput))
            {
               users= _userManager.Users.Select( U => new UserToReternDto()
               {
                         Id = U.Id,
                         UserName = U.UserName,
                         Email = U.Email,
                         FirstName = U.FirstName,
                         LastName = U.LastName,
                         Roles = _userManager.GetRolesAsync(U).Result
                   

               });
                
            }
            else
            {

                users = _userManager.Users.Select(U => new UserToReternDto()
                {
                    Id = U.Id,
                    UserName = U.UserName,
                    Email = U.Email,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Roles = _userManager.GetRolesAsync(U).Result


                }).Where(U => U.FirstName.ToLower().Contains(SearchInput.ToLower()));


            }

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id == null) return BadRequest(new { error = "Invalid Id" }); // 400  

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound(new { statusCode = 404, message = $"User With Id :{id} is not found" });

            var dto = new UserToReternDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = _userManager.GetRolesAsync(user).Result
            };

            return View(viewName, dto);
        }

        public Task<IActionResult> Edit(string? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserToReternDto model)
        {
            if (ModelState.IsValid)
            {

                if(id != model.Id) return BadRequest("Invalid Operation !");

              var user=await   _userManager.FindByIdAsync(id);

                if (user is null) return BadRequest("Invalid Operation !");
               
                  user.UserName = model.UserName;
                  user.FirstName = model.FirstName;
                  user.LastName = model.LastName; 
                  user.Email = model.Email;

                 var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }


            }


            return View(model);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, UserToReternDto model)
        {
            if (ModelState.IsValid)
            {

                if (id != model.Id) return BadRequest("Invalid Operation !");

                var user = await _userManager.FindByIdAsync(id);

                if (user is null) return BadRequest("Invalid Operation !");

                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }


            }


            return View(model);
        }


    }
}
