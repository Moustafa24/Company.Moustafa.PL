﻿using AutoMapper;
using Company.BLL.Interfaces;
using Company.Moustafa.BLL.Interfaces;
using Company.Moustafa.BLL.Repositories;
using Company.Moustafa.DAL.Models;
using Company.Moustafa.PL.Dtos;
using Company.Moustafa.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.IdentityModel.Tokens;

namespace Company.Moustafa.PL.Controllers
{
    [Authorize]

    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController
            (
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork
            )
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchInput))
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            else
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
           
            #region Commit
            //// Dictionary : 3 Properties
            //// 1.ViewData : Transfer Data from Controller (Action) to View
            //ViewData["Message"] = "Hello From ViewData!";

            //// 2.ViewBag  : Transfer Data from Controller (Action) to View
            //ViewBag.Message = new { Message = "Hello From ViewBag!" };

            // 3.TempData  
            #endregion

            return View(employees);
        }


        public async Task<IActionResult> Search(string? SearchInput)
        {

            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchInput))
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            else
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);


            return View("EmployeePartialView/EmployeeTablePartialView", employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "Image");

                var employee = _mapper.Map<Employee>(model);
                await _unitOfWork.EmployeeRepository.AddAsync(employee);

                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {

                    TempData["Message"] = $"Employee{employee.Name} Added Successfully!";

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
           
            if (id is null)
                return BadRequest("Invalid Id");
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null)
                return NotFound(new { statusCode = 404, message = $"Employee with id {id} Not Found" });

            var dto = _mapper.Map<CreateEmployeeDto>(employee);
            return View(viewName, dto);
        }

        public Task<IActionResult> Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {

                if (id != model.Id)
                    return BadRequest("Invalid Id");

                if (model.ImageName is not null && model.Image is not null)
                    DocumentSettings.DeleteFile(model.ImageName, "Image");

                if (model.Image is not null)
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "Image");


                var employee = _mapper.Map<Employee>(model);
                _unitOfWork.EmployeeRepository.Update(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                    return RedirectToAction("Index");
            }


            return View(model);
        }

        public Task<IActionResult> Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, CreateEmployeeDto model)
        {
            if (id != model.Id)
                return BadRequest("Invalid Id");

            var employee = _mapper.Map<Employee>(model);
            _unitOfWork.EmployeeRepository.Delete(employee);
            var count = await _unitOfWork.CompleteAsync();

            if (count > 0)
            {
                if (model.ImageName is not null)
                    DocumentSettings.DeleteFile(model.ImageName, "Image");

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
