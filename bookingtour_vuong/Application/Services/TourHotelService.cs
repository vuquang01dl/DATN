using Application.DTOs;
using Application.Services_Interface;
using Domain.Entities;
using Domain.Repositories;

public class TourHotelService : ITourHotelService
{
    private readonly ITourHotelRepository _repo;
    private readonly ITourRepository _tourRepo;
    private readonly IHotelRepository _hotelRepo;

    public TourHotelService(ITourHotelRepository repo, ITourRepository tourRepo, IHotelRepository hotelRepo)
    {
        _repo = repo;
        _tourRepo = tourRepo;
        _hotelRepo = hotelRepo;
    }

    public async Task<IEnumerable<TourHotelDTO>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        // Lấy thêm tên từ Tour và Hotel
        return list.Select(th => new TourHotelDTO
        {
            TourName = th.Tour.Name,
            HotelName = th.Hotel.Name
        });
    }

    public async Task<TourHotelDTO?> GetByNamesAsync(string tourName, string hotelName)
    {
        var tour = await _tourRepo.GetByNameAsync(tourName);
        var hotel = await _hotelRepo.GetByNameAsync(hotelName);

        if (tour == null || hotel == null) return null;
        var th = await _repo.GetByKeysAsync(tour.Id, hotel.HotelID);
        if (th == null) return null;

        return new TourHotelDTO
        {
            TourName = tour.Name,
            HotelName = hotel.Name
        };
    }

    public async Task AddAsync(TourHotelDTO dto)
    {
        var tour = await _tourRepo.GetByNameAsync(dto.TourName);
        var hotel = await _hotelRepo.GetByNameAsync(dto.HotelName);

        if (tour == null || hotel == null) throw new Exception("Tour hoặc Hotel không tồn tại!");

        var entity = new TourHotel
        {
            TourId = tour.Id,
            HotelId = hotel.HotelID
        };
        await _repo.AddAsync(entity);
    }

    public async Task DeleteAsync(string tourName, string hotelName)
    {
        // Sử dụng repository để xóa
        await _repo.DeleteAsync(tourName, hotelName);
    }
}
