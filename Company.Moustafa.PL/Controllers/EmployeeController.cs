using Company.Moustafa.BLL.Interfaces;
using Company.Moustafa.DAL.Models;
using Company.Moustafa.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Moustafa.PL.Controllers
{
    public class EmployeeController : Controller
    {
        // MVC Controller

        private readonly IEmployeeRepository _employeeRepository;

        // ASK CLR Create Object From DepartmentRepository
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet] // GET: /Department/Index
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();


            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Name = model.Name,
                    Address = model.Address,
                    Age = model.Age,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Phone = model.Phone,
                    Salary = model.Salary,


                };

                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); // 400

            var employee = _employeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee With Id : {id} is not found" });

            return View(viewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {


            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest(); // 400

                var count = _employeeRepository.Update(employee);
                if (count > 0) return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }



        [HttpGet]
        public IActionResult Delete(int? id)
        {

            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest(); // 400

                var count = _employeeRepository.Delete(employee);
                if (count > 0) return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }


    }
}
