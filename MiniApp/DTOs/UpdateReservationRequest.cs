namespace MiniApp.DTOs;

public class UpdateReservationRequest
{
    public int DiningTableId { get; set; }
    public DateTime ReservationDate { get; set; }
    public int GuestCount { get; set; }
    public string CustomerName { get; set; } = null!;
}
