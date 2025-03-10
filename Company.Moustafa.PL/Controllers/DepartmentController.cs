﻿using Company.Moustafa.BLL.Interfaces;
using Company.Moustafa.BLL.Repositories;
using Company.Moustafa.DAL.Models;
using Company.Moustafa.PL.Dtos;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model )
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt,
                };
              var count=  _departmentRepository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest("Invalid Id"); //400

         var department =   _departmentRepository.Get(id.Value);
            if(department is null)return NotFound(new {StatusCode=404,message =$"Department With Id :{id} is not found" });

            return View(department);
        }

        [HttpGet]
       public IActionResult Edit(int? id)
        {
            if(id is null) return BadRequest("Invalid Id"); //400

            var department = _departmentRepository.Get(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department With Id :{id} is not found" });

            return View(department);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {

            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest("Error in Id");

                var count = _departmentRepository.Update(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            return View(department);

        }

        #region Update 
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int id,UpdateDepartmentDto model)
        //{

        //    if (ModelState.IsValid)
        //    {


        //        var department = new Department()
        //        {
        //             Id = id,
        //             Name = model.Name,
        //             Code = model.Code,
        //             CreateAt = model.CreateAt,
        //        };

        //        var count = _departmentRepository.Update(department);
        //        if (count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }

        //    }

        //    return View(model);

        //} 
        #endregion


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var department = _departmentRepository.Get(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department With Id :{id} is not found" });

            return View(department);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {

            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest("Error in Id");

                var count = _departmentRepository.Delete(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            return View(department);

        }


    }
}
