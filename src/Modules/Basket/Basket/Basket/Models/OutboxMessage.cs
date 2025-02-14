namespace Basket.Basket.Models;

public class OutboxMessage : Entity<Guid>
{
    public string Type { get; set; }
    public string Content { get; set; }
    public bool Processed { get; set; }
    public DateTime OccuredOn { get; set; }
    public DateTime ProcessedOn { get; set; }
}
