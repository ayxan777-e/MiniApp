namespace MiniApp.DTOs.DiningTableDto;

public class CreateDiningTableRequest
{
    public int RestaurantId { get; set; }
    public string DiningTableNumber { get; set; } = null!;
    public int Capacity { get; set; }
}
