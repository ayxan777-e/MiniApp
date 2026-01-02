namespace MiniApp.Data.Entities;

public class DiningTable
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    public string DiningTableNumber { get; set; } = null!;
    public int Capacity { get; set; }
    public bool IsActive { get; set; }=true;
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
