using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Booking
{
    [Key]
    public Guid BookingId { get; set; }

    // Foreign key đến Tour
    public Guid TourId { get; set; }

    [ForeignKey(nameof(TourId))]
    public Tour Tour { get; set; }

    // Foreign key đến Customer
    public Guid CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; }

    // Foreign key đến Payment
    public Guid? PaymentId { get; set; }

    [ForeignKey(nameof(PaymentId))]
    public Payment? Payment { get; set; }

    // Tổng tiền
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    public int Adult { get; set; }
    public int Child { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifyAt { get; set; }

    public string? Note { get; set; }
}
