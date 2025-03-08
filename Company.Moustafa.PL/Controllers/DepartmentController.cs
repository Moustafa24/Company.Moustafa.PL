using Company.Moustafa.BLL.Interfaces;
using Company.Moustafa.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Company.Moustafa.PL.Controllers
{
    //MVC Controller 
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository= departmentRepository;
        }

        [HttpGet] //GEt: /Department/Index
        public IActionResult Index()
        {
            var departments =  _departmentRepository.GetAll();
            return View(departments);
        }


    }
}
