﻿using Application.DTOs.CustomerDTOs;
using Application.Services_Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models;

namespace Presentation.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequiredAdminOrManager")]
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        public CustomerController(ICustomerService customerService, IAccountService accountService)
        {
            _customerService = customerService;
            _accountService = accountService;
        }
        [Authorize(Policy = "customer-view")]
        public async Task<IActionResult> Index()
        {
            // Lấy tất cả khách hàng
            var customers = await _customerService.GetAllAsync();

            // Khởi tạo danh sách CustomerViewModel
            var customersViewModel = new List<CustomerViewModel>();

            // Duyệt qua từng khách hàng và lấy thông tin tài khoản
            foreach (var c in customers)
            {

                // Tạo CustomerViewModel từ thông tin khách hàng và tài khoản
                var customerViewModel = new CustomerViewModel
                {
                    CustomerID = c.CustomerID,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address,
                    Image = c.Image
                };

                // Thêm vào danh sách
                customersViewModel.Add(customerViewModel);
            }
            ViewData["ActivePage"] = "UserManager";
            // Trả về View với danh sách CustomerViewModel
            return View(customersViewModel);
        }

        [Authorize(Policy = "customer-add")]
        // GET: /Customer/Create
        public IActionResult Create()
        {
            ViewData["ActivePage"] = "UserManager";
            return View();
        }

        // POST: /Customer/Create
        [HttpPost]
        [Authorize(Policy = "customer-add")]
        public async Task<IActionResult> Create(CustomerCreationDto customer)
        {
            if (ModelState.IsValid)
            {
                var accountResult = await _accountService.CreateUserAsync(customer);
                if (accountResult.Result.Succeeded)
                {
                    customer.AccountID = accountResult.UserId;
                    await _customerService.CreateAsync(customer);
                    TempData["NotificationType"] = "success";
                    TempData["NotificationTitle"] = "Thành Công!";
                    TempData["NotificationMessage"] = "Thêm khách hàng thành công!";

                    return RedirectToAction("Index");
                }
                TempData["NotificationType"] = "danger";
                TempData["NotificationTitle"] = "Thất bại!";
                TempData["NotificationMessage"] = $"Tài khoản {customer.Email} đã tồn tại!";
                return View(customer);

            }
            TempData["NotificationType"] = "danger";
            TempData["NotificationTitle"] = "Thất bại!";
            TempData["NotificationMessage"] = "Dữ liệu nhập không hợp lệ!";
            return View(customer);
        }

        [Authorize(Policy = "customer-update")]
        // GET: /Customer/Update?{customer_id}
        public async Task<IActionResult> Update(Guid CustomerID)
        {
            var customer = await _customerService.GetById(CustomerID);
            if (customer == null)
            {
                TempData["NotificationType"] = "danger";
                TempData["NotificationTitle"] = "Thất bại!";
                TempData["NotificationMessage"] = $"Không thể lấy giữ liệu từ id: {CustomerID}";
                ViewData["ActivePage"] = "UserManager";
                return RedirectToAction("Index");
            }
            var customerViewModel = new CustomerUpdateDto
            {
                CustomerID = customer.CustomerID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                Phone = customer.Phone,
                Email = customer.Email,
                Image = customer.Image,
            };
            ViewData["ActivePage"] = "UserManager";
            return View(customerViewModel);
        }

        // POST: /Customer/Update?{CustomerID}
        [HttpPost]
        [Authorize(Policy = "customer-update")]
        public async Task<IActionResult> Update(CustomerUpdateDto customer, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                var customerData = await _customerService.GetById(customer.CustomerID);
                if (customerData == null)
                {
                    TempData["NotificationType"] = "danger";
                    TempData["NotificationTitle"] = "Thất bại!";
                    TempData["NotificationMessage"] = $"Không tìm thấy Customer ID: {customerData.CustomerID}";
                    return RedirectToAction("Index");
                }
                // Kiểm tra nếu có ảnh mới được tải lên
                if (imageFile != null && imageFile.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/assetPrive/img/customer/", imageFile.FileName); // Đường dẫn lưu ảnh
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    customer.Image = imageFile.FileName;
                }
                else
                {
                    // Nếu không có ảnh mới, giữ lại ảnh cũ
                    customer.Image = customer.Image ?? customerData.Image; // Nếu model.Image là null thì giữ ảnh cũ
                }

                await _customerService.UpdateAsync(customer.CustomerID, customer);
                TempData["NotificationType"] = "success";
                TempData["NotificationTitle"] = "Thành Công!";
                TempData["NotificationMessage"] = $"Cập nhật thông tin khách hàng {customer.LastName} {customer.FirstName} thành công!";
                return RedirectToAction("Index");
            }

            TempData["NotificationType"] = "danger";
            TempData["NotificationTitle"] = "Thất bại!";
            TempData["NotificationMessage"] = "Dữ liệu nhập không hợp lệ!";
            return View(customer);
        }

        // POST: /Customer/Delete?{customer_id}
        [Authorize(Policy = "customer-delete")]
        public async Task<IActionResult> Delete(Guid CustomerID)
        {
            var customer = await _customerService.GetById(CustomerID);
            if (customer != null)
            {
                await _customerService.DeleteAsync(CustomerID);
                TempData["NotificationType"] = "success";
                TempData["NotificationTitle"] = "Thành Công!";
                TempData["NotificationMessage"] = $"Xóa khách hàng {customer.FirstName}!";
                return RedirectToAction("Index");
            }

            TempData["NotificationType"] = "danger";
            TempData["NotificationTitle"] = "Thất bại!";
            TempData["NotificationMessage"] = "Không tìm thấy khách hàng";
            return RedirectToAction("Index");

        }
    }
}