using AutoMapper;
using Company.BLL.Interfaces;
using Company.Moustafa.BLL.Interfaces;
using Company.Moustafa.BLL.Repositories;
using Company.Moustafa.DAL.Models;
using Company.Moustafa.PL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.IdentityModel.Tokens;

namespace Company.Moustafa.PL.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository; 
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;


        // Ask CLR to create an object of employeeRepository
        public EmployeeController(
            IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IMapper mapper
            )
        {
            _employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet] // GET: employee/Index
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;

            if(string.IsNullOrEmpty(SearchInput))
            {
                 employees = _employeeRepository.GetAll();

            }
            else
            {
               employees = _employeeRepository.GetByName(SearchInput);

            }
            return View(employees);
        }

        [HttpGet] // GET: employee/Create
        public IActionResult Create()
        {
            //var departments = _departmentRepository.GetAll();
            //ViewBag.departments = departments;
            return View(/*departments*/);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                ////Manual Mapping 
                //var employee = new Employee
                //{
                //    Name = model.Name,
                //    Age = model.Age,
                //    Email = model.Email,
                //    Address = model.Address,
                //    Phone = model.Phone,
                //    Salary = model.Salary,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    HiringDate = model.HiringDate,
                //    DepartmentId= model.DepartmentId

                //};

               var employee= _mapper.Map<Employee>(model);
                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = $"Employee {model.EmpName} is Added";
                   return RedirectToAction("Index");
                }
                    
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest("Invalid Id"); // 400

            var employee = _employeeRepository.Get(id.Value);

            if (employee is null)
                return NotFound(new { statusCode = 404, message = $"Employee with Id {id} is not found" });

            return View(viewName, employee);
        }
       
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null)
            //    return BadRequest("Invalid Id"); // 400

            //var department = _employeeRepository.Get(id.Value);

            //if (employee is null)
            //    return NotFound(new { statusCode = 404, message = $"employee with Id {id} is not found" });

            //var departments = _departmentRepository.GetAll();
            //ViewBag.departments = departments;
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
                if (count > 0)
                    return RedirectToAction("Index");
            }
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            //var employee = _employeeRepository.Get(id);
            //if (employee is null)
            //    return NotFound(new { statusCode = 404, message = $"employee with Id {id} is not found" });
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Name = model.EmpName,
                    Age = model.Age,
                    Email = model.Email,
                    Address = model.Address,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    HiringDate = model.HiringDate,

                };
                var count = _employeeRepository.Delete(employee);
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
