using Company.Moustafa.DAL.Models;
using Company.Moustafa.PL.Dtos;
using Company.Moustafa.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Moustafa.PL.Controllers
{
    public class RoleController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDto> roles;

            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name,
                     


                });

            }
            else
            {

                roles = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name,



                }).Where(r =>r.Name.ToLower().Contains(SearchInput));


            }

            return View(roles);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Name);
                if (role is null)
                { 
                    role= new IdentityRole()
                    { 
                        Name = model.Name,

                    };
                   var result = await  _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
           
            }

            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id == null) return BadRequest(new { error = "Invalid Id" }); // 400  

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound(new { statusCode = 404, message = $"Role With Id :{id} is not found" });

            var dto = new RoleToReturnDto()
            {
                Id = role.Id,
                Name = role.Name,
            };
            return View(viewName, dto);
        }

        public Task<IActionResult> Edit(string? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {

                if (id != model.Id) return BadRequest("Invalid Operation !");

                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return BadRequest("Invalid Operation !");
              
                var roleresult =await _roleManager.FindByNameAsync(model.Name);
                if (roleresult is  null)
                {
                    role.Name = model.Name;


                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }

                ModelState.AddModelError("", "Invalid Operations!");

            }


            return View(model);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {


                if (id != model.Id) return BadRequest("Invalid Operation !");

                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return BadRequest("Invalid Operation !");

                    role.Name = model.Name;


                    var result = await _roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                ModelState.AddModelError("", "Invalid Operations!");


            }


            return View(model);
        }

        
    }
}
