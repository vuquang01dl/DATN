using Application.DTOs;
using Application.Services_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;



namespace Application.Services
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _repo;
        private readonly IBookingRepository _bookingRepo;
        private readonly ICustomerRepository _customerRepo;


        public TourService(ITourRepository repo)
        {
            _repo = repo;
        }
        public TourService(ITourRepository repo, IBookingRepository bookingRepo, ICustomerRepository customerRepo)
        {
            _repo = repo;
            _bookingRepo = bookingRepo;
            _customerRepo = customerRepo;
        }

        public async Task<IEnumerable<TourDTO>> GetAllToursAsync()
        {
            var tours = await _repo.GetAllAsync();
            return tours.Select(t => new TourDTO
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Price = t.Price,
                Location = t.Location,
                StartDate = t.StartDate,
                EndDate = t.EndDate
            });
        }

        public async Task<TourDTO?> GetTourByIdAsync(Guid id)
        {
            var t = await _repo.GetByIdAsync(id);
            if (t == null) return null;
            return new TourDTO
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Price = t.Price,
                Location = t.Location,
                StartDate = t.StartDate,
                EndDate = t.EndDate
            };
        }

        public async Task AddTourAsync(TourDTO dto)
        {
            var t = new Tour
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Location = dto.Location,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };
            await _repo.AddAsync(t);
        }

        public async Task UpdateTourAsync(TourDTO dto)
        {
            var t = new Tour
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Location = dto.Location,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };
            await _repo.UpdateAsync(t);
        }

        public async Task DeleteTourAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
        public async Task UpdateStatusAsync(Guid id, TourStatus newStatus)
        {
            var tour = await _repo.GetByIdAsync(id);
            if (tour != null)
            {
                tour.Status = newStatus;
                await _repo.UpdateAsync(tour);
            }
        }

        public async Task<IEnumerable<TourStatusDTO>> GetAllTourStatusesAsync()
        {
            var bookings = (await _bookingRepo.GetAllAsync()).Cast<Booking>().ToList();
            var tours = (await _repo.GetAllAsync()).Cast<Tour>().ToList();
            var customers = (await _customerRepo.GetAllAsync()).Cast<Customer>().ToList();

            var bookingTour = bookings
                .Join(tours,
                      b => b.TourID,
                      t => t.Id,
                      (b, t) => new BookingTourJoin { Booking = b, Tour = t });

            var result = bookingTour
                .Join(customers,
                      bt => bt.Booking.CustomerID,
                      c => c.CustomerID,
                      (bt, c) => new TourStatusDTO
                      {
                          TourName = bt.Tour.Name,
                          CustomerName = c.LastName,
                          BookingDate = bt.Booking.CreateAt.ToString("yyyy-MM-dd"),
                          Status = GetStatus(bt.Tour.StartDate, bt.Tour.EndDate)
                      });

            return result.ToList();
        }

        // Class tạm dùng cho Join
        private class BookingTourJoin
        {
            public Booking Booking { get; set; }
            public Tour Tour { get; set; }
        }




        private string GetStatus(DateTime start, DateTime end)
        {
            var today = DateTime.Today;
            if (today < start) return "Chưa bắt đầu";
            if (today >= start && today <= end) return "Đang diễn ra";
            return "Đã kết thúc";
        }



    }
}