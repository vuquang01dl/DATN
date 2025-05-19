﻿using Application.DTOs.BookingDTOs;
using Application.Services_Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Areas.Admin.Models;

namespace Presentation.Areas.Admin.Controllers
{

    [Authorize(Policy = "RequiredAdminOrManager")]
    [Area("Admin")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ITourService _tourService;
        private readonly IPaymentService _paymentService;
        private readonly ICustomerService _customerService;
        public BookingController(IBookingService bookingService, ITourService tourService, IPaymentService paymentService, ICustomerService customerService)
        {
            _bookingService = bookingService;
            _tourService = tourService;
            _paymentService = paymentService;
            _customerService = customerService;
        }


        [Authorize(Policy = "booking-view")]
        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingService.GetAllAsync();
            var bookingsViewModel = new List<BookingViewModel>();
            foreach( var i in bookings){
                var payment = await _paymentService.GetByBookingId(i.BookingID);
                ViewData[$"Status_{i.BookingID}"] = payment.Status;

                var customer = await _customerService.GetById(i.CustomerID);
                var tour = await _tourService.GetByIdAsync(i.TourID);
                var bookingViewModel = new BookingViewModel
                {
                    BookingID = i.BookingID,
                    CustomerID = i.CustomerID,
                    TourID = i.TourID,
                    Adult = i.Adult,
                    Child = i.Child,
                    CreateAt = i.CreateAt,
                    ModifyAt = i.ModifyAt,
                    TotalPrice = i.TotalPrice,
                    Customer = customer,
                    Tour = tour,
                };
                bookingsViewModel.Add(bookingViewModel);
            }
            ViewData["ActivePage"] = "TourManager";
            return View(bookingsViewModel);
        }
        [Authorize(Policy = "booking-add")]
        public async Task<IActionResult> Create()
        {
            var customers = await _customerService.GetAllAsync();
            if (customers != null)
            {
                var listCustomer = customers.Select(customer => new CustomerViewModel
                {
                    CustomerID = customer.CustomerID,
                    FirstName = $"{customer.FirstName} {customer.LastName}"
                }).ToList();

                ViewBag.ListCustomer = new SelectList(listCustomer, "CustomerID", "FirstName");
            }

            var tours = await _tourService.GetAllAsync();
            if (tours != null)
            {
                var listTour = tours.Select(tour => new TourViewModel
                {
                    TourID = tour.TourID,
                    Title = tour.Title
                }).ToList();

                ViewBag.ListTour = new SelectList(listTour, "TourID", "Title");
            }
            ViewData["ActivePage"] = "TourManager";
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "booking-add")]
        public async Task<IActionResult> Create(BookingCreationDto dto)
        {
            if (ModelState.IsValid)
            {
                await _bookingService.CreateAsync(dto);
                
                    TempData["NotificationType"] = "success";
                    TempData["NotificationTitle"] = "Thành công!";
                    TempData["NotificationMessage"] = "Đặt Tour thành công!";
                return RedirectToAction("Index");
            }

            TempData["NotificationType"] = "danger";
            TempData["NotificationTitle"] = "Thất bại!";
            TempData["NotificationMessage"] = "Đặt Tour thất bại, hãy kiểm tra lại các thông tin!";
            return View(dto);
        }
        [Authorize(Policy = "booking-update")]
        public async Task<IActionResult> Update(Guid BookingID)
        {
            var booking = await _bookingService.GetById(BookingID);
            if (booking != null)
            {
                var bookingViewModel = new BookingViewModel
                {
                    BookingID = booking.BookingID,
                    CustomerID = booking.CustomerID,
                    TourID = booking.TourID,
                    Adult = booking.Adult,
                    Child = booking.Child,
                    CreateAt = booking.CreateAt,
                    ModifyAt = booking.ModifyAt,
                    TotalPrice = booking.TotalPrice
                };
                ViewData["ActivePage"] = "TourManager";
                return View(bookingViewModel);
            }
            TempData["NotificationType"] = "danger";
            TempData["NotificationTitle"] = "Thất bại!";
            TempData["NotificationMessage"] = $"Không thể lấy giữ liệu từ id: {BookingID}";
            ViewData["ActivePage"] = "TourManager";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Policy = "booking-update")]
        public async Task<IActionResult> Update(Guid BookingID, BookingUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                var checkData = await _bookingService.UpdateAsync(BookingID, dto);
                if (!checkData)
                {
                    TempData["NotificationType"] = "danger";
                    TempData["NotificationTitle"] = "Thất bại!";
                    TempData["NotificationMessage"] = "Không tìm thấy tour hoặc số lượng người vượt quá số lượng!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["NotificationType"] = "success";
                    TempData["NotificationTitle"] = "Thành Công!";
                    TempData["NotificationMessage"] = "Cập nhật thông tin đặt tour thành công!";
                    return RedirectToAction("Index");
                }
                
            }
            TempData["NotificationType"] = "danger";
            TempData["NotificationTitle"] = "Thất bại!";
            TempData["NotificationMessage"] = "Dữ liệu nhập không hợp lệ!";
            return RedirectToAction("Index");
        }


        [Authorize(Policy = "booking-delete")]
        public async Task<IActionResult> Delete(Guid BookingID)
        {
            var booking = await _bookingService.GetById(BookingID);
            if (booking == null)
            {
                TempData["NotificationType"] = "danger";
                TempData["NotificationTitle"] = "Thất bại!";
                TempData["NotificationMessage"] = "Xóa thông tin đặt Tour thất bại, Không tìm thấy dữ liệu"; ;
                return RedirectToAction("Index");
            }
            await _bookingService.CancelBooking(BookingID);

            TempData["NotificationType"] = "success";
            TempData["NotificationTitle"] = "Thành công!";
            TempData["NotificationMessage"] = "Xóa thông tin đặt tour thành công";

            return RedirectToAction("Index");
        }
    }

}

