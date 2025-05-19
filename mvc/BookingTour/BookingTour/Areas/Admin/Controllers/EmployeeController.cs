﻿using Application.DTOs.EmployeeDTOs;
using Application.Services_Interface;
using Infrastructure.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models;

namespace Presentation.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequiredAdminOrManager")]
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAccountService _accountService;

        public EmployeeController(IEmployeeService employeeService, IAccountService accountService)
        {
            _employeeService = employeeService;
            _accountService = accountService;
        }

        [Authorize(Policy = "employee-view")]
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllAsync();
            var employeesViewModel = new List<EmployeeViewModel>();

            foreach (var c in employees)
            {
                var employeeModelView = new EmployeeViewModel
                {
                    EmployeeID = c.EmployeeID,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Position = c.Position,
                    Address = c.Address,
                    Image = c.Image,
                };
                employeesViewModel.Add(employeeModelView);
            }
            ViewData["ActivePage"] = "UserManager";
            return View(employeesViewModel);
        }

        [Authorize(Policy = "employee-add")]
        public IActionResult Create()
        {
            ViewData["ActivePage"] = "UserManager";
            return View();
        }


        // POST: /Employee/Create
        [HttpPost]
        [Authorize(Policy = "employee-add")]
        public async Task<IActionResult> Create(EmployeeCreationDto employee)
        {
            if (ModelState.IsValid)
            {
                var accountResult = await _accountService.CreateUserAsync(employee);
                if (accountResult.Result.Succeeded)
                {
                    employee.AccountID = accountResult.UserId;
                    await _employeeService.CreateAsync(employee);
                    TempData["NotificationType"] = "success";
                    TempData["NotificationTitle"] = "Thành công!";
                    TempData["NotificationMessage"] = "Thêm nhân viên  thành công";
                    return RedirectToAction("Index");
                }
                TempData["NotificationType"] = "danger";
                TempData["NotificationTitle"] = "Thất bại!";
                TempData["NotificationMessage"] = "Không thể tạo tài khoản hoặc email đã tồn tại !";

                return View(employee);
            }
            TempData["NotificationType"] = "danger";
            TempData["NotificationTitle"] = "Thất bại!";
            TempData["NotificationMessage"] = $"Dữ liệu nhập không hợp lệ";
            return View(employee);
        }


        // GET: /Employee/Update?{customer_id}
        [Authorize(Policy = "employee-update")]
        public async Task<IActionResult> Update(Guid EmployeeID)
        {

            var employee = await _employeeService.GetById(EmployeeID);
            if (employee == null)
            {
                TempData["NotificationType"] = "danger";
                TempData["NotificationTitle"] = "Thất bại!";
                TempData["NotificationMessage"] = $"Không tìm thấy dữ liệu địa điểm id: {EmployeeID}";
                ViewData["ActivePage"] = "UserManager";
                return RedirectToAction("Index");
            }
            var employeeViewData = new EmployeeUpdateDto
            {
                EmployeeID = employee.EmployeeID,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Position = employee.Position,
                Address = employee.Address,
                Email = employee.Email,
                Phone = employee.Phone,
                Image = employee.Image,
            };
            ViewData["ActivePage"] = "UserManager";
            return View(employeeViewData);
        }

        // POST: /Employee/Update?{customer_id}
        [HttpPost]
        [Authorize(Policy = "employee-update")]
        public async Task<IActionResult> Update(EmployeeUpdateDto employee)
        {

            if (ModelState.IsValid)
            {
                await _employeeService.UpdateAsync(employee.EmployeeID, employee);
                TempData["NotificationType"] = "success";
                TempData["NotificationTitle"] = "Thành công!";
                TempData["NotificationMessage"] = "Cập nhật thông tin nhân viên thành công";
                return RedirectToAction("Index");

            }
            TempData["NotificationType"] = "danger";
            TempData["NotificationTitle"] = "Thất bại!";
            TempData["NotificationMessage"] = $"Không tìm thấy nhân viên {employee.LastName} {employee.FirstName}";
            return View(employee);
        }

        [Authorize(Policy = "employee-delete")]
        public async Task<IActionResult> Delete(Guid EmployeeID)
        {
            var customer = await _employeeService.GetById(EmployeeID);
            if (customer != null)
            {
                await _employeeService.DeleteAsync(EmployeeID);
                TempData["NotificationType"] = "success";
                TempData["NotificationTitle"] = "Thành công!";
                TempData["NotificationMessage"] = "Xóa nhân viên thành công";
                return RedirectToAction("Index");
            }
            TempData["NotificationType"] = "danger";
            TempData["NotificationTitle"] = "Thất bại!";
            TempData["NotificationMessage"] = $"Không tìm thấy dữ liệu địa điểm id: {EmployeeID}";
            return RedirectToAction("Index");

        }
    }
}